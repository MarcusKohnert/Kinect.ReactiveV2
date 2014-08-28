#if NETFX_CORE
using WindowsPreview.Kinect.Input;
#else
using Microsoft.Kinect.Input;
#endif
using Microsoft.Kinect.Toolkit.Input;
using System;
using System.Reactive.Linq;

namespace Kinect.ReactiveV2.Input
{
    public static class ManipulatableModelExtensions
    {
        public static IObservable<KinectManipulationStartedEventArgs> ManipulationStartedObservable(this ManipulatableModel manipulatableModel)
        {
            return GetObservable<KinectManipulationStartedEventArgs>(manipulatableModel, "ManipulationStarted");
        }

        public static IObservable<KinectManipulationInertiaStartingEventArgs> ManipulationInertiaStartingObservable(this ManipulatableModel manipulatableModel)
        {
            return GetObservable<KinectManipulationInertiaStartingEventArgs>(manipulatableModel, "ManipulationInertiaStarting");
        }

        public static IObservable<KinectManipulationUpdatedEventArgs> ManipulationUpdatedObservable(this ManipulatableModel manipulatableModel)
        {
            return GetObservable<KinectManipulationUpdatedEventArgs>(manipulatableModel, "ManipulationUpdated");
        }

        public static IObservable<KinectManipulationCompletedEventArgs> ManipulationCompletedObservable(this ManipulatableModel manipulatableModel)
        {
            return GetObservable<KinectManipulationCompletedEventArgs>(manipulatableModel, "ManipulationCompleted");
        }

        private static IObservable<T> GetObservable<T>(ManipulatableModel model, string eventName)
        {
            return Observable.FromEventPattern<T>(model, eventName)
                             .Select(_ => _.EventArgs);
        }
    }
}