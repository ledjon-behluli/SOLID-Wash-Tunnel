
namespace SOLIDWashTunnel.BuildingBlocks.Mediator
{
    public interface IRequest<out TResponse>
    {
       
    }

    public interface IRequestHandler<in TRequest, out TResponse>
        where TRequest : IRequest<TResponse>
    {
        void Handle(TRequest request);
    }
}
