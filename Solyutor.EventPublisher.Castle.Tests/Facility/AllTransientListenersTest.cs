using System.Reflection;
using Castle.Core;
using Castle.Windsor;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Windsor;
using Solyutor.EventPublisher.Windsor.Facility;

namespace Solyutor.EventPublisher.Castle.Tests.Facility
{
    [TestFixture]
    public class AllTransientListenersTest
    {
        [Test]
        public void Can_register_all_transient_listenter_from_current_assembly()
        {
            var windsor = new WindsorContainer();

            windsor.Register(AllTransientListeners.FromCurrentAssembly());

            AssertRegistration(windsor);
        }

        [Test]
        public void Register_all_listeners_services_from_single_assembly()
        {
            var windsor = new WindsorContainer();

            windsor.Register(AllTransientListeners.From(Assembly.GetExecutingAssembly()));

            AssertRegistration(windsor);

        }

        [Test]
        public void Register_all_listeners_from_array_of_assemblies()
        {
            var windsor = new WindsorContainer();

            windsor.Register(AllTransientListeners.From(new[] {Assembly.GetExecutingAssembly()}));

            AssertRegistration(windsor);

        }

        private static void AssertRegistration(WindsorContainer windsor)
        {
            windsor.Kernel.Satisfy(kernel =>
                                   kernel.HasComponent(typeof(ITransientHandler<Message>)) &&
                                   kernel.GetHandler(typeof(ITransientHandler<Message>)).ComponentModel.LifestyleType ==
                                   LifestyleType.Transient &&
                                   kernel.HasComponent(typeof(ITransientHandler<int>)) &&
                                   kernel.HasComponent(typeof(ITransientHandler<string>))
                );
        }
    }

    public class MultiHandler : ITransientHandler<int>, ITransientHandler<string>
    {
        #region ITransientHandler<int> Members

        public void Handle(int message)
        {
        }

        #endregion

        #region ITransientHandler<string> Members

        public void Handle(string message)
        {
        }

        #endregion
    }
}