using System.Collections.Generic;
namespace Solyutor.EventPublisher.Impl
{
    public interface IListenerSource
    {
        IEnumerable<IHandler<TMessage>> ResolveListenersFor<TMessage>();
    }
}
