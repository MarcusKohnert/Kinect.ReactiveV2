Kinect.ReactiveV2
=================

This project contains extension methods to ease the use of the Kinect V2 SDK with the help of Rx.

“This is preliminary software and/or hardware and APIs are preliminary and subject to change”

##Examples
###BodyIndexObservable
```C#
using System.Linq;
using System.Reactive;
using Kinect.ReactiveV2;

var sensor = KinectSensor.Default;
sensor.Open();

var bodyIndexFrameDescription = sensor.BodyIndexFrameSource.FrameDescription;
var bodyIndexData = new byte[bodyIndexFrameDescription.LengthInPixels];

sensor.BodyIndexFrameArrivedObservable()
      .SelectBodyIndexData(bodyIndexData)
      .Subscribe(data => someBitmap.WritePixels(rect, data, stride, 0));

```
