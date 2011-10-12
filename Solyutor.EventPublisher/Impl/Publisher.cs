using System;

namespace Solyutor.EventPublisher.Impl
{
    public class Publisher : IPublisher
    {
        protected readonly IHandlerSource HandlerSource;
        protected readonly IPublishWay PublishWay;

        public Publisher(IHandlerSource handlerSource, IPublishWay publishWay)
        {
            if(handlerSource == null) 
                throw new ArgumentNullException("handlerSource");
            if(publishWay == null)
                throw new ArgumentNullException("publishWay");

            HandlerSource = handlerSource;
            PublishWay = publishWay;
        }

        public virtual void Publish<TMessage>(TMessage message)
        {
            foreach (var listener in HandlerSource.ResolveListenersFor<TMessage>())
            {
                PublishWay.Publish(message, listener);
            }
        }
    }
}