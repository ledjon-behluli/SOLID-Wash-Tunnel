using System;

namespace SOLIDWashTunnel.DI.Abstractions
{
    public interface IContainer
    {
        void AddTransient<TService>(Func<TService> implementationFactory);
        void AddTransient<TService, TImplementation>() where TImplementation : TService;
        void AddSingleton<TService>(Func<TService> implementationFactory);
        void AddSingleton<TService, TImplementation>() where TImplementation : TService;
        void Decorate<TService, TImplementation>() where TImplementation : TService;
        object GetService(Type type);
        TService GetService<TService>();
    }
}
