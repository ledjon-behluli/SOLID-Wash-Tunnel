using SOLIDWashTunnel.Vehicles;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Auxiliaries
{
    public class ControlPanel : IVehicleStatusPublisher
    {
        private List<IVehicleStatusSubscriber> _subscribers;

        public ControlPanel()
        {
            _subscribers = new List<IVehicleStatusSubscriber>();
        }

        public void Notify(IVehicle vehicle)
        {
            foreach (var subscriber in _subscribers)
                subscriber.Ready(vehicle);
        }

        public IVehicleStatusPublisher Subscribe(IVehicleStatusSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
            return this;
        }

        public IVehicleStatusPublisher Unsubscribe(IVehicleStatusSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
            return this;
        }
    }

    public interface IWashTunnelObserver
    {

    }
}
