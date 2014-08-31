using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Xaml.Controls;
using Windows.UI.Xaml.Controls;

namespace PressableControllerWinStore
{
    public sealed partial class CustomPressable : UserControl, IKinectControl
    {
        public CustomPressable()
        {
            this.InitializeComponent();
        }

        public IKinectController CreateController(IInputModel inputModel, KinectRegion kinectRegion)
        {
            return new PressableController(this, new PressableModel(inputModel.GestureRecognizer.GestureSettings, this), kinectRegion);
        }

        public bool IsManipulatable
        {
            get { return false; }
        }

        public bool IsPressable
        {
            get { return true; }
        }
    }
}