using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Kinect.ReactiveV2
{
    public static class BodyObservableExtensions
    {
        /// <summary>
        /// Selects the bodies from the body stream.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <returns>An observable sequence of bodies.</returns>
        public static IObservable<Body[]> SelectBodies(this IObservable<BodyFrameArrivedEventArgs> source, Body[] bodies)
        {
            if (source == null) throw new ArgumentNullException("source");

            return source.Select(_ =>
            {
                using (var frame = _.FrameReference.AcquireFrame())
                {
                    if (frame == null) return bodies;

                    frame.GetAndRefreshBodyData(bodies);

                    return bodies;
                }
            });
        }

        /// <summary>
        /// Selects the tracked bodies from the body stream. This observable produces only values if there is at least one tracked body.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <returns>An observable sequence of tracked bodies.</returns>
        public static IObservable<IEnumerable<Body>> SelectTracked(this IObservable<Body[]> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            return source.Select(bodies => bodies.Where(b => b.IsTracked))
                         .Where(bodies => bodies.Any());
        }

        /// <summary>
        /// Selects the JointType of the person from the bodies collection.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <param name="trackingId">The persons tracking id.</param>
        /// <param name="jointType">The type of joint to be tracked.</param>
        /// <param name="wherePredicate">An optional predicate to filter. Default value is (joint => joint.TrackingState != TrackingState.NotTracked).</param>
        /// <returns>An observable sequence of bodies.</returns>
        public static IObservable<Joint> SelectJointOf(this IObservable<Body[]> source, ulong trackingId, JointType jointType, Func<Joint, bool> wherePredicate = null)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (wherePredicate == null) wherePredicate = j => j.TrackingState != TrackingState.NotTracked;

            return source.Select(bodies =>
            {
                var body = bodies.FirstOrDefault(b => b.TrackingId == trackingId);
                if (body != null)
                {
                    return body.Joints[jointType];
                }
                else
                {
                    return new Joint { TrackingState = TrackingState.NotTracked };
                }
            }).Where(wherePredicate);
        }

        /// <summary>
        /// Selects the JointType from the bodies collection.
        /// </summary>
        /// <param name="source">The source observable.</param>
        /// <returns>An observable sequence of joints.</returns>
        public static IObservable<IEnumerable<Joint>> SelectJoints(this IObservable<Body[]> source, JointType jointType)
        {
            if (source == null) throw new ArgumentNullException("source");

            return source.Select(bodies => bodies.Where(b => b.IsTracked)
                                                 .Select(b => b.Joints[jointType])
                                                 .Where(j => j.TrackingState != TrackingState.NotTracked));
        }
    }
}