using Kinect.ReactiveV2;
using Microsoft.Kinect;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace GripClicks
{
    class Program
    {
        static void Main(string[] args)
        {
            var kinect = KinectSensor.GetDefault();
            kinect.Open();

            Body[] bodies = null;

            var tracked = kinect.BodyFrameArrivedObservable()
                                .SelectBodies(bodies)
                                .SelectTracked();

            var grips = tracked.Where(_ => _.Any(b => b.HandRightState == HandState.Closed));

            var releases = tracked.SkipUntil(grips)
                                  .Where(_ => _.Any(b => b.HandRightState != HandState.Closed));

            var clicks = tracked.SkipUntil(grips)
                                .SkipUntil(releases)
                                .Take(1)
                                .Repeat()
                                .Subscribe(_ => Console.WriteLine("{0} clicked...", DateTime.Now));

            Console.WriteLine("[ENTER] to quit");
            Console.ReadLine();

            clicks.Dispose();
            kinect.Close();
        }
    }
}