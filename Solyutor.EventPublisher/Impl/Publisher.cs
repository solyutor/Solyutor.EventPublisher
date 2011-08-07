using System;

namespace Solyutor.EventPublisher.Impl
{
    public class Publisher : IPublisher
    {
        private readonly IListenerSource _listenerSource;
        private readonly IPublishWay _publishWay;

        public Publisher(IListenerSource listenerSource, IPublishWay publishWay)
        {
            if(listenerSource == null) 
                throw new ArgumentNullException("listenerSource");
            if(publishWay == null)
                throw new ArgumentNullException("publishWay");

            _listenerSource = listenerSource;
            _publishWay = publishWay;
        }

        public virtual void Publish<TMessage>(TMessage message)
        {
            foreach (var listener in _listenerSource.ResolveListenersFor<TMessage>())
            {
                _publishWay.Publish(message, listener);
            }
        }
    }
}