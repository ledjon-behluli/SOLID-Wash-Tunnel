using System;
using System.Collections.Generic;
using System.Linq;
using SOLIDWashTunnel.DI.Abstractions;

namespace SOLIDWashTunnel.DI
{
    /* 
    * Principle: 
    *   Inversion of Control
    *   
    * Reason: 
    *   Objects do not create other objects on which they rely to do their work.
    *   Instead they get the objects that they need from an outside source/container.
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Inversion_of_control
    */

    public class Container : IContainer
    {
        private readonly Dictionary<Type, Func<object>> _serviceDescriptors;

        public Container()
        {
            _serviceDescriptors = new Dictionary<Type, Func<object>>();
        }

        public void AddTransient<TService>(Func<TService> implementationFactory)
            => _serviceDescriptors.Add(typeof(TService), () => implementationFactory());

        public void AddTransient<TService, TImplementation>() where TImplementation : TService
            => _serviceDescriptors.Add(typeof(TService), () => GetService(typeof(TImplementation)));


        public void AddSingleton<TService>(Func<TService> implementationFactory)
        {
            var lazy = new Lazy<TService>(implementationFactory);
            AddTransient(() => lazy.Value);
        }

        public void AddSingleton<TService, TImplementation>() where TImplementation : TService
        {
            var service = (TService)GetService(typeof(TImplementation));
            var lazy = new Lazy<TService>(service);
            AddTransient(() => lazy.Value);
        }


        public void Decorate<TService, TImplementation>() where TImplementation : TService
        {
            Type serviceType = typeof(TService);
            Type implementationType = typeof(TImplementation);

            TService decorated = GetService<TService>();

            if (decorated == null)
                throw new InvalidOperationException($"Can not register decorator {implementationType}, if no decorated service of {serviceType} has be registered.");
            
            var decorator = CreateService(implementationType);

            _serviceDescriptors.Remove(serviceType);
            _serviceDescriptors.Add(serviceType, () => decorator);
        }

        public object GetService(Type type)
        {
            if (type.IsAssignableFrom(typeof(IContainer)))
                return this;
            if (_serviceDescriptors.TryGetValue(type, out Func<object> implementationFactory))
                return implementationFactory();
            else if (!type.IsInterface && !type.IsAbstract)
                return CreateService(type);
            else
                throw new InvalidOperationException($"No registration for {type}.");
        }

        public TService GetService<TService>()
        {
            return (TService)GetService(typeof(TService));
        }

        private object CreateService(Type implementationType)
        {
            var ctor = implementationType.GetConstructors().Single();
            var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType);
            var dependencies = parameterTypes.Select(t => GetService(t)).ToArray();
            return Activator.CreateInstance(implementationType, dependencies);
        }
    }
}
