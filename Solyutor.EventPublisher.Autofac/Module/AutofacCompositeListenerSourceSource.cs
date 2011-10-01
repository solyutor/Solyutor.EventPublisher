﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Autofac.Module
{
    public class AutofacCompositeListenerSourceSource : CompositeListenerSource
    {
        private readonly IComponentContext _componentContext;

        public AutofacCompositeListenerSourceSource(IComponentContext componentContext)
        {
            _componentContext = componentContext;
            if(componentContext == null)
                throw new ArgumentNullException("componentContext");
        }

        public override IEnumerable<IListener<TMessage>> ResolveListenersFor<TMessage>()
        {
            InitializeIfNeeded();
            
            return base.ResolveListenersFor<TMessage>();
        }

        private void InitializeIfNeeded()
        {
            foreach (var listenerSource in _componentContext.Resolve<IEnumerable<IListenerSource>>().Where(source => source != this))
            {
                AddSource(listenerSource);
            }
        }
    }
}