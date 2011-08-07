using System;
using System.Collections.Generic;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Solyutor.EventPublisher.Castle.Impl;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Castle.Facility
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
            ResolveOfCreateAssignee();

            Kernel.Register(
                Component.For<IPublisher>().UsingFactoryMethod(CreatePublisher));
        }

        private IPublishWay ResolvePublishWay()
        {
            IPublishWay result = Kernel.HasComponent(typeof (IPublishWay)) ? Kernel.Resolve<IPublishWay>() : _publishWay;

            if (result == null)
                throw new InvalidOperationException(
                    "Publisher facility needs an instance of IPublishWay. Supply it using PublishFacility constructor or register it with container.");

            return result;
        }

        private void ResolveOfCreateAssignee()
        {
            if (Kernel.HasComponent(typeof (IAssignee))) return;
            Kernel.Register(
                Component.For<IAssignee, IListenerSource>().Instance(new SimpleAssignee()));
        }

        public IPublisher CreatePublisher()
        {
            var publishWay = ResolvePublishWay();

            var sources = new List<IListenerSource>(Kernel.ResolveAll<IListenerSource>()) {new TransientSource(Kernel)};

            var compositeSource = new CompositeListenerSource(sources);

            return new Publisher(compositeSource, publishWay);
        }
    }
}