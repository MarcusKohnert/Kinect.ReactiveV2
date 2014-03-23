using Microsoft.Kinect;
using System;
using System.Reactive.Linq;

namespace Kinect.ReactiveV2
{
    public static class ColorObservableExtensions
    {
        /// <summary>
        /// Selects the color data from the color stream.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <returns>An observable sequence of colorData.</returns>
        public static IObservable<byte[]> SelectColorData(this IObservable<ColorFrameArrivedEventArgs> source, byte[] frameData)
        {
            if (source == null) throw new ArgumentNullException("source");

            return source.Select(_ =>
            {
                using (var frame = _.FrameReference.AcquireFrame())
                {
                    if (frame == null) return frameData;

                    frame.CopyRawFrameDataToArray(frameData);

                    return frameData;
                }
            });
        }
    }
}