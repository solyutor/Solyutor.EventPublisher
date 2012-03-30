using System;
using Autofac;
using NUnit.Framework;
using Solyutor.EventPublisher.Autofac.Module;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Autofac.Tests.Module
{
    [TestFixture]
    public class PublisherModuleTest
    {
        [Test]
        public void Register_assignee()
        {
            var container = BuildContainer();

            var assignee = container.Resolve<IAssignee>();
        }

        [Test]
        public void Register_publisher()
        {
            var container = BuildContainer();

            var publisher = container.Resolve<IPublisher>();
        }

        [Test]
        public void Module_throws_if_no_publishway_were_supplied()
        {
            var builder = new ContainerBuilder();
            Assert.Throws<ArgumentNullException>(() => builder.RegisterModule(new PublisherModule(null)));
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new PublisherModule(new SimpleDispatcher()));
            var container = builder.Build();
            return container;
        }
    }
}