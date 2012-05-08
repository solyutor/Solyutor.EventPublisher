using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public class PublisherFacility : AbstractFacility
    {
        protected override void Init()
        {
            AddHandlersResolver();
            RegisterHandlerSources();
            RegisterDispatcher();
            RegisterPublisher();
        }

        private void AddHandlersResolver()
        {
            Kernel.Resolver.AddSubResolver(new HandlerSourceResolver(Kernel));
        }

        protected virtual void RegisterHandlerSources()
        {
            Kernel.Register(
                Component
                    .For<CompositeHandlerSource, IHandlerSource>(),
                Component
                    .For<IHandlerSource>()
                    .ImplementedBy<TransientSource>(),
                Component
                    .For<IAssignee, IHandlerSource>()
                    .ImplementedBy<SimpleAssignee>());
        }

        protected virtual void RegisterDispatcher()
        {
            Kernel.Register(
                Component
                .For<IDispatcher>()
                .ImplementedBy<SimpleDispatcher>());
        }

        protected virtual void RegisterPublisher()
        {
            Kernel.Register(
                Component
                    .For<IPublisher>()
                    .ImplementedBy<Publisher>());
        }
    }
}