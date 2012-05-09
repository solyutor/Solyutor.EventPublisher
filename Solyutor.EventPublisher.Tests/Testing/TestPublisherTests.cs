using System.Linq;
using NUnit.Framework;
using Solyutor.EventPublisher.Testing;

namespace Solyutor.EventPublisher.Tests.Testing
{
    [TestFixture]
    public class TestPublisherTests
    {
        public class FooMessage
        {
        }

        [Test]
        public void CreateTestHandlerFor_WhenCalled_CreatesTestHandlerAndAddsItsToPublisher()
        {
            var publisher = new TestPublisher();

            var handler = publisher.RegisterTestHandler<FooMessage>();

            Assert.That(handler, Is.InstanceOf<TestHandler<FooMessage>>());
            Assert.That(publisher.GetHandlerSource().ResolveHandlersFor<FooMessage>().First(), Is.SameAs(handler));
        }
    }
}