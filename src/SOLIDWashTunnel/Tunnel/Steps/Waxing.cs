using System;
using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class Waxing : WashStep
    {
        public override int Id => 7;
        public override int CleaningFactor => 2;
        public override Money Price => Money.Create(2.2m);

        public override void Act(IVehicle vehicle, Action<IWashAction, bool> callback)
        {
            if (vehicle.FinishType != PaintFinishType.Matte)
            {
                vehicle.Accept(this);
                callback.Invoke(this, true);
            }
            else
            {
                callback.Invoke(this, false);
            }

            base.Act(vehicle, callback);
        }

        public override string GetDescription()
        {
            return "Waxing";
        }
    }
}
