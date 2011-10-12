using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Autofac.Module
{
    public class AutofacCompositeHandlerSourceSource : CompositeHandlerSource
    {
        private readonly IComponentContext _componentContext;
        private bool _initialized;

        public AutofacCompositeHandlerSourceSource(IComponentContext componentContext)
        {
            _componentContext = componentContext;
            if(componentContext == null)
                throw new ArgumentNullException("componentContext");
        }

        public override IEnumerable<IHandler<TMessage>> ResolveListenersFor<TMessage>()
        {
            InitializeIfNeeded();
            
            return base.ResolveListenersFor<TMessage>();
        }

        private void InitializeIfNeeded()
        {
            if (_initialized) return;
            foreach (var listenerSource in _componentContext.Resolve<IEnumerable<IHandlerSource>>().Where(source => source != this))
            {
                AddSource(listenerSource);
            }
            _initialized = true;
        }
    }
}