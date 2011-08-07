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
        private IAssignee _assignee;

        public PublisherFacility()
        {
        }

        public PublisherFacility(IPublishWay publishWay)
        {
            _publishWay = publishWay;
        }

        protected override void Init()
        {
            _assignee = ResolveOfCreateAssignee();

            Kernel.Register(
                Component.For<IPublisher>().UsingFactoryMethod(CreatePublisher));
        }

        private IPublishWay ResolvePublishWay()
        {
            IPublishWay result = null;
            if (Kernel.HasComponent(typeof (IPublishWay)))
            {
                result = Kernel.Resolve<IPublishWay>();
            }
            else
            {
                result = _publishWay;
            }

            if (result == null)
                throw new InvalidOperationException(
                    "Publisher facility needs an instance of IPublishWay. Supply it using PublishFacility constructor or register it in advance in the container.");

            return result;
        }

        private IAssignee ResolveOfCreateAssignee()
        {
            IAssignee assignee = null;

            if (Kernel.HasComponent(typeof (IAssignee)))
            {
                assignee = Kernel.Resolve<IAssignee>();
            }
            else
            {
                assignee = new SimpleAssignee();
                Kernel.Register(
                    Component.For<IAssignee, IListenerSource>().Instance(assignee));
            }
            return assignee;
        }

        public IPublisher CreatePublisher()
        {
            IPublishWay publishWay = ResolvePublishWay();

            var sources = new List<IListenerSource>(Kernel.ResolveAll<IListenerSource>()) {new TransientSource(Kernel)};

            var compositeSource = new CompositeListenerSource(sources);

            return new Publisher(compositeSource, publishWay);
        }
    }
}