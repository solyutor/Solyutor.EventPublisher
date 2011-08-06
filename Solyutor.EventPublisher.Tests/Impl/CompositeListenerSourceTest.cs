using System.Collections.Generic;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Tests.Impl
{
    [TestFixture]
    public class CompositeListenerSourceTest
    {
        [Test]
        public void Resolve_listners_from_all_sources()
        {
            var compositeSource = new CompositeListenerSource();

            var firstListener = new TestListener();
            var secondListener = new TestListener();

            var firstSource = new SimpleAssignee();
            var secondSource = new SimpleAssignee();

            firstSource.Subscribe(firstListener);
            secondSource.Subscribe(secondListener);

            compositeSource.AddSource(firstSource);
            compositeSource.AddSource(secondSource);

            var listeners = new List<IListener<TestMessage>>(compositeSource.ResolveListenersFor<TestMessage>());

            listeners.Satisfy(list =>
                              list.Contains(firstListener) &&
                              list.Contains(secondListener));
        }
    }
}