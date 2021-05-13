
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using System;
using SOLIDWashTunnel.IoC;

namespace SOLIDWashTunnel.Tunnel
{
    /* 
    * Pattern: Mediator pattern
    * Reason: TODO
    * Learn more:
    */

    public enum Signal
    {
        WashProgramSelected,
        StartWashing,
        VehicleReady
    }

    public interface ICentralControllerUnit
    {
        void Transmit(Signal signal);
        void Transmit(Signal signal, object extraInfo);
    }

    public class CentralControllerUnit : ICentralControllerUnit
    {
        private readonly IContainer container;
        private IWashProgram _selectedProgram;

        public CentralControllerUnit(IContainer container)
        {
            this.container = container;
        }

        public void Transmit(Signal signal)
        {
            switch (signal)
            {
                case Signal.VehicleReady:
                    {
                        container.GetService<IBackDoor>()
                                 .Open();
                    }
                    break;
            };
        }

        public void Transmit(Signal signal, object extraInfo)
        {
            switch (signal)
            {
                case Signal.WashProgramSelected:
                    {
                        _selectedProgram = (IWashProgram)extraInfo;
                    }
                    break;
                case Signal.StartWashing:
                    {
                        if (_selectedProgram == null)
                            throw new InvalidOperationException("Vehicle washing can not start until a program has been selected!");

                        container.GetService<IWashTunnel>()
                                 .Wash((IVehicle)extraInfo, _selectedProgram);
                    }
                    break;
            };
        }
    }
}
