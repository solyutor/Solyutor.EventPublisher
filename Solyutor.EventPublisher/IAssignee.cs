using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher
{
    public interface IAssignee : IHandlerSource
    {
        void Subscribe(object handler);

        void Unsubscribe(object handler);
    }
}
