namespace Solyutor.EventPublisher
{
    public interface ISubscriber<TMessage>
    {
        void Receive(TMessage message);
    }
}
