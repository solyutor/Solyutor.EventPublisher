using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Testing
{
    public class TestPublisher : Publisher
    {
        public IAssignee Assignee { get { return (IAssignee) HandlerSource; } }
        
        public TestPublisher() : base(new SimpleAssignee(), new SimplePublishWay())
        {
            
        }
    }
}