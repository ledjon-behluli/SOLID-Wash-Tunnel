using System;

namespace SOLIDWashTunnel.DI.Abstractions
{
    public interface IContainer
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        void Register<TService>(Func<TService> instantiator);
        void Register<TService>(TService instance);
        void RegisterSingleton<TService>(Func<TService> instantiator);
        void Decorate<TService, TImplementation>() where TImplementation : TService;
        object GetService(Type type);
        TService GetService<TService>();
    }
}
