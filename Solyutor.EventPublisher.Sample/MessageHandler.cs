namespace Solyutor.EventPublisher.Sample
{
    public class MessageHandler : IHandler<HelloMessage>
    {
        private readonly IHellowStrategy _hellowStrategy;

        public MessageHandler(IHellowStrategy hellowStrategy)
        {
            _hellowStrategy = hellowStrategy;
        }

        public void Handle(HelloMessage message)
        {
            _hellowStrategy.SayHello(string.Format("Hello, {0}!", message.Content));
        }
    }
}