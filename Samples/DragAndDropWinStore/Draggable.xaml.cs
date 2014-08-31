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
            return new DragAndDropController(this, new ManipulatableModel(inputModel.GestureRecognizer.GestureSettings, this), kinectRegion);
        }

        public bool IsManipulatable { get { return true; } }

        public bool IsPressable { get { return false; } }
    }
}