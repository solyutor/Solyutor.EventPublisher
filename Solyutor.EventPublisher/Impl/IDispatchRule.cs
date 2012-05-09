namespace Solyutor.EventPublisher.Impl
{
    public interface IDispatchRule
    {
        bool CanDispatch(object message, object handler);
    }
}