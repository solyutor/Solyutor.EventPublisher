using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher
{
    public interface IAssignee : IHandlerSource
    {
        void Subscribe(object listener);

        void Unsubscribe(object listener);
    }
}
