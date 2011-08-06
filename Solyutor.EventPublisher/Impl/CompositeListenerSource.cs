using System.Collections.Generic;

namespace Solyutor.EventPublisher.Impl
{
    public class CompositeListenerSource : IListenerSource
    {
        private readonly ISet<IListenerSource> _sources;

        public CompositeListenerSource()
        {
            _sources = new HashSet<IListenerSource>();
        }

        public CompositeListenerSource(IEnumerable<IListenerSource> listenerSources) : this()
        {
            foreach (var listenerSource in listenerSources)
            {
                AddSource(listenerSource);
            }
        }

        #region IListenerSource Members

        public virtual IEnumerable<IListener<TMessage>> ResolveListenersFor<TMessage>()
        {
            var result = new HashSet<IListener<TMessage>>();
            foreach (var listenerSource in _sources)
            {
                foreach (var listener in listenerSource.ResolveListenersFor<TMessage>())
                {
                    result.Add(listener);
                }
            }
            return result;
        }

        #endregion

        public virtual void AddSource(IListenerSource listenerSource)
        {
            _sources.Add(listenerSource);
        }

        public virtual void RemoveSource(IListenerSource listenerSource)
        {
            _sources.Remove(listenerSource);
        }
    }
}