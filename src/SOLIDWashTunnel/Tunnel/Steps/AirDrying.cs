using SOLIDWashTunnel.ClientFacing;
using System;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class AirDrying : WashStep
    {
        public override int Id => 1;
        public override int CleaningFactor => 1;
        public override Money Price => Money.Create(0.5m);

        public override void Act(IVehicle vehicle, Action<IWashAction, bool> callback)
        {
            vehicle.Accept(this);
            callback.Invoke(this, true);

            base.Act(vehicle, callback);
        }

        public override string GetDescription()
        {
            return "Air drying";
        }
    }
}
