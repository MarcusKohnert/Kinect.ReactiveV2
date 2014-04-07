using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Kinect.ReactiveV2
{
    public static class KinectSensorExtensions
    {
        /// <summary>
        /// Converts the BodyFrameArrived event to an observable sequence.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<BodyFrameArrivedEventArgs> BodyFrameArrivedObservable(this KinectSensor kinectSensor)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.Create<BodyFrameArrivedEventArgs>(observer =>
            {
                var reader = kinectSensor.BodyFrameSource.OpenReader();

                var disposable = kinectSensor.BodyFrameArrivedObservable(reader)
                                             .Subscribe(x => observer.OnNext(x),
                                                        e => observer.OnError(e),
                                                        () => observer.OnCompleted());

                return new CompositeDisposable { disposable, reader };
            });
        }

        /// <summary>
        /// Converts the BodyFrameArrived event to an observable sequence and uses the specified reader.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <param name="kinectSensor">The reader to be used to subscribe to the FrameArrived event.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<BodyFrameArrivedEventArgs> BodyFrameArrivedObservable(this KinectSensor kinectSensor, BodyFrameReader reader)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.FromEventPattern<BodyFrameArrivedEventArgs>(h => reader.FrameArrived += h,
                                                                          h => reader.FrameArrived -= h)
                             .Select(e => e.EventArgs);
        }

        /// <summary>
        /// Converts the BodyIndexFrameArrived event to an observable sequence.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<BodyIndexFrameArrivedEventArgs> BodyIndexFrameArrivedObservable(this KinectSensor kinectSensor)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.Create<BodyIndexFrameArrivedEventArgs>(observer =>
            {
                var reader = kinectSensor.BodyIndexFrameSource.OpenReader();

                var disposable = kinectSensor.BodyIndexFrameArrivedObservable(reader)
                                             .Subscribe(x => observer.OnNext(x),
                                                        e => observer.OnError(e),
                                                        () => observer.OnCompleted());

                return new CompositeDisposable { disposable, reader };
            });
        }

        /// <summary>
        /// Converts the BodyIndexFrameArrived event to an observable sequence and uses the specified reader.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <param name="kinectSensor">The reader to be used to subscribe to the FrameArrived event.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<BodyIndexFrameArrivedEventArgs> BodyIndexFrameArrivedObservable(this KinectSensor kinectSensor, BodyIndexFrameReader reader)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.FromEventPattern<BodyIndexFrameArrivedEventArgs>(h => reader.FrameArrived += h,
                                                                               h => reader.FrameArrived -= h)
                             .Select(e => e.EventArgs);
        }

        /// <summary>
        /// Converts the ColorFrameArrived event to an observable sequence.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<ColorFrameArrivedEventArgs> ColorFrameArrivedObservable(this KinectSensor kinectSensor)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.Create<ColorFrameArrivedEventArgs>(observer =>
            {
                var reader = kinectSensor.ColorFrameSource.OpenReader();

                var disposable = kinectSensor.ColorFrameArrivedObservable(reader)
                                             .Subscribe(x => observer.OnNext(x),
                                                        e => observer.OnError(e),
                                                        () => observer.OnCompleted());

                return new CompositeDisposable { disposable, reader };
            });
        }

        /// <summary>
        /// Converts the ColorFrameArrived event to an observable sequence and uses the specified reader.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <param name="kinectSensor">The reader to be used to subscribe to the FrameArrived event.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<ColorFrameArrivedEventArgs> ColorFrameArrivedObservable(this KinectSensor kinectSensor, ColorFrameReader reader)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.FromEventPattern<ColorFrameArrivedEventArgs>(h => reader.FrameArrived += h,
                                                                           h => reader.FrameArrived -= h)
                             .Select(e => e.EventArgs);
        }

        /// <summary>
        /// Converts the DepthFrameArrived event to an observable sequence.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<DepthFrameArrivedEventArgs> DepthFrameArrivedObservable(this KinectSensor kinectSensor)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.Create<DepthFrameArrivedEventArgs>(observer =>
            {
                var reader = kinectSensor.DepthFrameSource.OpenReader();

                var disposable = kinectSensor.DepthFrameArrivedObservable(reader)
                                             .Subscribe(x => observer.OnNext(x),
                                                        e => observer.OnError(e),
                                                        () => observer.OnCompleted());

                return new CompositeDisposable { disposable, reader };
            });
        }

        /// <summary>
        /// Converts the DepthFrameArrived event to an observable sequence and uses the specified reader.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <param name="kinectSensor">The reader to be used to subscribe to the FrameArrived event.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<DepthFrameArrivedEventArgs> DepthFrameArrivedObservable(this KinectSensor kinectSensor, DepthFrameReader reader)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.FromEventPattern<DepthFrameArrivedEventArgs>(h => reader.FrameArrived += h,
                                                                           h => reader.FrameArrived -= h)
                             .Select(e => e.EventArgs);
        }

        /// <summary>
        /// Converts the InfraredFrameArrived event to an observable sequence.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<InfraredFrameArrivedEventArgs> InfraredFrameArrivedObservable(this KinectSensor kinectSensor)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.Create<InfraredFrameArrivedEventArgs>(observer =>
            {
                var reader = kinectSensor.InfraredFrameSource.OpenReader();

                var disposable = kinectSensor.InfraredFrameArrivedObservable(reader)
                                             .Subscribe(x => observer.OnNext(x),
                                                        e => observer.OnError(e),
                                                        () => observer.OnCompleted());

                return new CompositeDisposable { disposable, reader };
            });
        }

        /// <summary>
        /// Converts the InfraredFrameArrived event to an observable sequence and uses the specified reader.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <param name="kinectSensor">The reader to be used to subscribe to the FrameArrived event.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<InfraredFrameArrivedEventArgs> InfraredFrameArrivedObservable(this KinectSensor kinectSensor, InfraredFrameReader reader)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.FromEventPattern<InfraredFrameArrivedEventArgs>(h => reader.FrameArrived += h,
                                                                              h => reader.FrameArrived -= h)
                             .Select(e => e.EventArgs);
        }

        /// <summary>
        /// Converts the MultiSourceFrameArrived event to an observable sequence.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <param name="types">The sources to include in the MultiSourceFrameReader.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<MultiSourceFrameArrivedEventArgs> MultiSourceFrameArrivedObservable(this KinectSensor kinectSensor, FrameSourceTypes types = FrameSourceTypes.None)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");
            if (types == FrameSourceTypes.None) types = FrameSourceTypes.Body | FrameSourceTypes.Color | FrameSourceTypes.Depth;

            return Observable.Create<MultiSourceFrameArrivedEventArgs>(observer =>
            {
                var reader = kinectSensor.OpenMultiSourceFrameReader(types);

                var disposable = kinectSensor.MultiSourceFrameArrivedObservable(reader)
                                             .Subscribe(x => observer.OnNext(x),
                                                        e => observer.OnError(e),
                                                        () => observer.OnCompleted());

                return new CompositeDisposable { disposable, reader };
            });
        }

        /// <summary>
        /// Converts the MultiSourceFrameArrived event to an observable sequence.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <param name="kinectSensor">The reader to be used to subscribe to the MultiSourceFrameArrived event.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<MultiSourceFrameArrivedEventArgs> MultiSourceFrameArrivedObservable(this KinectSensor kinectSensor, MultiSourceFrameReader reader)
        {
            if (kinectSensor == null) throw new ArgumentNullException("kinectSensor");

            return Observable.FromEventPattern<MultiSourceFrameArrivedEventArgs>(h => reader.MultiSourceFrameArrived += h,
                                                                                 h => reader.MultiSourceFrameArrived -= h)
                             .Select(e => e.EventArgs);
        }

        /// <summary>
        /// Returns an observable that calls on next when persons enter the scene or leave the scene.
        /// </summary>
        /// <param name="kinectSensor">The kinect sensor.</param>
        /// <returns>The observable sequence.</returns>
        public static IObservable<SceneChangedEventArgs> SceneChanges(this KinectSensor kinectSensor)
        {
            var bodiesInScene = new Dictionary<ulong, Body>(kinectSensor.BodyFrameSource.BodyCount);
            var bodies = new Body[kinectSensor.BodyFrameSource.BodyCount];

            return Observable.Create<SceneChangedEventArgs>(observer =>
            {
                return kinectSensor.BodyFrameArrivedObservable()
                                   .SelectBodies(bodies)
                                   .Subscribe(bs =>
                                   {
                                       bs.Where(b => b != null && b.IsTracked)
                                         .Where(b => bodiesInScene.ContainsKey(b.TrackingId) == false) // were not yet in scene
                                         .ForEach(b =>
                                         {
                                             bodiesInScene.Add(b.TrackingId, b);
                                             observer.OnNext(new SceneChangedEventArgs(new PersonEnteredScene(b.TrackingId)));
                                         });

                                       var toRemove = bodiesInScene.Keys
                                                    .Except(bs.Select(b => b.TrackingId))
                                                    .ToList();

                                       toRemove.ForEach(_ =>
                                       {
                                           bodiesInScene.Remove(_);
                                           observer.OnNext(new SceneChangedEventArgs(new PersonLeftScene(_)));
                                       });
                                   },
                                   e => observer.OnError(e),
                                   () => observer.OnCompleted());
            });
        }
    }
}