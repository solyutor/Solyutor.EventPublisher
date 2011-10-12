using System.Collections.Generic;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Tests.Impl
{
    [TestFixture]
    public class CompositeListenerSourceTest
    {
        private CompositeHandlerSource _compositeSource;
        private SimpleAssignee _firstSource;
        private SimpleAssignee _secondSource;

        [SetUp]
        public void SetUp()
        {
            _compositeSource = new CompositeHandlerSource();

            _firstSource = new SimpleAssignee();
            _secondSource = new SimpleAssignee();

            _compositeSource.AddSource(_firstSource);
            _compositeSource.AddSource(_secondSource);
        }

        [Test]
        public void Resolve_listners_from_all_sources()
        {
            var firstListener = new TestHandler();
            var secondListener = new TestHandler();

            _firstSource.Subscribe(firstListener);
            _secondSource.Subscribe(secondListener);

            var listeners = GetListenersFromCompositeSource();

            listeners.Satisfy(list =>
                              list.Contains(firstListener) &&
                              list.Contains(secondListener));
        }

        private IList<IHandler<TestMessage>> GetListenersFromCompositeSource()
        {
            return new List<IHandler<TestMessage>>(_compositeSource.ResolveListenersFor<TestMessage>());
        }

        [Test]
        public void Resolve_will_return_the_only_instance_of_listener_if_it_exists_in_many_sources()
        {
            var listener = new TestHandler();

            _firstSource.Subscribe(listener);
            _secondSource.Subscribe(listener);

            var result = GetListenersFromCompositeSource();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Remove_removes_source()
        {
            var listener = new TestHandler();
            _firstSource.Subscribe(listener);

            _compositeSource.RemoveSource(_firstSource);

            var reulst = GetListenersFromCompositeSource();

            Assert.That(reulst, Is.Empty);
        }

        [Test]
        public void Constructor_add_all_sources()
        {
            _compositeSource = new CompositeHandlerSource(new[]{_firstSource, _secondSource});

            var firstListener = new TestHandler();
            var secondListener = new TestHandler();

            _firstSource.Subscribe(firstListener);
            _secondSource.Subscribe(secondListener);

            var listeners = GetListenersFromCompositeSource();

            listeners.Satisfy(list =>
                              list.Contains(firstListener) &&
                              list.Contains(secondListener));
        }
    }
}