using System.Collections.Generic;
namespace Solyutor.EventPublisher.Impl
{
    public interface IHandlerSource
    {
        IEnumerable<IHandler<TMessage>> ResolveListenersFor<TMessage>();
    }
}
