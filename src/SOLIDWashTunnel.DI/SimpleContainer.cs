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

    public class SimpleContainer : IContainer
    {
        private Dictionary<Type, Func<object>> _registrations;

        public SimpleContainer()
        {
            _registrations = new Dictionary<Type, Func<object>>();
        }

        public void Register<TService, TImplementation>() where TImplementation : TService
            => _registrations.Add(typeof(TService), () => GetService(typeof(TImplementation)));

        public void Register<TService>(Func<TService> instanceCreator) 
            => _registrations.Add(typeof(TService), () => instanceCreator());

        public void Register<TService>(TService instance) 
            => _registrations.Add(typeof(TService), () => instance);

        public void RegisterSingleton<TService>(Func<TService> instanceCreator)
        {
            var lazy = new Lazy<TService>(instanceCreator);
            Register(() => lazy.Value);
        }

        // In real projects use a container which supports decorator registrations out of the box.
        public void Decorate<TService, TImplementation>() where TImplementation : TService
        {
            Type serviceType = typeof(TService);
            Type implementationType = typeof(TImplementation);

            TService decorated = GetService<TService>();

            if (decorated == null)
                throw new InvalidOperationException($"Can not register decorator {implementationType}, if no decorated service of {serviceType} has be registered.");
            
            var decorator = CreateService(implementationType);

            _registrations.Remove(serviceType);
            _registrations.Add(serviceType, () => decorator);
        }

        public object GetService(Type type)
        {
            if (_registrations.TryGetValue(type, out Func<object> creator))
                return creator();
            else if (!type.IsAbstract)
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
