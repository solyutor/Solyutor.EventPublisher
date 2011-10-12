using System;
using System.Reflection;
using Autofac;

namespace Solyutor.EventPublisher.Autofac.Module
{
    public static class PerDependencyHandlers
    {
         public static void RegisterPerDependencyListenersFrom(this ContainerBuilder builder, params Assembly[] assemblies)
         {
             builder.RegisterAssemblyTypes(assemblies)
                 .Where(type => type.IsPerDependencyHandler())
                 .AsClosedTypesOf(typeof(IHandler<>))
                 .InstancePerDependency();
         }

         private static bool IsPerDependencyHandler(this Type self)
        {
            var interfaces = self.FindInterfaces(
                (@interface, nomatter) => @interface.IsGenericType &&
                                          @interface.GetGenericTypeDefinition() == typeof (IPerDependencyHandler<>),
                null);

            return interfaces.Length > 0;
        }
    }
}