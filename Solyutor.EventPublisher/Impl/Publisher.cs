using System;

namespace Solyutor.EventPublisher.Impl
{
    public class Publisher : IPublisher
    {
        protected readonly IListenerSource ListenerSource;
        protected readonly IPublishWay PublishWay;

        public Publisher(IListenerSource listenerSource, IPublishWay publishWay)
        {
            if(listenerSource == null) 
                throw new ArgumentNullException("listenerSource");
            if(publishWay == null)
                throw new ArgumentNullException("publishWay");

            ListenerSource = listenerSource;
            PublishWay = publishWay;
        }

        public virtual void Publish<TMessage>(TMessage message)
        {
            foreach (var listener in ListenerSource.ResolveListenersFor<TMessage>())
            {
                PublishWay.Publish(message, listener);
            }
        }
    }
}