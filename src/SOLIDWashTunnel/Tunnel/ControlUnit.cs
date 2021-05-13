using SOLIDWashTunnel.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SOLIDWashTunnel.Tunnel
{
    /* 
    * Pattern: Mediator pattern
    * Reason: Encapsulate communication logic between objects, in order to reduce dependencies between them.
    * Learn more:
    */

    public interface IControlUnitSignal
    {

    }

    // Marker interface
    public interface IControlUnitSignalHandler
    {

    }

    public interface IControlUnitSignalHandler<in T> : IControlUnitSignalHandler
        where T : IControlUnitSignal
    {
        void Handle(T signal);
    }

    public interface IControlUnit
    {
        void Transmit<T>(T signal) where T : IControlUnitSignal;
    }

    public class ControlUnit : IControlUnit
    {
        private readonly IContainer _container;

        public ControlUnit(IContainer container)
        {
            _container = container;
        }

        public void Transmit<T>(T signal) where T : IControlUnitSignal
        {
            var signalHandlers = GetAllSignalHandlers<T>();
            foreach (var handler in signalHandlers)
            {
                handler.Handle(signal);
            }
        }

        private IEnumerable<IControlUnitSignalHandler<T>> GetAllSignalHandlers<T>()
            where T : IControlUnitSignal
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(IControlUnitSignalHandler<T>).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(type => CreateHandlerInstance<T>(type));
        }

        private IControlUnitSignalHandler<T> CreateHandlerInstance<T>(Type type)
             where T : IControlUnitSignal
        {
            object handlerInstance = null;
            var constructors = type.GetConstructors();
            var firstConstrutor = constructors.FirstOrDefault();

            if (firstConstrutor != null)
            {
                var constructorParameters = firstConstrutor.GetParameters();
                if (constructorParameters != null && constructorParameters.Any())
                {
                    var objectList = new List<object>();
                    foreach (var constructorParameter in constructorParameters)
                    {
                        var cpType = constructorParameter.ParameterType;
                        var instance = typeof(IContainer).IsAssignableFrom(cpType) ? _container : _container.GetService(cpType);
                        objectList.Add(instance);
                    }

                    handlerInstance = Activator.CreateInstance(type, objectList.ToArray());
                }
            }

            if (handlerInstance == null)
            {
                handlerInstance = Activator.CreateInstance(type);
            }

            return (IControlUnitSignalHandler<T>)handlerInstance;
        }
    }
}
