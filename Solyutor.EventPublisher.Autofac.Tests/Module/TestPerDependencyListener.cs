using Solyutor.EventPublisher.Tests.Impl;

namespace Solyutor.EventPublisher.Autofac.Tests.Module
{
    public class TestPerDependencyListener : IPerDependencyListener<TestMessage>
    {
        private int _calledTimes;

        public void ListenTo(TestMessage message)
        {
            _calledTimes++;
        }

        protected int CalledTimes
        {
            get { return _calledTimes; }
        }
    }
}