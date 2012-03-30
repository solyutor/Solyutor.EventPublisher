﻿using System;
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

            windsor.Register(AllTransientHandlers.FromCurrentAssembly());

            windsor.Register(Component.For<MainForm>().LifeStyle.Singleton,
                Component.For<IHellowStrategy>().ImplementedBy<HellowStrategy>());

            windsor.AddFacility("Event.Publisher", new PublisherFacility(new SimpleDispatcher()));


            Application.Run(windsor.Resolve<MainForm>());
        }
    }
}
