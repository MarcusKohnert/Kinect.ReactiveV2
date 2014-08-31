using Kinect.ReactiveV2.Input;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Xaml.Controls;
using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PressableControllerWinStore
{
    public class PressableController : IKinectPressableController, IDisposable
    {
        private WeakReference element;
        private KinectRegion kinectRegion;
        private PressableModel inputModel;
        private CompositeDisposable eventSubscriptions;

        public PressableController(FrameworkElement element, PressableModel model, KinectRegion kinectRegion)
        {
            this.element = new WeakReference(element);
            this.kinectRegion = kinectRegion;
            this.inputModel = model;

            if (this.inputModel == null)
                return;

            this.eventSubscriptions = new CompositeDisposable 
            {
                this.inputModel.PressingStartedObservable()
                               .Subscribe(_ => VisualStateManager.GoToState(this.Control, "Focused", true)),

                this.inputModel.HoldingObservable()
                               .Subscribe(_ => Debug.WriteLine(string.Format("Holding: {0}, ", DateTime.Now))),

                this.inputModel.PressingUpdatedObservable()
                               .Subscribe(_ => Debug.WriteLine(string.Format("PressingUpdated: {0}, ", DateTime.Now))),

                this.inputModel.PressingCompletedObservable()
                               .Subscribe(_ => VisualStateManager.GoToState(this.Control, "Unfocused", true)),

                this.inputModel.TappedObservable()
                               .Subscribe(_ => Debug.WriteLine(string.Format("Tapped: {0}, ", DateTime.Now))),
            };
        }

        public PressableModel PressableInputModel
        {
            get { return this.inputModel; }
        }

        public FrameworkElement Element
        {
            get { return (FrameworkElement)this.element.Target; }
        }

        internal Control Control { get { return (Control)this.Element; } }

        public void Dispose()
        {
            this.eventSubscriptions.Dispose();
        }
    }
}