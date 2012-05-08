using System;
using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Windsor;
using Solyutor.EventPublisher.Windsor.Facility;

namespace Solyutor.EventPublisher.Castle.Tests.Facility
{
    [TestFixture]
    public class PublisherFacilityTest
    {
        private WindsorContainer _windsor;

        [SetUp]
        public void SetUp()
        {
            _windsor = new WindsorContainer();
            _windsor.AddFacility<PublisherFacility>();
        }

        [Test]
        public void Facility_after_init_register_all_needed_services()
        {
            Assert_facility_configured_to_send_message_to_transient_and_non_transient_handlers();
        }

        [Test]
        public void Facility_after_init_with_publishway_register_all_needed_services()
        {
            Assert_facility_configured_to_send_message_to_transient_and_non_transient_handlers();
        }

        [Test]
        public void Facility_after_init_with_registered_assignee_register_all_needed_services()
        {

            Assert_facility_configured_to_send_message_to_transient_and_non_transient_handlers();
        }

        [Test]
        public void Facility_throws_if_no_publishway_were_supplied()
        {
            //Assert.DoesNotThrow(() => _windsor.Resolve<IPublisher>());
        }

        private void Assert_facility_configured_to_send_message_to_transient_and_non_transient_handlers()
        {
            var handler = new TestHandler();

            _windsor.Resolve<IAssignee>().Subscribe(handler);
            _windsor.Register(Component.For<ITransientHandler<Message>>().ImplementedBy<TestTransientHandler>());

            var publisher = _windsor.Resolve<IPublisher>();
            var message = new Message();

            publisher.Publish(message);

            message.Satisfy(m =>
                            m == handler.Message &&
                            m == TestTransientHandler.StaticMessage);
        }
    }
}