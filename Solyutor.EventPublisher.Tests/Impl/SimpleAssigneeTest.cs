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
            IEnumerable<IListener<TestMessage>> listeners = new SimpleAssignee().ResolveListenersFor<TestMessage>();

            Assert.That(listeners, Is.Empty);
        }

        [Test]
        public void Subscribe_adds_it_to_the_source_so_it_can_be_resolved()
        {
            var assignee = new SimpleAssignee();
            var listener = new TestListener();
            assignee.Subscribe(listener);

            IEnumerable<IListener<TestMessage>> listeners = assignee.ResolveListenersFor<TestMessage>();

            Assert.That(listeners, Has.Member(listener));
        }

        [Test]
        public void Unsubscribe_removes_it_from_source_so_it_could_be_resolved()
        {
            var assignee = new SimpleAssignee();
            var listener = new TestListener();
            assignee.Subscribe(listener);
            assignee.Unsubscribe(listener);
            IEnumerable<IListener<TestMessage>> listeners = assignee.ResolveListenersFor<TestMessage>();

            Assert.That(listeners, Has.No.Member(listener));
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