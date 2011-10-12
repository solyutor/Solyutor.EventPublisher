using Solyutor.EventPublisher.Tests.Impl;

namespace Solyutor.EventPublisher.Autofac.Tests.Module
{
    public class TestPerDependencyHandler : IPerDependencyHandler<TestMessage>
    {
        private int _calledTimes;

        public void Handle(TestMessage message)
        {
            _calledTimes++;
        }

        protected int CalledTimes
        {
            get { return _calledTimes; }
        }
    }
}