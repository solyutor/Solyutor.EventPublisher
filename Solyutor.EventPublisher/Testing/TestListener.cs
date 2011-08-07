using System.Collections.Generic;
using System.Linq;

namespace Solyutor.EventPublisher.Testing
{
    public class TestListener<TMessage> : IListener<TMessage>
    {
        private readonly List<TMessage> _messages;

        public TestListener()
        {
            _messages = new List<TMessage>();
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

        public void ListenTo(TMessage message)
        {
            WasCalled++;
            _messages.Add(message);
        }
    }
}