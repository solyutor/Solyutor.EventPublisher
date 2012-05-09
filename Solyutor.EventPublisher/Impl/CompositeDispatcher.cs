using System;
using System.Collections.Generic;

namespace Solyutor.EventPublisher.Impl
{
    public class CompositeDispatcher : IDispatcher
    {
        private const string ErrorTemplate = 
            "For the message {0} of type {1} and handler {2} of type {3} was not found appropriate subdispatcher. "+
            "Please configure subdispatcheres to be able to perform invocation.";
        
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
                if(subdispatcher.TryInvoke(message, handler)) return;
            }

            var errorMessage = string.Format(ErrorTemplate, message, message.GetType(), handler, handler.GetType());
            throw new InvalidOperationException(errorMessage);
        }
    }
}