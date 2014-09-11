using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DragAndDropWinStore
{
    public sealed partial class Draggable : UserControl, IKinectControl
    {
        public static readonly DependencyProperty DragContentProperty = DependencyProperty.Register("DragContent",
                                                                                                    typeof(object),
                                                                                                    typeof(Draggable),
                                                                                                    new PropertyMetadata(null));

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

        public object DragContent
        {
            get { return (object)GetValue(DragContentProperty); }
            set { SetValue(DragContentProperty, value); }
        }

        public bool IsManipulatable { get { return true; } }

        public bool IsPressable { get { return false; } }
    }
}