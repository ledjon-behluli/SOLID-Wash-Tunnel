using SOLIDWashTunnel.BuildingBlocks.IoC;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SOLIDWashTunnel.BuildingBlocks.Mediator
{
    public interface IMediator
    {
        TResponse Send<TResponse>(IRequest<TResponse> request);
        void Notify<TNotification>(TNotification notification) where TNotification : INotification;
    }

    public abstract class Mediator : IMediator
    {
        private readonly IContainer _container;

        public Mediator(IContainer container)
        {
            _container = container;
        }

        public abstract void Register();

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var targetType = request.GetType();
            var targetHandler = typeof(IRequestHandler<,>).MakeGenericType(targetType, typeof(TResponse));
            var instance = _container.GetService(targetHandler);
            var result = InvokeInstance(instance, request, targetHandler);

            return result;
        }

        public void Notify<TNotification>(TNotification notification) where TNotification : INotification
        {
            throw new System.NotImplementedException();
        }

        private TResponse InvokeInstance<TResponse>(object instance, IRequest<TResponse> request, Type targetHandler)
        {
            var method = instance.GetType()
                .GetTypeInfo()
                .GetMethod("");     // TODO

            if (method == null)
            {
                throw new ArgumentException($"{instance.GetType().Name} is not a known {targetHandler.Name}", instance.GetType().FullName);
            }

            return (TResponse)method.Invoke(instance, new object[] { request });
        }
    }
}
