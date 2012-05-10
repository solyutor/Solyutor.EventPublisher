using System;
using NUnit.Framework;
using Solyutor.EventPublisher.Impl;
using Solyutor.EventPublisher.Testing;

namespace Solyutor.EventPublisher.Tests.Impl
{
    [TestFixture]
    public class CompositeDispatcherTests
    {
        
        [Test]
        public void Invokes_until_appropriate_subdispatcher_found()
        {
            var denyDispatcher = new SimpleDispatcher(new Rule((message, handler) => false));
            var successDispatcher = new SimpleDispatcher(new Rule((message, handler) => true));

            var dispatcher = new CompositeDispatcher(new IDispatcher[] {denyDispatcher, successDispatcher});

            var testHandler = new TestHandler<TestMessage>();
            dispatcher.Invoke(new TestMessage(), testHandler);

            Assert.That(testHandler.WasCalledOnce, Is.True);
        }

        [Test]
        public void Invokation_should_not_be_perfomed_twice()
        {
            var successDispatcher1 = new SimpleDispatcher(new Rule((message, handler) => true));
            var successDispatcher2 = new SimpleDispatcher(new Rule((message, handler) => true));

            var dispatcher = new CompositeDispatcher(new IDispatcher[] { successDispatcher1, successDispatcher2 });

            var testHandler = new TestHandler<TestMessage>();
            dispatcher.Invoke(new TestMessage(), testHandler);

            Assert.That(testHandler.WasCalled, Is.EqualTo(1));
        }

        [Test]
        public void Should_throw_if_cannot_dispatch_message()
        {
            var dispatcher = new CompositeDispatcher(new[] {new SimpleDispatcher(new Rule((message, handler) => false))});

            Assert.Throws<InvalidOperationException>(delegate { dispatcher.Invoke(new TestMessage(), new TestHandler<TestMessage>()); });
        }
    }
}