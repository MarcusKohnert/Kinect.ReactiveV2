using Microsoft.Kinect.Face;
using System;
using System.Reactive.Linq;

namespace Face.NET45
{
    public static class FaceFrameReaderExtensions
    {
        public static IObservable<FaceFrameArrivedEventArgs> FaceFrameArrivedObservable(this FaceFrameReader reader)
        {
            return Observable.FromEventPattern<FaceFrameArrivedEventArgs>(h => reader.FrameArrived += h,
                                                                          h => reader.FrameArrived -= h)
                             .Select(_ => _.EventArgs);
        }
    }
}