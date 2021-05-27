using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;
using System;

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
        private readonly IMotherboard _motherboard;
        private readonly IWashProgramFactory _programFactory;

        public UserPanel(
            IMotherboard motherboard,
            IWashProgramFactory programFactory)
        {
            _motherboard = motherboard;
            _programFactory = programFactory;
        }

        public ICustomerInformationCollector SelectBuiltInProgram(ProgramType type)
        {
            IWashProgram program = _programFactory.Create(type);
            _motherboard.Transmit(new WashProgramSelectedSignal(program));

            return this;
        }

        public ICustomerInformationCollector CustomizeProgram(ICustomWashProgramBuilder builder)
        {
            IWashProgram program = builder.Build();
            _motherboard.Transmit(new WashProgramSelectedSignal(program));

            return this;
        }

        public IWashProcessStarter AsIndividual(string firstName, string lastName, Currency preferedCurrecy)
        {
            _motherboard.Transmit(new IndividualCustomerInfoEnteredSignal(firstName, lastName, preferedCurrecy));
            return this;
        }

        public IWashProcessStarter AsCompany(string companyName, Currency preferedCurrecy)
        {
            _motherboard.Transmit(new CompanyCustomerInfoEnteredSignal(companyName, preferedCurrecy));
            return this;
        }

        public void Start(IVehicle vehicle, Action<string> invoiceCallback)
        {
            _motherboard.Transmit(new VehicleWashingStartedSignal(vehicle, invoiceCallback));
        }
    }
}
