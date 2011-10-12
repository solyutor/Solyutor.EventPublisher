﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.MicroKernel.Registration;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public static class AllTransientHandlers
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
                var services = type.TransientHandlerServices();

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


        public static Type[] TransientHandlerServices(this Type self)
        {
            return self.FindInterfaces(
                (@interface, nomatter) => @interface.IsGenericType &&
                                          @interface.GetGenericTypeDefinition() == typeof (ITransientHandler<>), null);
        }


    }
}