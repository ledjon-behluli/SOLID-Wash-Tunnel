using SOLIDWashTunnel.ClientFacing;
using System;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class ThreeColorFoaming : WashStep
    {
        public override int Id => 6;
        public override int CleaningFactor => 2;
        public override Money Price => Money.Create(1.7m);

        public override void Act(IVehicle vehicle, Action<IWashStep, bool> callback)
        {
            vehicle.Accept(this);
            callback.Invoke(this, true);

            base.Act(vehicle, callback);
        }

        public override string GetDescription()
        {
            return "Foaming using a three color foam";
        }
    }
}
