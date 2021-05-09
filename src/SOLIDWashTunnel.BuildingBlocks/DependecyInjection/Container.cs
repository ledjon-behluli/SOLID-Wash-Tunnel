using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLIDWashTunnel.BuildingBlocks.DependecyInjection
{
    public class Container : IContainer
    {
        private Dictionary<Type, Func<object>> _registrations;
    
        public Container()
        {
            _registrations = new Dictionary<Type, Func<object>>();
        }

        public void Register<TService, TImplementation>() where TImplementation : TService 
            => _registrations.Add(typeof(TService), () => GetService(typeof(TImplementation)));

        public void Register<TService>(Func<TService> instanceCreator) 
            => _registrations.Add(typeof(TService), () => instanceCreator());

        public void RegisterInstance<TService>(TService instance) 
            => _registrations.Add(typeof(TService), () => instance);

        public void RegisterSingleton<TService>(Func<TService> instanceCreator)
        {
            var lazy = new Lazy<TService>(instanceCreator);
            Register(() => lazy.Value);
        }

        public object GetService(Type type)
        {
            Func<object> creator;

            if (_registrations.TryGetValue(type, out creator)) 
                return creator();
            else if (!type.IsAbstract) 
                return CreateService(type);
            else 
                throw new InvalidOperationException("No registration for " + type);
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
