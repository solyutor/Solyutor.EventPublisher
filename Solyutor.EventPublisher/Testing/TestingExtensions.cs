namespace Solyutor.EventPublisher.Testing
{
    public static class TestingExtensions
    {
        public static TestListener<TMessage> CreateTestHandlerFor<TMessage>(this TestPublisher self)
        {
            var result = new TestListener<TMessage>();

            self.Assignee.Subscribe(result);

            return result;
        }
    }
}