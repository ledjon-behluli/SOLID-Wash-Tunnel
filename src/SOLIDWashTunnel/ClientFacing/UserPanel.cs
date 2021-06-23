using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;
using System;
using SOLIDWashTunnel.Control.Signals;

namespace SOLIDWashTunnel.ClientFacing
{
    /* 
    * Pattern:
    *   Builder
    *   
    * Reason: 
    *   Collect user information to start the vehicle wash process
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Builder_pattern
    */

    public class UserPanel : IUserPanel, ICustomerInformationCollector, IWashProcessStarter
    {
        private readonly ISignalTransmitter _transmitter;
        private readonly IWashProgramFactory _programFactory;

        public UserPanel(
            ISignalTransmitter transmitter,
            IWashProgramFactory programFactory)
        {
            _transmitter = transmitter;
            _programFactory = programFactory;
        }

        public ICustomerInformationCollector SelectBuiltInProgram(ProgramType type)
        {
            IWashProgram program = _programFactory.Create(type);
            _transmitter.Transmit(new WashProgramSelectedSignal(program));

            return this;
        }

        public ICustomerInformationCollector CustomizeProgram(ICustomWashProgramBuilder builder)
        {
            IWashProgram program = builder.Build();
            _transmitter.Transmit(new WashProgramSelectedSignal(program));

            return this;
        }

        public IWashProcessStarter AsIndividual(string firstName, string lastName, Currency preferedCurrecy)
        {
            _transmitter.Transmit(new IndividualCustomerInfoEnteredSignal(firstName, lastName, preferedCurrecy));
            return this;
        }

        public IWashProcessStarter AsCompany(string companyName, Currency preferedCurrecy)
        {
            _transmitter.Transmit(new CompanyCustomerInfoEnteredSignal(companyName, preferedCurrecy));
            return this;
        }

        public void Start(IVehicle vehicle, Action<string> invoiceCallback)
        {
            _transmitter.Transmit(new VehicleWashingStartedSignal(vehicle, invoiceCallback));
        }
    }
}
