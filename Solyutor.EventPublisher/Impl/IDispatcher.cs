namespace Solyutor.EventPublisher.Impl
{
    public interface IDispatcher
    {
        /// <summary>
        /// Try to invoke a handler with a message.
        /// </summary>
        /// <typeparam name="TMessage">The type of message.</typeparam>
        /// <param name="message">Message to be delivered to handler.</param>
        /// <param name="handler">Handler that should process the message.</param>
        /// <returns>Returns true if message was successfully dispatched to the handler. Returns false otherwise.</returns>
        bool Invoke<TMessage>(TMessage message, IHandler<TMessage> handler);
    }
}