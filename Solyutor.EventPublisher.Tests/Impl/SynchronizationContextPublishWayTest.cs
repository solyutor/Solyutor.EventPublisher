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
            AssertDispatchesCall(new SynchronizationContextDispatcher(new SynchronizationContext()));
        }

        [Test]
        public void Generic_publish_way_creates_synchronization_context()
        {
            AssertDispatchesCall(new SynchronizationContextDispatcher<SynchronizationContext>());
        }

        private static void AssertDispatchesCall(SynchronizationContextDispatcher dispatcher)
        {
            var handler = new TestHandler();
            var message = new TestMessage();

            dispatcher.Invoke(message, handler);

            handler.Called.WaitOne(1000);

            Assert.That(handler.TestMessage, Is.SameAs(message));
        }
    }
}