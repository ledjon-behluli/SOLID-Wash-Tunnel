using SOLIDWashTunnel.IoC;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SOLIDWashTunnel.Control
{
    /* 
     * Pattern: 
     *   Mediator
     *   
     * Reason: 
     *   Encapsulate communication logic between wash tunnel components, in order to reduce dependencies between them.
     *   
     * Learn more: 
     *   https://en.wikipedia.org/wiki/Mediator_pattern
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
                .Select(type => _container.GetService<IControlUnitSignalHandler<T>>(type));
        }
    }
}
