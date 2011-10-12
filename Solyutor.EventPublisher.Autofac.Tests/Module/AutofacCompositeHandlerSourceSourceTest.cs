using System.Linq;
using System.Reflection;
using Autofac;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Autofac.Module;
using Solyutor.EventPublisher.Impl;
using Solyutor.EventPublisher.Tests.Impl;

namespace Solyutor.EventPublisher.Autofac.Tests.Module
{
    [TestFixture]
    public class AutofacCompositeHandlerSourceSourceTest : IHandler<TestMessage>
    {
        [Test]
        public void Can_get_listeners_from_all_sources()
        {
            var builder = new ContainerBuilder();
        
            builder.RegisterModule(new PublisherModule(new SimplePublishWay()));
            builder.RegisterPerDependencyListenersFrom(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var assigne = container.Resolve<IAssignee>();
            assigne.Subscribe(this);

            var listenerSource = container.Resolve<IHandlerSource>();

            
            var listeners = listenerSource.ResolveListenersFor<TestMessage>();

            listeners.Satisfy(l =>
                l.Count() == 2 && 
                l.Contains(this) &&
                l.Any( x => x.GetType() == typeof(TestPerDependencyHandler)));
        }

        public void Handle(TestMessage message)
        {
            
        }
    }
}