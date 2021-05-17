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
     * Thinking process:
     *   A motherboard allows communication between many of the crucial electronic components of a system, 
     *   such as the central processing unit and memory, and provides connectors for other peripherals.
     *   As such the motherboard is a good implementation of the Mediator Pattern.
     *   
     *   The injected dependencies reflect the real-word usage of a Motherboard
     *      IContainer -
     *          The motherboard has access to all of it's connected components (CPU, Memory etc.) but is not tied to any of them.
     *          We can think of the container as the collection of electronic paths on a PCB (Printed Circuit Board).
     *          
     *   
     * Learn more: 
     *   https://en.wikipedia.org/wiki/Mediator_pattern
     */

    public interface IMotherboard
    {
        void Transmit<T>(T signal) where T : ISignal;
    }

    public class Motherboard : IMotherboard
    {
        private readonly IContainer _container;

        public Motherboard(IContainer container)
        {
            _container = container;
        }

        public void Transmit<T>(T signal) where T : ISignal
        {
            var signalHandlers = GetSignalHandlers<T>();
            foreach (var handler in signalHandlers)
            {
                handler.Handle(signal);
            }
        }

        private IEnumerable<ISignalHandler<T>> GetSignalHandlers<T>()
            where T : ISignal
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(ISignalHandler<T>).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(type => _container.GetService<ISignalHandler<T>>(type));
        }
    }
}
