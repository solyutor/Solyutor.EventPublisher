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
            var handler = new TestListener<int>();

            handler.ListenTo(10);

            handler.Satisfy(h => 
                h.WasCalled == 1 &&
                h.WasCalledOnce &&
                h.WasCalledAtLeastOnce  &&
                h.LastMessage == 10);
        }
        
        [Test]
        public void Handle_CalledTwice_HasRightCorrectProperties()
        {
            var handler = new TestListener<int>();

            handler.ListenTo(10);
            handler.ListenTo(15);

            handler.Satisfy(h =>
                h.WasCalled == 2 &&
                h.WasCalledAtLeastOnce &&
                h.WasCalledOnce == false &&
                h.Messages[0] == 10 &&
                h.Messages[1] == 15 &&
                h.LastMessage == 15);
        }
    }
}