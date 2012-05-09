namespace Solyutor.EventPublisher
{
    /// <summary>
    /// Can subscribe and unsubscribe handlers.
    /// </summary>
    public interface IAssignee
    {
        /// <summary>
        /// Subscribes handler to events.
        /// </summary>
        /// <param name="handler">Object to subscribe.
        /// <remarks>If handler already subscribed impelement <see cref="IHandler{TMessage}"/> it will be silently ignored.</remarks>
        /// </param>
        void Subscribe(object handler);

        /// <summary>
        /// Unsubscribes handler from events. 
        /// </summary>
        /// <param name="handler">Object to unsubscirbed
        /// <remarks>If handler already unsubscribed it will be silently ignored.</remarks>
        /// </param>
        void Unsubscribe(object handler);
    }
}
