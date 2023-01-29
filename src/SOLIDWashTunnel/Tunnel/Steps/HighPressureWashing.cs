using SOLIDWashTunnel.ClientFacing;
using System;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class HighPressureWashing : WashStep
    {
        public override int Id => 3;
        public override int CleaningFactor => 5;
        public override Money Price => Money.Create(0.3m);

        public override void Act(IVehicle vehicle, Action<IWashStep, bool> callback)
        {
            vehicle.Accept(this);
            callback.Invoke(this, true);

            base.Act(vehicle, callback);
        }

        public override string GetDescription()
        {
            return "High water pressure washing";
        }
    }
}
