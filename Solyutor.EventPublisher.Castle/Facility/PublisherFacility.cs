using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Castle.Facility
{
    public class PublisherFacility : AbstractFacility
    {
        private readonly IPublishWay _publishWay;

        public PublisherFacility() : this(new SimplePublishWay())
        {
        }

        private PublisherFacility(IPublishWay publishWay)
        {
            _publishWay = publishWay;
        }

        protected override void Init()
        {
            var assignee = new SimpleAssignee();

            Kernel.Register(
                Component.For<IAssignee, IListenerSource>().Instance(assignee),
                Component.For<IPublisher>()
                    .UsingFactoryMethod(kernel =>
                                        new Publisher(assignee, _publishWay)));

        }
    }
}