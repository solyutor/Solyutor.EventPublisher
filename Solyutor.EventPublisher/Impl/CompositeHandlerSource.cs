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
            var copyOfsources = GetCopyOfSources();

            return ResolveHandlersFromSources<TMessage>(copyOfsources);
        }

        private static IEnumerable<IHandler<TMessage>> ResolveHandlersFromSources<TMessage>(IEnumerable<IHandlerSource> sources)
        {
            var result = new HashSet<IHandler<TMessage>>();
            foreach (var handlerSource in sources)
            {
                result.UnionWith(handlerSource.ResolveHandlersFor<TMessage>());
            }
            return result;
        }

        private IEnumerable<IHandlerSource> GetCopyOfSources()
        {
            //will return a copy of contained sources for the sake of thread safety. 
            lock (_sources)
            {
                var copyOfsources = new IHandlerSource[_sources.Count];
                _sources.CopyTo(copyOfsources, 0);
                return copyOfsources;
            }
        }

        #endregion

        public virtual void AddSource(IHandlerSource handlerSource)
        {
            lock (_sources)
            {
                _sources.Add(handlerSource);
            }
            
        }

        public virtual void RemoveSource(IHandlerSource handlerSource)
        {
            lock (_sources)
            {
                _sources.Remove(handlerSource); 
            }
        }
    }
}