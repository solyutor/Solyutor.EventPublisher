using System;
using NUnit.Framework;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Tests.Impl
{
    [TestFixture]
    public class PublisherTest
    {
        [Test]
        public void Publish_will_send_the_message_to_listner()
        {
            var assignee = new SimpleAssignee();
            var publishWay = new SimpleDispatcher();
            var handler = new TestHandler();
            var message = new TestMessage();
            var publisher = new Publisher(assignee, publishWay);

            assignee.Subscribe(handler);

            publisher.Publish(message);

            Assert.That(handler.TestMessage, Is.SameAs(message));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contstructor_will_throw_if_publishway_is_null()
        {
            new Publisher(new SimpleAssignee(), null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contstructor_will_throw_if_listnersource_is_null()
        {
            new Publisher(null, new SimpleDispatcher());
        }
    }
}