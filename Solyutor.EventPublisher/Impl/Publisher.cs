using System;

namespace Solyutor.EventPublisher.Impl
{
    public class Publisher : IPublisher
    {
        protected readonly IHandlerSource HandlerSource;
        protected readonly IDispatcher Dispatcher;

        public Publisher(IHandlerSource handlerSource, IDispatcher dispatcher)
        {
            if(handlerSource == null) 
                throw new ArgumentNullException("handlerSource");
            if(dispatcher == null)
                throw new ArgumentNullException("dispatcher");

            HandlerSource = handlerSource;
            Dispatcher = dispatcher;
        }

        public virtual void Publish<TMessage>(TMessage message)
        {
            foreach (var handler in HandlerSource.ResolveHandlersFor<TMessage>())
            {
                Dispatcher.Invoke(message, handler);
            }
        }
    }
}