namespace Solyutor.EventPublisher
{
    public interface IHandler<TMessage>
    {
        void Handle(TMessage message);
    }
}
