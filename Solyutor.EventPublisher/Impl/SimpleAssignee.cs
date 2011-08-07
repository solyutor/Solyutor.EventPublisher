using System.Collections.Generic;
using System.Linq;

namespace Solyutor.EventPublisher.Impl
{
    public class SimpleAssignee : IAssignee
    {
        private readonly ISet<object> _listeners;

        public SimpleAssignee()
        {
            _listeners = new HashSet<object>();
        }

        public virtual void Subscribe(object listener)
        { 
            _listeners.Add(listener);
        }

        public virtual void Unsubscribe(object listener)
        {
            _listeners.Remove(listener);
        }

        public virtual IEnumerable<IListener<TMessage>> ResolveListenersFor<TMessage>()
        {
            return _listeners.OfType<IListener<TMessage>>();
        }
    }
}