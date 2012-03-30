using System;
using System.Collections.Generic;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public class PublisherFacility : AbstractFacility
    {
        private readonly IDispatcher _dispatcher;

        public PublisherFacility()
        {
        }

        public PublisherFacility(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected override void Init()
        {
            RegisterAssignee();

            Kernel.Register(
                Component.For<IPublisher>()
                .UsingFactoryMethod(CreatePublisher));
        }

        protected virtual IDispatcher ResolvePublishWay()
        {
            IDispatcher result = Kernel.HasComponent(typeof (IDispatcher)) ? Kernel.Resolve<IDispatcher>() : _dispatcher;

            if (result == null)
                throw new InvalidOperationException(
                    "Publisher facility needs an instance of IDispatcher. Supply it using PublishFacility constructor or register it with container.");

            return result;
        }

        protected virtual void RegisterAssignee()
        {
            if (Kernel.HasComponent(typeof (IAssignee))) return;
            Kernel.Register(
                Component.For<IAssignee, IHandlerSource>()
                .Instance(new SimpleAssignee())
                .LifeStyle.Singleton);
        }

        protected virtual IPublisher CreatePublisher()
        {
            var publishWay = ResolvePublishWay();

            var sources = new List<IHandlerSource>(Kernel.ResolveAll<IHandlerSource>()) {new TransientSource(Kernel)};
            sources.AddRange(Kernel.ResolveAll<IAssignee>());

            var compositeSource = new CompositeHandlerSource(sources);

            return new Publisher(compositeSource, publishWay);
        }
    }
}