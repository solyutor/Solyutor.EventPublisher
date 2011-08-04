namespace Solyutor.EventPublisher
{
    public interface IPublisher
    {
        void Publish<TMessage>(TMessage message);
    }
}
