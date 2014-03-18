namespace Kinect.ReactiveV2
{
    public abstract class SceneChangedType
    {
        public ulong TrackingId { get; protected set; }
    }

    public class PersonEnteredScene : SceneChangedType
    {
        public PersonEnteredScene(ulong trackingId)
        {
            this.TrackingId = trackingId;
        } 
    }

    public class PersonLeftScene : SceneChangedType
    {
        public PersonLeftScene(ulong trackingId)
        {
            this.TrackingId = trackingId;
        } 
    }
}