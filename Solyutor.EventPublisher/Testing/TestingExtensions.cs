namespace Solyutor.EventPublisher.Testing
{
    public static class TestingExtensions
    {
        public static TestHandler<TMessage> CreateTestHandlerFor<TMessage>(this TestPublisher self)
        {
            var result = new TestHandler<TMessage>();

            self.Assignee.Subscribe(result);

            return result;
        }
    }
}