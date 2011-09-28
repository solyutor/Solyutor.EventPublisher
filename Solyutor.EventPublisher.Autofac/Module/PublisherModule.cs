using System;
using Autofac;
using Autofac.Core;
using Solyutor.EventPublisher.Impl;


namespace Solyutor.EventPublisher.Autofac.Module
{
    public class PublisherModule : IModule
    {
        private readonly IPublishWay _publishWay;
        private ContainerBuilder _builder;

        public PublisherModule(IPublishWay publishWay)
        {
            if (publishWay == null)
            {
                throw new ArgumentNullException("publishWay");
            }
            _builder = new ContainerBuilder();

            _publishWay = publishWay;
        }

        public void Configure(IComponentRegistry componentRegistry)
        {
            if (componentRegistry == null)
                throw new ArgumentNullException("componentRegistry");

            IAssignee assignee = new SimpleAssignee();
            IListenerSource listenerSource = new CompositeListenerSource(new IListenerSource[] {new AutofacListenerSource(), assignee});

            _builder.RegisterInstance(_publishWay).As<IPublishWay>().SingleInstance();
            _builder.RegisterInstance(assignee).As<IAssignee>().SingleInstance();
            _builder.RegisterInstance(listenerSource).As<IListenerSource>();

            _builder.RegisterType<Publisher>().AsImplementedInterfaces().SingleInstance();
            
            _builder.Update(componentRegistry);
        }
    }
}