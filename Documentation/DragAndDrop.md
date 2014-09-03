### Drag and Drop sample - IKinectManipulateableController

This sample shows how you can implement a custom KinectControl for example to move things around in a <code>Canvas</code>.

To hook into the whole <code>KinectRegion</code> magic you can implement your own <code>UserControl</code> that implements <code>IKinectControl</code>.
When you move your hand <code>KinectRegion</code> keeps track of the movements and will constantly check if there is some <i>Kinect enabled control</i> at the current hand pointer's position.
To determine such a control the <code>KinectRegion</code> looks for <code>IKinectControl</code>s.

The <code>IKinectControl</code> interface forces you to implement the method <code>IKinectController CreateController(IInput inputModel, KinectRegion kinectRegion)</code>.
The <code>IKinectControl</code> is also required to implement <code>IsManipulatable</code> and <code>IsPressable</code>.
This way you are specifying on what gestures your control will react to.
Because this sample shows how to move controls around the <code>Draggable</code> control's property <code>IsManipulatable</code> returns <code>true</code>.

#### UserControl Draggable.cs implements IKinectControl
```c#
public sealed partial class Draggable : UserControl, IKinectControl
{
    public Draggable()
    {
        this.InitializeComponent();
    }

    public IKinectController CreateController(IInputModel inputModel, KinectRegion kinectRegion)
    {
        // Only one controller is instantiated for one Control
        var model = new ManipulatableModel(inputModel.GestureRecognizer.GestureSettings, this);
        return new DragAndDropController(this, model, kinectRegion);
    }

    public bool IsManipulatable { get { return true; } }

    public bool IsPressable { get { return false; } }
}
```

You'll find two interfaces in the SDK *.Controls namespace that implement <code>IKinectController</code> and of course they are closely related to the other properties you have to implement.

Those interfaces are:
- IKinectPressableController
- IKinectManipulateableController

Since <code>IsManipulatable</code> returns true CreateController should return an instance of <code>IKinectManipulateableController</code>.
For an example how to implement IKinectPressableController see [here](https://github.com/MarcusKohnert/Kinect.ReactiveV2/tree/master/Samples/PressableControllerWinStore).

####  Controller class DragAndDropController
```c#
public class DragAndDropController : IKinectManipulatableController, IDisposable
{
}
```

The <code>DragAndDropController</code>'s constructor gets a reference to the manipulatable control, an <code>IInputModel</code> and a reference to the <code>KinectRegion</code>.

```c#
public DragAndDropController(FrameworkElement element, ManipulatableModel model, KinectRegion kinectRegion)
{
    this.element = new WeakReference(element);
    this.kinectRegion = kinectRegion;
    this.inputModel = model;

    if (this.inputModel == null)
        return;
...
```

The <code>ManipulatableModel</code> provides four events you can subscribe to in order to react to user input.
This sample uses the nuget package [Kinect.ReactiveV2.Input](https://www.nuget.org/packages/Kinect.ReactiveV2.Input/) that provides specific [Rx](http://rx.codeplex.com/)-extension methods to subscribe to these events.

```c#
...
    this.eventSubscriptions = new CompositeDisposable 
    {
        this.inputModel.ManipulationStartedObservable()
                       .Subscribe(_ => VisualStateManager.GoToState(this.Control, "Focused", true)),

        this.inputModel.ManipulationInertiaStartingObservable()
                       .Subscribe(_ => Debug.WriteLine(string.Format("ManipulationInertiaStarting: {0}, ", DateTime.Now))),

        this.inputModel.ManipulationUpdatedObservable()
                       .Subscribe(_ => OnManipulationUpdated(_)),

        this.inputModel.ManipulationCompletedObservable()
                       .Subscribe(_ => VisualStateManager.GoToState(this.Control, "Unfocused", true)),
    };
}
```

All subscriptions are composed into one <code>CompositeDisposable</code> that is disposed in the controllers's Dispose() method.

+ ManipulationStartedObservable --> is fired when the user closes it's hand
+ ManipulationInertiaStartingObservable --> ???
+ ManipulationUpdatedObservable --> is fired when the user moves it's hand while keeping it closed
+ ManipulationCompletedObservable --> is fired when the user releases it's hand

For this sample the most interesting observable is ManipulationUpdatedObservable. Everytime this event is fired the method OnManipulationUpdated is called.

```c#
private void OnManipulationUpdated(KinectManipulationUpdatedEventArgs args)
{
    var dragableElement = this.Element;
    if (!(dragableElement.Parent is Canvas)) return;

    var delta = args.Delta.Translation;
    var translationPoint = new Point(delta.X, delta.Y);
    var translatedPoint = InputPointerManager.TransformInputPointerCoordinatesToWindowCoordinates(translationPoint, this.kinectRegion.Bounds);

    var offsetY = Canvas.GetTop(dragableElement);
    var offsetX = Canvas.GetLeft(dragableElement);

    if (double.IsNaN(offsetY)) offsetY = 0;
    if (double.IsNaN(offsetX)) offsetX = 0;

    Canvas.SetTop(dragableElement, offsetY + translatedPoint.Y);
    Canvas.SetLeft(dragableElement, offsetX + translatedPoint.X);
}
```

If this <code>IKinectControl</code> is placed inside a <code>Canvas</code> it's position is updated relative to the users hand.

The complete code is found [here](https://github.com/MarcusKohnert/Kinect.ReactiveV2/tree/master/Samples/DragAndDropWinStore).