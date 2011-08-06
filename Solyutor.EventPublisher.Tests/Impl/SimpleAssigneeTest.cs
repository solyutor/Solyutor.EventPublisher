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
            var listeners = new SimpleAssignee().ResolveListenersFor<TestMessage>();

            Assert.That(listeners, Is.Empty);
        }

        [Test]
        public void Subscribe_adds_it_to_the_source_so_it_can_be_resolved()
        {
            var assignee = new SimpleAssignee();
            var listener = new TestListener();
            assignee.Subscribe(listener);

            var listeners = assignee.ResolveListenersFor<TestMessage>();

            Assert.That(listeners, Has.Member(listener));
        }

        [Test]
        public void Subscribe_will_not_add_same_object_twice()
        {
            var assignee = new SimpleAssignee();
            var listener = new TestListener();
            assignee.Subscribe(listener);

            var listeners = assignee.ResolveListenersFor<TestMessage>();

            Assert.That(listeners, Has.Member(listener));
        }

        [Test]
        public void Unsubscribe_removes_it_from_source_so_it_could_be_resolved()
        {
            var assignee = new SimpleAssignee();
            var listener = new TestListener();
            assignee.Subscribe(listener);
            assignee.Subscribe(listener);
            
            var listeners = new List<IListener<TestMessage>>(assignee.ResolveListenersFor<TestMessage>());

            Assert.That(listeners.Count, Is.EqualTo(1));
        }

        [Test]
        public void Unsubscribe_if_not_subscribed_will_be_skipped_silently()
        {
            new SimpleAssignee().Unsubscribe(new TestListener());
        }
    }

    public class TestMessage
    {
    }

    public class TestListener : IListener<TestMessage>
    {
        public TestMessage TestMessage;

        #region IListener<TestMessage> Members

        public void ListenTo(TestMessage message)
        {
            TestMessage = message;
        }

        #endregion
    }
}