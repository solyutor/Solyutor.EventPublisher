using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.MicroKernel.Registration;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public static class AllHandlers
    {
        public static IRegistration[] FromCurrentAssembly()
        {
            return From(Assembly.GetCallingAssembly());
        }

        public static IRegistration[] From(Assembly assembly)
        {
            var result = new List<IRegistration>();
            foreach (var type in assembly.GetTypes())
            {
                var services = type.HandlerServices();

                if (services.Length == 0) continue;

                result.Add(Component.For(services).ImplementedBy(type).LifeStyle.Transient);
            }
            return result.ToArray();
        }

        public static IRegistration[] From(Assembly[] assemblies)
        {
            var results = new List<IRegistration>();
            foreach (var assembly in assemblies)
            {
                results.AddRange(From(assembly));
            }
            return results.ToArray();
        }


        public static Type[] HandlerServices(this Type self)
        {
            return self.ImplementationOfGenericInterface(typeof(IHandler<>));
        }

        public static Type[] ImplementationOfGenericInterface(this Type self, Type genericInterface)
        {
            return self.FindInterfaces(
                (@interface, nomatter) => @interface.IsGenericType &&
                                          @interface.GetGenericTypeDefinition() == genericInterface, null);
        }

        public static bool IsHandler(this Type self)
        {
            return self.IsGenericType && self.GetGenericTypeDefinition() == typeof(IHandler<>);
        }
    }
}