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
            var handler = new TestHandler();

            simplePublishWay.Publish(message, handler);

            Assert.That(handler.TestMessage, Is.SameAs(message));
        }
    }
}