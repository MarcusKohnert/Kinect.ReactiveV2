#if NETFX_CORE
using WindowsPreview.Kinect;
#else
    using Microsoft.Kinect;
#endif

using System;
using System.Reactive.Linq;

namespace Kinect.ReactiveV2
{
    public static class DepthObservableExtensions
    {
        /// <summary>
        /// Selects the depth data from the DepthFrame stream.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <returns>An observable sequence of depthData.</returns>
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