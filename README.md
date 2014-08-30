Kinect.ReactiveV2
=================

This project contains extension methods to ease the use of the <a href="http://www.microsoft.com/en-us/kinectforwindows/">Kinect for Windows V2 SDK</a> with the help of <a href="http://rx.codeplex.com/">Rx</a>.

“This is preliminary software and/or hardware and APIs are preliminary and subject to change”

##Examples
###SceneChanges
```C#
using System;
using System.Linq;
using System.Reactive;
using Microsoft.Kinect;
using Kinect.ReactiveV2;

var sensor = KinectSensor.Default;
sensor.Open();

sensor.SceneChanges()
      .Subscribe(_ =>
      {
            if (_.SceneChangedType is PersonEnteredScene)
            {
                  Console.WriteLine("Person {0} entered scene", _.SceneChangedType.TrackingId);
            }
            else if (_.SceneChangedType is PersonLeftScene)
            {
                  Console.WriteLine("Person {0} left scene", _.SceneChangedType.TrackingId);
            }
      });
```

###BodyIndexFrameArrivedObservable
```C#
using System.Linq;
using System.Reactive;
using Microsoft.Kinect;
using Kinect.ReactiveV2;

var sensor = KinectSensor.Default;
sensor.Open();

var bodyIndexFrameDescription = sensor.BodyIndexFrameSource.FrameDescription;
var bodyIndexData = new byte[bodyIndexFrameDescription.LengthInPixels];

sensor.BodyIndexFrameArrivedObservable()
      .SelectBodyIndexData(bodyIndexData)
      .Subscribe(data => someBitmap.WritePixels(rect, data, stride, 0));

```
