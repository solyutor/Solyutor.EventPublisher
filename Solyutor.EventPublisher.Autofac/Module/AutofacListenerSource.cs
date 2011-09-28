using System;
using System.Collections.Generic;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Autofac.Module
{
    public class AutofacListenerSource : IListenerSource
    {
        public IEnumerable<IListener<TMessage>> ResolveListenersFor<TMessage>()
        {
            throw new NotImplementedException();
        }
    }
}