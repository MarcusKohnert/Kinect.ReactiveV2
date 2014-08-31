using System;
using System.Reactive.Linq;

namespace Kinect.ReactiveV2.Input
{
    public static class ModelObservable
    {
        public static IObservable<TObs> GetObservable<T, TObs>(T model, string eventName)
        {
            return Observable.FromEventPattern<TObs>(model, eventName)
                             .Select(_ => _.EventArgs);
        }
    }
}