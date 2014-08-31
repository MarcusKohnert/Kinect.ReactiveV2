using Kinect.ReactiveV2.Input;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Xaml.Controls;
using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using Windows.Foundation;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using WindowsPreview.Kinect.Input;

namespace DragAndDropWinStore
{
    public class DragAndDropController : IKinectManipulatableController, IDisposable
    {
        private WeakReference element;
        private KinectRegion kinectRegion;
        private ManipulatableModel inputModel;
        private IDisposable eventSubscriptions;

        public DragAndDropController(FrameworkElement element, ManipulatableModel model, KinectRegion kinectRegion)
        {
            this.element = new WeakReference(element);
            this.kinectRegion = kinectRegion;
            this.inputModel = model;

            if (this.inputModel == null)
                return;

            this.eventSubscriptions = new CompositeDisposable 
            {
                this.inputModel.ManipulationStartedObservable()
                               .Subscribe(_ => VisualStateManager.GoToState(this.Control, "Focused", true)),

                this.inputModel.ManipulationInertiaStartingObservable()
                               .Subscribe(_ => Debug.WriteLine(string.Format("ManipulationInertiaStarting: {0}, ", DateTime.Now))),

                this.inputModel.ManipulationUpdatedObservable()
                               .Subscribe(_ => OnManipulationUpdated(_)),

                this.inputModel.ManipulationCompletedObservable()
                               .Subscribe(_ => VisualStateManager.GoToState(this.Control, "Unfocused", true)),
            };
        }

        private void OnManipulationUpdated(KinectManipulationUpdatedEventArgs args)
        {
            var dragableElement = this.Element;
            if (!(dragableElement.Parent is Canvas)) return;

            var delta = args.Delta.Translation;
            var translationPoint = new Point(delta.X, delta.Y);
            var translatedPoint = InputPointerManager.TransformInputPointerCoordinatesToWindowCoordinates(translationPoint, this.kinectRegion.Bounds);

            var offsetY = Canvas.GetTop(dragableElement);
            var offsetX = Canvas.GetLeft(dragableElement);

            if (double.IsNaN(offsetY)) offsetY = 0;
            if (double.IsNaN(offsetX)) offsetX = 0;

            Canvas.SetTop(dragableElement, offsetY + translatedPoint.Y);
            Canvas.SetLeft(dragableElement, offsetX + translatedPoint.X);
        }

        public ManipulatableModel ManipulatableInputModel
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