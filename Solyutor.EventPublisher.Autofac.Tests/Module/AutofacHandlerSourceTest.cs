using System.Linq;
using Autofac;
using NUnit.Framework;
using Solyutor.EventPublisher.Autofac.Module;
using Solyutor.EventPublisher.Tests.Impl;

namespace Solyutor.EventPublisher.Autofac.Tests.Module
{
    [TestFixture]
    public class AutofacHandlerSourceTest
    {
        [Test]
        public void Resolve_returns_transient_instances_from_kernel()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestPerDependencyHandler>().AsImplementedInterfaces();
            var container = builder.Build();

            var source = new AutofacHandlerSource(container);

            var listenters = source.ResolveHandlersFor<TestMessage>();

            Assert.That(listenters.OfType<IPerDependencyHandler<TestMessage>>().Count(), Is.EqualTo(1));
        }
    }
}