using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Solyutor.EventPublisher.Castle.Impl;

namespace Solyutor.EventPublisher.Castle.Tests.Impl
{
    [TestFixture]
    public class TransientSourceTest
    {
        [Test]
        public void Resolve_returns_transient_instances_from_kernel()
        {
            var windsor = new WindsorContainer();
            windsor.Register(
                Component.For<ITransientListener<Message>>()
                    .ImplementedBy<TestTransientListener>());
            
            var source = new TransientSource(windsor.Kernel);

            var listenters = source.ResolveListenersFor<Message>();

            Assert.That(listenters.OfType<ITransientListener<Message>>().Count(), Is.EqualTo(1));

        }
    }
}