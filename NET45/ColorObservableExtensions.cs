#if NETFX_CORE
using WindowsPreview.Kinect;
#else
    using Microsoft.Kinect;
#endif

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
        public static IObservable<byte[]> SelectColorData(this IObservable<ColorFrameArrivedEventArgs> source, byte[] frameData, ColorImageFormat colorImageFormat = ColorImageFormat.Bgra)
        {
            if (source == null) throw new ArgumentNullException("source");

            return source.Select(_ =>
            {
                using (var frame = _.FrameReference.AcquireFrame())
                {
                    if (frame == null) return frameData;

                    if (frame.RawColorImageFormat == ColorImageFormat.Bgra)
                    {
                        frame.CopyRawFrameDataToArray(frameData);
                    }
                    else
                    {
                        frame.CopyConvertedFrameDataToArray(frameData, colorImageFormat);
                    }

                    return frameData;
                }
            });
        }
    }
}