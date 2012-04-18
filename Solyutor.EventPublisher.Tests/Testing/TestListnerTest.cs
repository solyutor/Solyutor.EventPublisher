using System;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Testing;

namespace Solyutor.EventPublisher.Tests.Testing
{

    [TestFixture]
    public class TestListnerTest
    {
        [Test]
        public void Handle_CalledOnce_HasRightCorrectProperties()
        {
            var handler = new TestHandler<int>();

            handler.Handle(10);

            handler.Satisfy(h => 
                h.WasCalled == 1 &&
                h.WasCalledOnce &&
                h.WasCalledAtLeastOnce  &&
                h.LastMessage == 10);
        }
        
        [Test]
        public void Handle_CalledTwice_HasRightCorrectProperties()
        {
            var handler = new TestHandler<int>();

            handler.Handle(10);
            handler.Handle(15);

            handler.Satisfy(h =>
                h.WasCalled == 2 &&
                h.WasCalledAtLeastOnce &&
                h.WasCalledOnce == false &&
                h.Messages[0] == 10 &&
                h.Messages[1] == 15 &&
                h.LastMessage == 15);
        }

        [Test]
        public void Throws_if_no_call_to_handle_was_perfomed()
        {
            var handler = new TestHandler<int>();

            Assert.Throws<InvalidOperationException>(handler.WaitUntilCalled);
        }

        [Test]
        public void Skip_waiting_when_call_performed()
        {
            var handler = new TestHandler<int>();

            handler.Handle(10);

            Assert.DoesNotThrow(handler.WaitUntilCalled);
        }
    }
}