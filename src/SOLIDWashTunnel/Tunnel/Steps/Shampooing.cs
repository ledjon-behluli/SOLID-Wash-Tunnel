using SOLIDWashTunnel.ClientFacing;
using System;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class Shampooing : WashStep
    {
        public override int Id => 4;
        public override int CleaningFactor => 1;
        public override Money Price => Money.Create(0.8m);

        public override void Act(IVehicle vehicle, Action<IWashStep, bool> callback)
        {
            vehicle.Accept(this);
            callback.Invoke(this, true);

            base.Act(vehicle, callback);
        }

        public override string GetDescription()
        {
            return "Shampooing";
        }
    }
}
