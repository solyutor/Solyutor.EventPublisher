using System.Collections.Generic;
using NUnit.Framework;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Tests.Impl
{
    [TestFixture]
    public class SimpleAssigneeTest
    {
        [Test]
        public void ResolveListenersFor_should_return_empty_collection_if_no_listners()
        {
            var handlers = new SimpleAssignee().ResolveHandlersFor<TestMessage>();

            Assert.That(handlers, Is.Empty);
        }

        [Test]
        public void Subscribe_adds_it_to_the_source_so_it_can_be_resolved()
        {
            var assignee = new SimpleAssignee();
            var handler = new TestHandler();
            assignee.Subscribe(handler);

            var handlers = assignee.ResolveHandlersFor<TestMessage>();

            Assert.That(handlers, Has.Member(handler));
        }

        [Test]
        public void Subscribe_will_not_add_same_object_twice()
        {
            var assignee = new SimpleAssignee();
            var handler = new TestHandler();
            assignee.Subscribe(handler);

            var handlers = assignee.ResolveHandlersFor<TestMessage>();

            Assert.That(handlers, Has.Member(handler));
        }

        [Test]
        public void Unsubscribe_removes_it_from_source_so_it_could_be_resolved()
        {
            var assignee = new SimpleAssignee();
            var handler = new TestHandler();
            assignee.Subscribe(handler);
            assignee.Subscribe(handler);
            
            var handlers = new List<IHandler<TestMessage>>(assignee.ResolveHandlersFor<TestMessage>());

            Assert.That(handlers.Count, Is.EqualTo(1));
        }

        [Test]
        public void Unsubscribe_if_not_subscribed_will_be_skipped_silently()
        {
            new SimpleAssignee().Unsubscribe(new TestHandler());
        }
    }

    public class TestMessage
    {
    }

    public class TestHandler : IHandler<TestMessage>
    {
        public TestMessage TestMessage;

        #region IHandler<TestMessage> Members

        public void Handle(TestMessage message)
        {
            TestMessage = message;
        }

        #endregion
    }
}