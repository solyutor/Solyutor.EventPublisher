using System;
using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Solyutor.EventPublisher.Castle.Facility;
using Solyutor.EventPublisher.Impl;

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

            windsor.Register(AllTransientListeners.FromCurrentAssembly());

            windsor.Register(Component.For<MainForm>().LifeStyle.Singleton,
                Component.For<IHellowStrategy>().ImplementedBy<HellowStrategy>());

            windsor.AddFacility("Event.Publisher", new PublisherFacility(new SimplePublishWay()));


            Application.Run(windsor.Resolve<MainForm>());
        }
    }
}
