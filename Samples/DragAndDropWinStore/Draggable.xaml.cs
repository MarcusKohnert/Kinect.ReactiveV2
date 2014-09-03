using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Xaml.Controls;
using Windows.UI.Xaml.Controls;

namespace DragAndDropWinStore
{
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
}