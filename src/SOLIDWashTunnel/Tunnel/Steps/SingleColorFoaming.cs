using SOLIDWashTunnel.ClientFacing;
using System;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class SingleColorFoaming : WashStep
    {
        public override int CleaningFactor => 1;
        public override Money Price => Money.Create(1.1m);

        public override void Act(IVehicle vehicle, Action<IWashAction, bool> callback)
        {
            vehicle.Accept(this);
            callback.Invoke(this, true);

            base.Act(vehicle, callback);
        }

        public override string GetDescription()
        {
            return "Foaming using a single color foam";
        }
    }
}
