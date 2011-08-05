using System.Collections.Generic;
namespace Solyutor.EventPublisher.Impl
{
    interface IListenerSource
    {
        IEnumerable<TMessage> ResolveListenersFor<TMessage>();
    }
}
