using System.Linq;
using NUnit.Framework;
using Solyutor.EventPublisher.Testing;

namespace Solyutor.EventPublisher.Tests.Testing
{
    [TestFixture]
    public class TestingExtensionsFixture
    {
        [Test]
        public void CreateTestHandlerFor_WhenCalled_CreatesTestHandlerAndAddsItsToPublisher()
        {
            var publisher = new TestPublisher();

            var handler = publisher.CreateTestHandlerFor<FooMessage>();


            Assert.That(handler, Is.InstanceOf<TestListener<FooMessage>>());
            Assert.That(publisher.Assignee.ResolveListenersFor<FooMessage>().First(), Is.SameAs(handler));
        }
    }

    public class FooMessage
    {
    }
}