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
            AssertDispatchesCall(new SynchronizationContextPublishWay(new SynchronizationContext()));
        }

        [Test]
        public void Generic_publish_way_creates_synchronization_context()
        {
            AssertDispatchesCall(new SynchronizationContextPublishWay<SynchronizationContext>());
        }

        private static void AssertDispatchesCall(SynchronizationContextPublishWay publishWay)
        {
            var listener = new TestHandler();
            var message = new TestMessage();

            publishWay.Publish(message, listener);

            Thread.Sleep(10); //Wait for async call

            Assert.That(listener.TestMessage, Is.SameAs(message));
        }
    }
}