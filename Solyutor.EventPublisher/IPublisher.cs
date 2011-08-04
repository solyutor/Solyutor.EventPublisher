namespace Solyutor.EventPublisher
{
    public interface IPublisher
    {
        void Publish(object message);
    }
}
