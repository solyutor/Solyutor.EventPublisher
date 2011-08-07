using System.Threading;
using NUnit.Framework;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Tests.Impl
{
    [TestFixture]
    public class SynchronizationContextPublishWayTest
    {
        [Test]
        public void Publish_will_call_listner()
        {
            var listener = new TestListener();
            var message = new TestMessage();

            var publishWay = new SynchronizationContextPublishWay(new SynchronizationContext());

            publishWay.Publish(message, listener);

            Thread.Sleep(10); //Wait for async call

            Assert.That(listener.TestMessage, Is.SameAs(message));
        }
    }
}