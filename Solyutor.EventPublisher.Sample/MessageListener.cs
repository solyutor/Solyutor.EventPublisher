using Solyutor.EventPublisher.Castle;

namespace Solyutor.EventPublisher.Sample
{
    public class MessageListener : ITransientListener<HelloMessage>
    {
        private readonly IHellowStrategy _hellowStrategy;

        public MessageListener(IHellowStrategy hellowStrategy)
        {
            _hellowStrategy = hellowStrategy;
        }

        public void ListenTo(HelloMessage message)
        {
            _hellowStrategy.SayHello(string.Format("Hello, {0}!", message.Content));
        }
    }
}