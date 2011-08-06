using System.Collections.Generic;
namespace Solyutor.EventPublisher.Impl
{
    public interface IListenerSource
    {
        IEnumerable<IListener<TMessage>> ResolveListenersFor<TMessage>();
    }
}
