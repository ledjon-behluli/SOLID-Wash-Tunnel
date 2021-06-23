namespace SOLIDWashTunnel.Control
{
    /* 
     * Pattern: 
     *   Command
     *   
     * Reason: 
     *   Encapsulate all information needed to perform an action, or react to an event.
     *  
     * Learn more: 
     *   https://en.wikipedia.org/wiki/Command_pattern
     */

    public interface ISignalHandler<in T> 
        where T : ISignal
    {
        void Handle(T signal);
    }
}
