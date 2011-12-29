using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Impl;
using Solyutor.EventPublisher.Windsor;
using Solyutor.EventPublisher.Windsor.Facility;

namespace Solyutor.EventPublisher.Castle.Tests.Facility
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

            TestFacility(windsor);
        }

        [Test]
        public void Facility_after_init_with_publishway_register_all_needed_services()
        {
            var windsor = new WindsorContainer();
            windsor.AddFacility("PublisherFacility", new PublisherFacility(new SimplePublishWay()));

            TestFacility(windsor);
        }

        [Test]
        public void Facility_after_init_with_registered_assignee_register_all_needed_services()
        {
            var windsor = new WindsorContainer();
            windsor.Register(Component.For<IAssignee>().ImplementedBy<SimpleAssignee>());
            windsor.AddFacility("PublisherFacility", new PublisherFacility(new SimplePublishWay()));

            TestFacility(windsor);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Facility_throws_if_no_publishway_were_supplied()
        {
            var windsor = new WindsorContainer();
            windsor.AddFacility("PublisherFacility", new PublisherFacility());

            var publisher = windsor.Resolve<IPublisher>();
        }

        private static void TestFacility(WindsorContainer windsor)
        {
            var handler = new TestHandler();

            windsor.Resolve<IAssignee>().Subscribe(handler);
            windsor.Register(Component.For<ITransientHandler<Message>>().ImplementedBy<TestTransientHandler>());

            var publisher = windsor.Resolve<IPublisher>();
            var message = new Message();

            publisher.Publish(message);

            message.Satisfy(m =>
                            m == handler.Message &&
                            m == TestTransientHandler.StaticMessage);
        }
    }
}