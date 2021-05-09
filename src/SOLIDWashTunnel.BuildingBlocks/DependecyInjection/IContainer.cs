using System;

namespace SOLIDWashTunnel.BuildingBlocks.DependecyInjection
{
    public interface IContainer
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        void Register<TService>(Func<TService> instanceCreator);
        void Register<TService>(TService instance);
        void RegisterSingleton<TService>(Func<TService> instanceCreator);
        object GetService(Type type);
        TService GetService<TService>();
    }
}
