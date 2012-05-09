using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Testing
{
    public class TestPublisher : Publisher
    {
        public IAssignee GetAssignee()
        {
            return (IAssignee) HandlerSource;
        }

        public IHandlerSource GetHandlerSource()
        {
            return HandlerSource;
        }

        public TestPublisher() : base(new SimpleAssignee(), new SimpleDispatcher())
        {
            
        }

        public TestHandler<TMessage> RegisterTestHandler<TMessage>()
        {
            var result = new TestHandler<TMessage>();
            GetAssignee().Subscribe(result);
            return result;
        }
    }
}