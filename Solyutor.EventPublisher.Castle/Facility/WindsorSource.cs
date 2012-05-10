using System.Collections.Generic;
using Castle.MicroKernel;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public class WindsorSource : IHandlerSource
    {
        private readonly IKernel _kernel;

        public WindsorSource(IKernel kernel)
        {
            _kernel = kernel;
        }

        public virtual IEnumerable<IHandler<TMessage>> ResolveHandlersFor<TMessage>()
        {
            return _kernel.ResolveAll<IHandler<TMessage>>();
        }
    }
}