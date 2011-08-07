using System;
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
            IAssignee assignee = ResolveOfCreateAssignee();
            IPublishWay publishWay = ResolvePublishWay();


            var compositeSource =
                new CompositeListenerSource(new[] {(IListenerSource) assignee, new TransientSource(Kernel)});

            Kernel.Register(
                Component.For<IPublisher>().UsingFactoryMethod(kernel =>
                                                               new Publisher(compositeSource, publishWay)));
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
                    Component.For<IAssignee>().Instance(assignee));
            }
            return assignee;
        }
    }
}