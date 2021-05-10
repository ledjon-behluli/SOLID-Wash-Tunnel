
namespace SOLIDWashTunnel.BuildingBlocks.Mediator
{
    public interface INotification : IRequest<NaN>
    {
        
    }

    public interface INotificationHandler<in TNotification>
        where TNotification : INotification
    {
        void Handle(TNotification notification);
    }
}
