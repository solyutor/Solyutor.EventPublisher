using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Solyutor.EventPublisher.Windsor;
using Solyutor.EventPublisher.Windsor.Facility;

namespace Solyutor.EventPublisher.Castle.Tests.Facility
{
    [TestFixture]
    public class TransientSourceTest
    {
        [Test]
        public void Resolve_returns_transient_instances_from_kernel()
        {
            var windsor = new WindsorContainer();
            windsor.Register(
                Component.For<ITransientHandler<Message>>()
                    .ImplementedBy<TestTransientHandler>());
            
            var source = new TransientSource(windsor.Kernel);

            var handlers = source.ResolveHandlersFor<Message>();

            Assert.That(handlers.OfType<ITransientHandler<Message>>().Count(), Is.EqualTo(1));

        }
    }
}