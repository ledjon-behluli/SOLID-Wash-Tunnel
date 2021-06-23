using SOLIDWashTunnel.DI.Abstractions;
using System;
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
     *   The motherboard has access to all of its connected components (CPU, RAM etc.) 
     *   but is not tied to any of them, or any implementation of them either.
     *          
     * Learn more: 
     *   https://en.wikipedia.org/wiki/Mediator_pattern
     */


    /*
     * Anti-Pattern
     *   Service Locator
     *   
     * Reason:
     *   As we can see we are injecting the 'IContainer' in motherboard constructor.
     *   We use the container to resolve the various signal handlers.
     *   Resolving services within an object is known as the 'Service Locator (Anti)Pattern', and in general should be avoided
     *   in favor of dependency injection. But if we didn't inject IContainer, we would need to inject all the ISignalHandler<T>'s. 
     *   This would be a hard to maintain solution and would violate the Open/Closed principle, since we would need to modify 
     *   the motherboard class each time a new singal is introduced.
     *   
     * Learn more: 
     *  https://en.wikipedia.org/wiki/Service_locator_pattern
     *  https://bit.ly/3oDvNrk
     */

    public interface ISignalTransmitter
    {
        void Transmit<T>(T signal) where T : ISignal;
    }

    public class Motherboard : ISignalTransmitter
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

        private IEnumerable<ISignalHandler<T>> GetSignalHandlers<T>() where T : ISignal
            => Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(ISignalHandler<T>).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(type => (ISignalHandler<T>)_container.GetService(type));
    }
}
