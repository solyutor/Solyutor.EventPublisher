using System;
using Autofac;
using Autofac.Core;
using Solyutor.EventPublisher.Impl;
using AutofacModule = global::Autofac.Module;

namespace Solyutor.EventPublisher.Autofac.Module
{
    public class PublisherModule : AutofacModule
    {
        private readonly IDispatcher _dispatcher;

        public PublisherModule(IDispatcher dispatcher)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException("dispatcher");
            }

            _dispatcher = dispatcher;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_dispatcher).As<IDispatcher>().SingleInstance();
            
            builder.RegisterType<SimpleAssignee>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<AutofacHandlerSource>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<AutofacCompositeHandlerSource>()
                .AsImplementedInterfaces()
                .SingleInstance();
            
            builder.RegisterType<Publisher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}