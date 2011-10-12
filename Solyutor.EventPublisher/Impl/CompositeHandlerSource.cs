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

        public CompositeHandlerSource(IEnumerable<IHandlerSource> handlerSources) : this()
        {
            foreach (var handlerSource in handlerSources)
            {
                AddSource(handlerSource);
            }
        }

        #region IHandlerSource Members

        public virtual IEnumerable<IHandler<TMessage>> ResolveHandlersFor<TMessage>()
        {
            var result = new HashSet<IHandler<TMessage>>();
            foreach (var handlerSource in _sources)
            {
                foreach (var handler in handlerSource.ResolveHandlersFor<TMessage>())
                {
                    result.Add(handler);
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