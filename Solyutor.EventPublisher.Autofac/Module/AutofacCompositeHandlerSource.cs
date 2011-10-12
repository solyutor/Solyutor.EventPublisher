using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Autofac.Module
{
    public class AutofacCompositeHandlerSource : CompositeHandlerSource
    {
        private readonly IComponentContext _componentContext;
        private bool _initialized;

        public AutofacCompositeHandlerSource(IComponentContext componentContext)
        {
            _componentContext = componentContext;
            if(componentContext == null)
                throw new ArgumentNullException("componentContext");
        }

        public override IEnumerable<IHandler<TMessage>> ResolveHandlersFor<TMessage>()
        {
            InitializeIfNeeded();
            
            return base.ResolveHandlersFor<TMessage>();
        }

        private void InitializeIfNeeded()
        {
            if (_initialized) return;
            foreach (var handlerSource in _componentContext.Resolve<IEnumerable<IHandlerSource>>().Where(source => source != this))
            {
                AddSource(handlerSource);
            }
            _initialized = true;
        }
    }
}