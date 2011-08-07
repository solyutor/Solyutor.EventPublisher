using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher
{
    public interface IAssignee : IListenerSource
    {
        void Subscribe(object listener);

        void Unsubscribe(object listener);
    }
}
