#if NETFX_CORE
    using WindowsPreview.Kinect;
#else
using Microsoft.Kinect;
#endif

using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kinect.ReactiveV2
{
    public static class AudioBeamObservableExtensions
    {
        /// <summary>
        /// Selects the data from the body index stream.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <returns>An observable sequence of bodyIndex data.</returns>
        public static IObservable<IReadOnlyList<AudioBeamSubFrame>> SelectAudioBeamFrames(this IObservable<AudioBeamFrameArrivedEventArgs> source)
        {
            if (source == null) throw new ArgumentNullException("source");
            var subFramesLengthInBytes = KinectSensor.GetDefault()
                                                     .AudioSource
                                                     .SubFrameLengthInBytes;

            return source.Select(_ =>
            {
                var frame = _.FrameReference.AcquireBeamFrames();

                if (frame == null) return new ReadOnlyCollection<AudioBeamSubFrame>(new List<AudioBeamSubFrame>());

                var disposableFrame = frame as IDisposable;

                var data = frame[0].SubFrames;

                if (frame != null)
                    disposableFrame.Dispose();

                return data;
            });
        }
    }
}