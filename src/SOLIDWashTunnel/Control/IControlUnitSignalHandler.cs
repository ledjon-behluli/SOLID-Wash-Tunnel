namespace SOLIDWashTunnel.Control
{
    // Marker interface
    public interface IControlUnitSignalHandler
    {

    }

    public interface IControlUnitSignalHandler<in T> : IControlUnitSignalHandler
        where T : IControlUnitSignal
    {
        void Handle(T signal);
    }
}
