﻿using System.Reflection;
using Autofac;
using NUnit.Framework;
using Solyutor.EventPublisher.Autofac.Module;
using Solyutor.EventPublisher.Tests.Impl;

namespace Solyutor.EventPublisher.Autofac.Tests.Module
{
    [TestFixture]
    public class PerDependencyHandlersTest
    {
        private ContainerBuilder _builder;
        private IContainer _container;

        [SetUp]
        public void SetUp()
        {
            _builder = new ContainerBuilder();

            _builder.RegisterPerDependencyListenersFrom(Assembly.GetExecutingAssembly());

            _container = _builder.Build();
        }

        [Test]
        public void Register_per_dependency_instances()
        {
            _container.Resolve<IHandler<TestMessage>>();
        }

        [Test]
        public void Return_different_instances_when_resoleved()
        {
            var first = _container.Resolve<IHandler<TestMessage>>();

            var second = _container.Resolve<IHandler<TestMessage>>();

            Assert.That(first, Is.Not.SameAs(second));
        }
    }
}