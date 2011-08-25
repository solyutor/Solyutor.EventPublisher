using System.Collections.Generic;
using System.Linq;

namespace Solyutor.EventPublisher.Impl
{
    public class SimpleAssignee : IAssignee
    {
        private readonly ISet<object> _listeners;
        private readonly object _latch;

        public SimpleAssignee()
        {
            _listeners = new HashSet<object>();
            _latch = new object();
        }

        public virtual void Subscribe(object listener)
        { 
            lock(_latch)
            {
                _listeners.Add(listener);
            }
        }

        public virtual void Unsubscribe(object listener)
        {
            lock (_latch)
            {
                _listeners.Remove(listener);
            }
        }

        public virtual IEnumerable<IListener<TMessage>> ResolveListenersFor<TMessage>()
        {
            lock(_latch)
            {
                return _listeners.OfType<IListener<TMessage>>().ToList();
            }
        }
    }
}