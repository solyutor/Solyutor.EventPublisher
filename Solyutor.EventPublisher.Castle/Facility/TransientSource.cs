using System.Collections.Generic;
using Castle.MicroKernel;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public class TransientSource : IHandlerSource
    {
        private readonly IKernel _kernel;

        public TransientSource(IKernel kernel)
        {
            _kernel = kernel;
        }

        public virtual IEnumerable<IHandler<TMessage>> ResolveListenersFor<TMessage>()
        {
            return _kernel.ResolveAll<ITransientHandler<TMessage>>();
        }
    }
}