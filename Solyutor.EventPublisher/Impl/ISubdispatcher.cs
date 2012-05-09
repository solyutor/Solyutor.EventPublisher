namespace Solyutor.EventPublisher.Impl
{
    public interface ISubdispatcher
    {
        /// <summary>
        /// Try to invoke a handler with a message.
        /// </summary>
        /// <typeparam name="TMessage">The type of message.</typeparam>
        /// <param name="message">Message to be delivered to handler.</param>
        /// <param name="handler">Handler that should process the message.</param>
        /// <returns>Returns true if message was successfully dispatched to the handler. Returns false otherwise.</returns>
        bool TryInvoke<TMessage>(TMessage message, IHandler<TMessage> handler); 
    }
}