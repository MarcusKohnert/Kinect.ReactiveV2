using Microsoft.Kinect;
using System;
using System.Reactive.Linq;

namespace Kinect.ReactiveV2
{
    public static class BodyIndexObservableExtensions
    {
        /// <summary>
        /// Selects the data from the body index stream.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <returns>An observable sequence of skeletons.</returns>
        public static IObservable<byte[]> SelectBodyIndexData(this IObservable<BodyIndexFrameArrivedEventArgs> source, byte[] frameData)
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