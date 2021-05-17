using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLIDWashTunnel.IoC
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

    /* 
    * Pattern:
    *   Singelton
    *   
    * Reason: 
    *   Although this pattern is considered nowdays to be an anti-pattern, because of dependency injection and singelton lifespan.
    *   It sometimes makes sense to implement it:
    *   Our simple IoC container below, doesn't make sense to have multiple instance of it running.
    *   It also doesn't make sense to register **the container, within the container** with a singelton lifespan.
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Singleton_pattern
    */

    public class Container : IContainer, IDisposable
    {
        private Dictionary<Type, Func<object>> _registrations;

        private static readonly object _lock = new object();
        private static IContainer _instance;

        public static IContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if(_instance == null)
                        {
                            _instance = new Container();
                        }
                    }
                }

                return _instance;
            }
        }
    
        private Container()
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

        public object GetService(Type type)
        {
            if (_registrations.TryGetValue(type, out Func<object> creator))
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

        public TService GetService<TService>(Type type)
        {
            return (TService)GetService(type);
        }

        private object CreateService(Type implementationType)
        {
            var ctor = implementationType.GetConstructors().Single();
            var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType);
            var dependencies = parameterTypes.Select(t => GetService(t)).ToArray();
            return Activator.CreateInstance(implementationType, dependencies);
        }

        public void Dispose()
        {
            _registrations.Clear();
            _instance = null;
        }
    }
}
