namespace SOLIDWashTunnel.Control
{
    // Marker interface
    public interface ISignalHandler
    {

    }

    public interface ISignalHandler<in T> : ISignalHandler
        where T : ISignal
    {
        void Handle(T signal);
    }
}
