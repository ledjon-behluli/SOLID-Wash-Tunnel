
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using System;

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
        private readonly IUserPanel _userPanel;
        private readonly IWashTunnel _washTunnel;
        private readonly IBackDoor _backDoor;

        private IWashProgram _selectedProgram;

        public CentralControllerUnit(
            IUserPanel userPanel,
            IWashTunnel washTunnel,
            IBackDoor backDoor)
        {
            _userPanel = userPanel;
            _washTunnel = washTunnel;
            _backDoor = backDoor;
        }

        public void Transmit(Signal signal)
        {
            switch (signal)
            {
                case Signal.VehicleReady: _backDoor.Open();
                    break;
            };
        }

        public void Transmit(Signal signal, object extraInfo)
        {
            switch (signal)
            {
                case Signal.WashProgramSelected: _selectedProgram = (IWashProgram)extraInfo;
                    break;
                case Signal.StartWashing:
                    {
                        if (_selectedProgram == null)
                            throw new InvalidOperationException("Vehicle washing can not start until a program has been selected!");

                        _washTunnel.Wash((IVehicle)extraInfo, _selectedProgram);
                    }
                    break;
            };
        }
    }
}
