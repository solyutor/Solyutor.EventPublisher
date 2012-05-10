using System;
using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Solyutor.EventPublisher.Impl;
using Solyutor.EventPublisher.Windsor.Facility;

namespace Solyutor.EventPublisher.Sample
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var windsor = new WindsorContainer();

            windsor.Register(
                Classes
                    .FromThisAssembly()
                    .Where(type => type.IsHandler() && type != typeof(MainForm))
                    .WithServiceAllHandlers()
                    .LifestyleTransient());

            windsor.Register(
                Component
                    .For<IDispatcher>()
                    .ImplementedBy<SynchronizationContextDispatcher<WindowsFormsSynchronizationContext>>()
                    .LifestyleSingleton(),
                Component
                    .For<MainForm>()
                    .LifestyleSingleton(),
                Component
                    .For<IHellowStrategy>()
                    .ImplementedBy<HellowStrategy>()
                    .LifestyleSingleton());
                
            windsor.AddFacility<PublisherFacility>();


            Application.Run(windsor.Resolve<MainForm>());
        }
    }
}
