using System.Collections.Generic;
using System.Linq;

namespace Solyutor.EventPublisher.Impl
{
    public class SimpleAssignee : IAssignee
    {
        private readonly ISet<object> _handlers;
        private readonly object _latch;

        public SimpleAssignee()
        {
            _handlers = new HashSet<object>();
            _latch = new object();
        }

        public virtual void Subscribe(object handler)
        { 
            lock(_latch)
            {
                _handlers.Add(handler);
            }
        }

        public virtual void Unsubscribe(object handler)
        {
            lock (_latch)
            {
                _handlers.Remove(handler);
            }
        }

        public virtual IEnumerable<IHandler<TMessage>> ResolveHandlersFor<TMessage>()
        {
            lock(_latch)
            {
                return _handlers.OfType<IHandler<TMessage>>().ToList();
            }
        }
    }
}