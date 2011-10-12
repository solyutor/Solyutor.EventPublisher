using System;
using Autofac;
using Autofac.Core;
using Solyutor.EventPublisher.Impl;
using AutofacModule = global::Autofac.Module;

namespace Solyutor.EventPublisher.Autofac.Module
{
    public class PublisherModule : AutofacModule
    {
        private readonly IPublishWay _publishWay;

        public PublisherModule(IPublishWay publishWay)
        {
            if (publishWay == null)
            {
                throw new ArgumentNullException("publishWay");
            }

            _publishWay = publishWay;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_publishWay).As<IPublishWay>().SingleInstance();
            
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