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
                Component.For<IPublisher>().UsingFactoryMethod(CreatePublisher));
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
                Component.For<IAssignee, IListenerSource>().Instance(new SimpleAssignee()));
        }

        protected virtual IPublisher CreatePublisher()
        {
            var publishWay = ResolvePublishWay();

            var sources = new List<IListenerSource>(Kernel.ResolveAll<IListenerSource>()) {new TransientSource(Kernel)};

            var compositeSource = new CompositeListenerSource(sources);

            return new Publisher(compositeSource, publishWay);
        }
    }
}