namespace Solyutor.EventPublisher.Impl
{
    public interface ISubdispatcher
    {
        bool CanInvoke<TMessage>(TMessage message, IHandler<TMessage> handler); 

        void Invoke<TMessage>(TMessage message, IHandler<TMessage> handler); 
    }
}