using Microsoft.Kinect.Face;
using System;
using System.Reactive.Linq;

namespace Face.NET45
{
    public static class FaceFrameObservableExtensions
    {
        public static IObservable<FaceFrameResult> SelectFaceFrame(this IObservable<FaceFrameArrivedEventArgs> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            return source.Select(_ =>
            {
                using (var frame = _.FrameReference.AcquireFrame())
                {
                    if (frame == null) return null;

                    return frame.FaceFrameResult;
                }
            })
            .Where(_ => _ != null);
        }
    }
}