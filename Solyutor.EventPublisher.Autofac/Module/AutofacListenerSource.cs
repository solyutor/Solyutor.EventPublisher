using System;
using System.Collections.Generic;
using Autofac;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Autofac.Module
{
    public class AutofacListenerSource : IListenerSource
    {
        private readonly IComponentContext _componentContext;

        public AutofacListenerSource(IComponentContext componentContext)
        {
            _componentContext = componentContext;
            if(componentContext == null)
                throw  new ArgumentNullException("componentContext");
        }

        public IEnumerable<IHandler<TMessage>> ResolveListenersFor<TMessage>()
        {
            return _componentContext.Resolve<IEnumerable<IHandler<TMessage>>>();
        }
    }
}