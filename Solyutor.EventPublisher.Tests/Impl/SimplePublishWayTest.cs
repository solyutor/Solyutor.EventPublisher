using NUnit.Framework;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Tests.Impl
{
    [TestFixture]
    public class SimplePublishWayTest
    {
        [Test]
        public void Publish_calls_the_method_on_subscriber()
        {
            var simplePublishWay = new SimplePublishWay();
            var message = new TestMessage();
            var listener = new TestListener();

            simplePublishWay.Publish(message, listener);

            Assert.That(listener.TestMessage, Is.SameAs(message));
        }
    }
}