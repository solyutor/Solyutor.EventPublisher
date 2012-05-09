using NUnit.Framework;
using Solyutor.EventPublisher.Impl;
using Solyutor.EventPublisher.Testing;

namespace Solyutor.EventPublisher.Tests.Impl
{
    [TestFixture]
    public class CompositeDispatcherTests
    {
        public class DenySubdispatcher : ISubdispatcher
        {
            public bool TryInvoke<TMessage>(TMessage message, IHandler<TMessage> handler)
            {
                return false;
            }
        }

        public class SuccessfulDispatcher : ISubdispatcher
        {
            public bool TryInvoke<TMessage>(TMessage message, IHandler<TMessage> handler)
            {
                handler.Handle(message);
                return true;
            }
        }
        
        [Test]
        public void Invokes_until_appropriate_subdispatcher_found()
        {
            var denyDispatcher = new DenySubdispatcher();
            var successDispatcher = new SuccessfulDispatcher();

            var dispatcher = new CompositeDispatcher(new ISubdispatcher[] {denyDispatcher, successDispatcher});

            var testHandler = new TestHandler<TestMessage>();
            dispatcher.Invoke(new TestMessage(), testHandler);

            Assert.That(testHandler.WasCalledOnce, Is.True);
        }

        [Test]
        public void Invokation_should_not_be_perfomed_twice()
        {
            var successDispatcher1 = new SuccessfulDispatcher();
            var successDispatcher2 = new SuccessfulDispatcher();

            var dispatcher = new CompositeDispatcher(new ISubdispatcher[] { successDispatcher1, successDispatcher2 });

            var testHandler = new TestHandler<TestMessage>();
            dispatcher.Invoke(new TestMessage(), testHandler);

            Assert.That(testHandler.WasCalled, Is.EqualTo(1));
        }

        [Test]
        public void Should_throw_if_cannot_dispatch_message()
        {
            var dispatcher = new CompositeDispatcher(new[] {new DenySubdispatcher()});
        }
    }
}