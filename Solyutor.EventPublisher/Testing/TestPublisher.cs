using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Testing
{
    public class TestPublisher : Publisher
    {
        public IAssignee Assignee { get { return (IAssignee) HandlerSource; } }
        
        public TestPublisher() : base(new SimpleAssignee(), new SimpleDispatcher())
        {
            
        }

        public TestHandler<TMessage> RegisterTestHandler<TMessage>()
        {
            var result = new TestHandler<TMessage>();
            Assignee.Subscribe(result);
            return result;
        }
    }
}