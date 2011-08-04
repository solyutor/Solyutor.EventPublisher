namespace Solyutor.EventPublisher
{
    public interface IListener<TMessage>
    {
        void ListenTo(TMessage message);
    }
}
