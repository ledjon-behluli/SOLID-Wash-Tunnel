using System;

namespace SOLIDWashTunnel.DI.Abstractions
{
    public interface IContainer : IDisposable
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        void Register<TService>(Func<TService> instanceFactory);
        void Register<TService>(TService instance);
        void RegisterSingleton<TService>(Func<TService> instanceFactory);
        void Decorate<TService, TImplementation>() where TImplementation : TService;
        object GetService(Type type);
        TService GetService<TService>();
    }
}
