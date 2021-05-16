using SOLIDWashTunnel.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SOLIDWashTunnel.Control
{
    /* 
     * Pattern: Mediator pattern
     * Reason: Encapsulate communication logic between wash tunnel components, in order to reduce dependencies between them.
     * Learn more: https://refactoring.guru/design-patterns/mediator
     */

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
            var signalHandlers = GetSignalHandlers<T>();
            foreach (var handler in signalHandlers)
            {
                handler.Handle(signal);
            }
        }

        private IEnumerable<IControlUnitSignalHandler<T>> GetSignalHandlers<T>()
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
                        var instance = _container.GetService(cpType);
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
