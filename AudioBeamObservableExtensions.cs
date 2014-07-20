#if NETFX_CORE
    using WindowsPreview.Kinect;
#else
using Microsoft.Kinect;
#endif

using System;
using System.Reactive.Linq;
using System.Collections.Generic;

namespace Kinect.ReactiveV2
{
    public static class AudioBeamObservableExtensions
    {
        /// <summary>
        /// Selects the data from the body index stream.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <returns>An observable sequence of bodyIndex data.</returns>
        public static IObservable<IReadOnlyList<AudioBeamFrame>> SelectAudioBeamFrames(this IObservable<AudioBeamFrameArrivedEventArgs> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            return source.Select(_ =>
            {
                using (var frame = _.FrameReference.AcquireBeamFrames())
                {
                    if (frame == null) return new List<AudioBeamFrame>().AsReadOnly();

                    return new List<AudioBeamFrame>(frame).AsReadOnly();
                }
            });
        }
    }
}