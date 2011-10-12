using System.Collections.Generic;

namespace Solyutor.EventPublisher.Impl
{
    public class CompositeHandlerSource : IHandlerSource
    {
        private readonly ISet<IHandlerSource> _sources;

        public CompositeHandlerSource()
        {
            _sources = new HashSet<IHandlerSource>();
        }

        public CompositeHandlerSource(IEnumerable<IHandlerSource> listenerSources) : this()
        {
            foreach (var listenerSource in listenerSources)
            {
                AddSource(listenerSource);
            }
        }

        #region IHandlerSource Members

        public virtual IEnumerable<IHandler<TMessage>> ResolveListenersFor<TMessage>()
        {
            var result = new HashSet<IHandler<TMessage>>();
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

        public virtual void AddSource(IHandlerSource handlerSource)
        {
            _sources.Add(handlerSource);
        }

        public virtual void RemoveSource(IHandlerSource handlerSource)
        {
            _sources.Remove(handlerSource);
        }
    }
}