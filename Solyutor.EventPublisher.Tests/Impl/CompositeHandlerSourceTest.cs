﻿using System.Collections.Generic;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Tests.Impl
{
    [TestFixture]
    public class CompositeHandlerSourceTest
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
            var firstHandler = new TestHandler();
            var secondHandler = new TestHandler();

            _firstSource.Subscribe(firstHandler);
            _secondSource.Subscribe(secondHandler);

            var listeners = GetListenersFromCompositeSource();

            listeners.Satisfy(list =>
                              list.Contains(firstHandler) &&
                              list.Contains(secondHandler));
        }

        private IList<IHandler<TestMessage>> GetListenersFromCompositeSource()
        {
            return new List<IHandler<TestMessage>>(_compositeSource.ResolveHandlersFor<TestMessage>());
        }

        [Test]
        public void Resolve_will_return_the_only_instance_of_listener_if_it_exists_in_many_sources()
        {
            var handler = new TestHandler();

            _firstSource.Subscribe(handler);
            _secondSource.Subscribe(handler);

            var result = GetListenersFromCompositeSource();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Remove_removes_source()
        {
            var handler = new TestHandler();
            _firstSource.Subscribe(handler);

            _compositeSource.RemoveSource(_firstSource);

            var results = GetListenersFromCompositeSource();

            Assert.That(results, Is.Empty);
        }

        [Test]
        public void Constructor_add_all_sources()
        {
            _compositeSource = new CompositeHandlerSource(new[]{_firstSource, _secondSource});

            var firstHandler = new TestHandler();
            var secondHandler = new TestHandler();

            _firstSource.Subscribe(firstHandler);
            _secondSource.Subscribe(secondHandler);

            var listeners = GetListenersFromCompositeSource();

            listeners.Satisfy(list =>
                              list.Contains(firstHandler) &&
                              list.Contains(secondHandler));
        }
    }
}