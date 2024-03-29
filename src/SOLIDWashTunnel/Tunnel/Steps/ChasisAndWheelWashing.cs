using SOLIDWashTunnel.ClientFacing;
using System;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class ChasisAndWheelWashing : WashStep
    {
        public override int Id => 2;
        public override int CleaningFactor => 3;
        public override Money Price => Money.Create(1.5m);

        public override void Act(IVehicle vehicle, Action<IWashStep, bool> callback)
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
