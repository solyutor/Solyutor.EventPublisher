using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Castle.Facility;
using Solyutor.EventPublisher.Castle.Tests.Impl;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Castle.Tests
{
    [TestFixture]
    public class PublisherFacilityTest
    {
        [Test]
        public void Facility_after_init_register_all_needed_services()
        {
            var windsor = new WindsorContainer();
            windsor.Register(Component.For<IPublishWay>().ImplementedBy<SimplePublishWay>());

            windsor.AddFacility<PublisherFacility>();

            var listener = new TestListener();


            windsor.Resolve<IAssignee>().Subscribe(listener);
            windsor.Register(Component.For<TestTransientListener>());


            var publisher = windsor.Resolve<IPublisher>();
            var message = new Message();
            
            publisher.Publish(message);

            message.Satisfy(m =>
                            m == listener.Message &&
                            m == TestTransientListener.StaticMessage);

        }
    }
}