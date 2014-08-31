#if NETFX_CORE
using WindowsPreview.Kinect.Input;
#else
using Microsoft.Kinect.Input;
#endif
using Microsoft.Kinect.Toolkit.Input;
using System;
using System.Reactive.Linq;
using System.Reactive;

namespace Kinect.ReactiveV2.Input
{
    public static class PressableModelExtensions
    {
        public static IObservable<KinectPressingStartedEventArgs> PressingStartedObservable(this PressableModel pressableModel)
        {
            return GetObservable<KinectPressingStartedEventArgs>(pressableModel, "PressingStarted");
        }

        public static IObservable<KinectHoldingEventArgs> HoldingObservable(this PressableModel pressableModel)
        {
            return GetObservable<KinectHoldingEventArgs>(pressableModel, "Holding");
        }

        public static IObservable<KinectPressingUpdatedEventArgs> PressingUpdatedObservable(this PressableModel pressableModel)
        {
            return GetObservable<KinectPressingUpdatedEventArgs>(pressableModel, "PressingUpdated");
        }

        public static IObservable<KinectPressingCompletedEventArgs> PressingCompletedObservable(this PressableModel pressableModel)
        {
            return GetObservable<KinectPressingCompletedEventArgs>(pressableModel, "PressingCompleted");
        }

        public static IObservable<KinectTappedEventArgs> TappedObservable(this PressableModel pressableModel)
        {
            return GetObservable<KinectTappedEventArgs>(pressableModel, "Tapped");
        }

        private static IObservable<T> GetObservable<T>(PressableModel model, string eventName)
        {
            return ModelObservable.GetObservable<PressableModel, T>(model, eventName);
        }
    }
}