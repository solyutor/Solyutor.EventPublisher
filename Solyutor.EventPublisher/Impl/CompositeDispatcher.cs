using System;
using System.Collections.Generic;

namespace Solyutor.EventPublisher.Impl
{
    public class CompositeDispatcher : IDispatcher
    {
        private readonly IEnumerable<ISubdispatcher> _subdispatchers;

        public CompositeDispatcher(IEnumerable<ISubdispatcher> subdispatchers)
        {
            if(subdispatchers == null)
                throw new ArgumentNullException("subdispatchers");

            _subdispatchers = subdispatchers;
        }

        public void Invoke<TMessage>(TMessage message, IHandler<TMessage> handler)
        {
            foreach (var subdispatcher in _subdispatchers)
            {
                if(subdispatcher.TryInvoke(message, handler)) break;
            }
        }
    }
}