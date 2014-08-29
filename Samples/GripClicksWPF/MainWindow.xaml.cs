using Kinect.ReactiveV2;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GripClicksWPF
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IDisposable sceneChanges;
        private KinectSensor kinectSensor;

        public MainWindow()
        {
            this.kinectSensor = KinectSensor.GetDefault();
            this.kinectSensor.Open();

            this.DataContext = this;
            InitializeComponent();
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs args)
        {
            Body[] bodies = null;
            var reader = this.kinectSensor.BodyFrameSource.OpenReader();
            var bodyFrameObservable = this.kinectSensor
                                          .BodyFrameArrivedObservable(reader)
                                          .SelectBodies(bodies);

            this.SubscribeToSceneChanges(bodyFrameObservable);
        }

        private void SubscribeToSceneChanges(IObservable<Body[]> bodyFrameObservable)
        {
            var subscriptions = new Dictionary<ulong, IDisposable>(6);
            this.sceneChanges = this.kinectSensor
                                    .SceneChanges()
                                    .Subscribe(change =>
                                    {
                                        var trackingId = change.SceneChangedType.TrackingId;

                                        if (change.SceneChangedType is PersonEnteredScene)
                                        {
                                            subscriptions.Add(trackingId, this.SubscribeToRightHand(trackingId, bodyFrameObservable));
                                        }
                                        else if (change.SceneChangedType is PersonLeftScene)
                                        {
                                            var subscription = subscriptions[trackingId];
                                            subscriptions.Remove(trackingId);
                                            subscription.Dispose();
                                        }
                                    });
        }

        private IDisposable SubscribeToRightHand(ulong trackingId, IObservable<Body[]> bodyFrameObservable)
        {
            var ellipse = new Ellipse { Height = 70, Width = 70, Fill = this.GetRandomBrush() };
            this.panel.Children.Add(ellipse);

            var personSubscription = bodyFrameObservable.SelectJointOf(trackingId, JointType.HandRight)
                                                        .Select(j => j.Position)
                                                        .Subscribe(pos =>
                                                        {
                                                            var p = this.MapToWindowPoint(pos);
                                                            Canvas.SetLeft(ellipse, p.X);
                                                            Canvas.SetTop(ellipse, p.Y);
                                                        });

            var gripsSubscription = this.SubscribeToClicks(trackingId, bodyFrameObservable);

            return new CompositeDisposable
            {
                Disposable.Create(() => this.panel.Children.Remove(ellipse)),
                gripsSubscription,
                personSubscription,
            };
        }

        private IDisposable SubscribeToClicks(ulong trackingId, IObservable<Body[]> bodyFrameObservable)
        {
            var trackedBody = bodyFrameObservable.SelectTracked(trackingId);

            var grips = trackedBody.Where(_ => _.HandRightState == HandState.Closed)
                                   .Select(_ => this.IsOver<Rectangle>(this.MapToWindowPoint(_.Joints[JointType.HandRight].Position)))
                                   .Where(_ => _.Item1);

            var releases = trackedBody.SkipUntil(grips)
                                      .Where(_ => _.HandRightState != HandState.Closed)
                                      .Select(_ => this.IsOver<Rectangle>(this.MapToWindowPoint(_.Joints[JointType.HandRight].Position)))
                                      .Where(_ => _.Item1);

            return trackedBody.SkipUntil(grips)
                              .SkipUntil(releases)
                              .Take(1)
                              .Repeat()
                              .Subscribe(b =>
                              {
                                  var pos = this.MapToWindowPoint(b.Joints[JointType.HandRight].Position);
                                  var elem = this.IsOver<Rectangle>(pos);
                              
                                  if (elem.Item1)
                                      this.Background = elem.Item2.Fill;
                              });
        }

        private Brush GetRandomBrush()
        {
            var brushesType = typeof(Brushes);
            var prop = brushesType.GetProperties()[DateTime.Now.Second];
            return (Brush)brushesType.InvokeMember(prop.Name, System.Reflection.BindingFlags.GetProperty, null, null, null);
        }

        private Tuple<bool, T> IsOver<T>(Point windowPoint) where T : FrameworkElement
        {
            var elem = this.InputHitTest(windowPoint) as T;
            return Tuple.Create(elem != null, elem);
        }

        private Point MapToWindowPoint(CameraSpacePoint cameraSpacePoint)
        {
            return new Point
            {
                X = cameraSpacePoint.X * (float)this.ActualWidth,
                Y = (1 - cameraSpacePoint.Y) * (float)this.ActualHeight
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void MainWindowClosing(object sender, EventArgs args)
        {
            this.kinectSensor.Close();
            this.sceneChanges.Dispose();
        }
    }
}