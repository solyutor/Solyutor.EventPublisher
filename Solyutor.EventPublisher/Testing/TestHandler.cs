using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Solyutor.EventPublisher.Testing
{
    public class TestHandler<TMessage> : IHandler<TMessage>
    {
        
        private readonly List<TMessage> _messages;
        private AutoResetEvent _called;

        public void WaitUntilCalled()
        {
            if (_called.WaitOne(1000)) return;
            throw new InvalidOperationException(string.Format("TestHandler<{0}> was not called in a second.", typeof(TMessage)));
        }

        public TestHandler()
        {
            _messages = new List<TMessage>();
            _called = new AutoResetEvent(false);
        }

        public int WasCalled { get; private set; }

        public bool WasCalledAtLeastOnce
        {
            get { return WasCalled > 0; }
        }

        public TMessage LastMessage
        {
            get { return _messages.Last(); }
        }


        public IList<TMessage> Messages
        {
            get { return _messages.AsReadOnly(); }
        }

        public bool WasCalledOnce
        {
            get { return WasCalled == 1; }
        }

        public void Handle(TMessage message)
        {
            WasCalled++;
            _messages.Add(message);
            _called.Set();
        }
    }
}