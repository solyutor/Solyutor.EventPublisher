using System.Reflection;
using Castle.Core;
using Castle.Windsor;
using NUnit.Framework;
using SharpTestsEx;
using Solyutor.EventPublisher.Castle.Facility;

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
                                   kernel.HasComponent(typeof(ITransientListener<Message>)) &&
                                   kernel.GetHandler(typeof(ITransientListener<Message>)).ComponentModel.LifestyleType ==
                                   LifestyleType.Transient &&
                                   kernel.HasComponent(typeof(ITransientListener<int>)) &&
                                   kernel.HasComponent(typeof(ITransientListener<string>))
                );
        }
    }

    public class MultiListener : ITransientListener<int>, ITransientListener<string>
    {
        #region ITransientListener<int> Members

        public void ListenTo(int message)
        {
        }

        #endregion

        #region ITransientListener<string> Members

        public void ListenTo(string message)
        {
        }

        #endregion
    }
}