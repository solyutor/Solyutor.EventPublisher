namespace Solyutor.EventPublisher.Impl
{
    public abstract class RuleBasedSubdispatcher : IDispatcher
    {
        private readonly IDispatchRule[] _rules;

        protected RuleBasedSubdispatcher(params IDispatchRule[] rules)
        {
            _rules = rules;
        }

        public virtual bool Invoke<TMessage>(TMessage message, IHandler<TMessage> handler)
        {
            foreach(var rule in _rules)
            {
                if (!rule.CanDispatch(message, handler)) continue;
                
                IntervalInvoke(message, handler);
                
                return true;
            }
            return false;
        }

        protected abstract void IntervalInvoke<TMessage>(TMessage message, IHandler<TMessage> handler);
    }
}