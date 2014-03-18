using Microsoft.Kinect;
using System;
using System.Reactive.Linq;

namespace Kinect.ReactiveV2
{
    public static class DepthObservableExtensions
    {
        /// <summary>
        /// Selects the bodies from the body stream.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <returns>An observable sequence of skeletons.</returns>
        public static IObservable<ushort[]> SelectDepthData(this IObservable<DepthFrameArrivedEventArgs> source, ushort[] frameData)
        {
            if (source == null) throw new ArgumentNullException("source");

            return source.Select(_ =>
            {
                using (var frame = _.FrameReference.AcquireFrame())
                {
                    if (frame == null) return frameData;

                    frame.CopyFrameDataToArray(frameData);

                    return frameData;
                }
            });
        }
    }
}