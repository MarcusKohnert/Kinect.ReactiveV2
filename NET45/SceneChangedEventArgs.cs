using System;

namespace Kinect.ReactiveV2
{
    public class SceneChangedEventArgs : EventArgs
    {
        public SceneChangedEventArgs(SceneChangedType type)
        {
            this.SceneChangedType = type;
        }

        public SceneChangedType SceneChangedType { get; private set; }
    }
}