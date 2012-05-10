using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Solyutor.EventPublisher.Testing;
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
                Component
                    .For<IHandler<Message>>()
                    .ImplementedBy<TestHandler<Message>>()
                    .LifestyleSingleton(),
                Component
                    .For<IHandler<Message>>()
                    .ImplementedBy<TestHandler<Message>>()
                    .Named("transient")
                    .LifestyleTransient());
            
            var source = new WindsorSource(windsor.Kernel);

            var handlers = source.ResolveHandlersFor<Message>();

            Assert.That(handlers.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Registration_helper_methods_should_properly_register_handlers()
        {
            var windsor = new WindsorContainer();
            windsor.Register(
                Classes
                    .FromThisAssembly()
                    .IncludeNonPublicTypes()
                    .Where(type => type.IsHandler())
                    .WithServiceAllHandlers());

            var handlers = windsor.ResolveAll<IHandler<Message>>();

            Assert.That(handlers.Count(), Is.EqualTo(2));
        }

    }
}