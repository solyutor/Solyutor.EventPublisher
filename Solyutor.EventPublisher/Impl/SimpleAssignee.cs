using System;
using System.Collections.Generic;
using System.Linq;

namespace Solyutor.EventPublisher.Impl
{
    public class SimpleAssignee : IAssignee
    {
        private readonly object _latch;
        private readonly Dictionary<Type, ISet<object>> _storeHandlers;
        private readonly HashSet<object> _emptySet;

        public SimpleAssignee()
        {
            _storeHandlers = new Dictionary<Type, ISet<object>>();
            _emptySet = new HashSet<object>();
            _latch = new object();
        }

        public virtual void Subscribe(object handler)
        {
            var interfaces = GetImplementedHandlers(handler);

            if(interfaces.Length == 0) return;
  
            lock(_latch)
            {
                foreach (var @interface in interfaces)
                {
                    ISet<object> handlers;
                    if(_storeHandlers.TryGetValue(@interface, out handlers))
                    {
                        handlers.Add(handler);
                    }
                    else
                    {
                        _storeHandlers[@interface] = new HashSet<object> { handler };
                    }
                }
            }
        }

        public virtual void Unsubscribe(object handler)
        {
            var interfaces = GetImplementedHandlers(handler);
            if (interfaces.Length == 0) return;

            lock (_latch)
            {
                foreach (var @interface in interfaces)
                {
                    ISet<object> handlers;
                    if (_storeHandlers.TryGetValue(@interface, out handlers))
                    {
                        handlers.Remove(handler);
                    }
                }
            }
        }

        public virtual IEnumerable<IHandler<TMessage>> ResolveHandlersFor<TMessage>()
        {
            ISet<object> handlers;
            lock(_latch)
            {
                _storeHandlers.TryGetValue(typeof (IHandler<TMessage>), out handlers);
                handlers = new HashSet<object>(handlers ?? _emptySet);
            }
            return handlers.Cast<IHandler<TMessage>>();
        }

        private static Type[] GetImplementedHandlers(object handler)
        {
            var interfaces = handler.GetType().FindInterfaces(
                (@interface, nomatter) => @interface.IsGenericType &&
                                          @interface.GetGenericTypeDefinition() == typeof (IHandler<>), null);
            return interfaces;
        }
    }
}