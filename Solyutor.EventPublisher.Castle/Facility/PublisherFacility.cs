using System;
using System.Collections.Generic;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public class PublisherFacility : AbstractFacility
    {
        private readonly IPublishWay _publishWay;

        public PublisherFacility()
        {
        }

        public PublisherFacility(IPublishWay publishWay)
        {
            _publishWay = publishWay;
        }

        protected override void Init()
        {
            RegisterAssignee();

            Kernel.Register(
                Component.For<IPublisher>()
                .UsingFactoryMethod(CreatePublisher));
        }

        protected virtual IPublishWay ResolvePublishWay()
        {
            IPublishWay result = Kernel.HasComponent(typeof (IPublishWay)) ? Kernel.Resolve<IPublishWay>() : _publishWay;

            if (result == null)
                throw new InvalidOperationException(
                    "Publisher facility needs an instance of IPublishWay. Supply it using PublishFacility constructor or register it with container.");

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