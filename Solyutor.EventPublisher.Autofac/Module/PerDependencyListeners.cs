using System;
using System.Reflection;
using Autofac;

namespace Solyutor.EventPublisher.Autofac.Module
{
    public static class PerDependencyListeners
    {
         public static void RegisterPerDependencyListenersFrom(this ContainerBuilder builder, params Assembly[] assemblies)
         {
             builder.RegisterAssemblyTypes(assemblies)
                 .Where(type => type.IsPerDependencyListener())
                 .AsClosedTypesOf(typeof(IListener<>))
                 .InstancePerDependency();
         }

         private static bool IsPerDependencyListener(this Type self)
        {
            var interfaces = self.FindInterfaces(
                (@interface, nomatter) => @interface.IsGenericType &&
                                          @interface.GetGenericTypeDefinition() == typeof (IPerDependencyListener<>),
                null);

            return interfaces.Length > 0;
        }
    }
}