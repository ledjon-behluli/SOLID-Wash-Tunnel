using SOLIDWashTunnel.ClientFacing;
using System;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class ChasisAndWheelWashing : WashStep
    {
        public override int CleaningFactor => 3;
        public override Money Price => Money.Create(1.5m);

        public override void Act(IVehicle vehicle, Action<IWashAction, bool> callback)
        {
            vehicle.Accept(this);
            callback.Invoke(this, true);

            base.Act(vehicle, callback);
        }

        public override string GetDescription()
        {
            return "Chasis & wheels washing";
        }
    }
}
