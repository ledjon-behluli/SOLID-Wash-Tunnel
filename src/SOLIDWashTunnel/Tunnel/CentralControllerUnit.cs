
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using System;
using SOLIDWashTunnel.IoC;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Customers;

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
        ClientInfosCollected,
        StartWashing,
        VehicleReady
    }

    public interface ICentralControllerUnit
    {
        void Transmit(Signal signal, params object[] extraInfos);
    }

    public class CentralControllerUnit : ICentralControllerUnit
    {
        private readonly IContainer container;

        private IWashProgram _selectedProgram;
        private InvoiceCustomerInfo _invoiceInfo;

        public CentralControllerUnit(IContainer container)
        {
            this.container = container;
        }

        public void Transmit(Signal signal, params object[] extraInfos)
        {
            switch (signal)
            {
                case Signal.WashProgramSelected:
                    {
                        _selectedProgram = (IWashProgram)extraInfos[0];
                    }
                    break;
                case Signal.ClientInfosCollected:
                    {
                        _invoiceInfo = (InvoiceCustomerInfo)extraInfos[0];
                    }
                    break;
                case Signal.StartWashing:
                    {
                        if (_selectedProgram == null)
                            throw new InvalidOperationException("Vehicle washing can not start until a program has been selected!");

                        container.GetService<IWashTunnel>()
                                 .Wash((IVehicle)extraInfos[0], _selectedProgram);
                    }
                    break;
                case Signal.VehicleReady:
                    {
                        IInvoiceBuilder invoiceBuilder = container.GetService<IInvoiceBuilder>();

                        /*
                         * Note: We could have moved this logic into the invoice builder, but would it have been correct?
                           Is it really the responsibility of the invoice builder to know, wether to create an invoice for an individual
                           or a company. Or should it simply provide an interface for builing an invoice, and the caller (CCU here) should
                           decide what kind of invoice to build. I think its the 2nd, but it is one of these situation where a clear cut answer
                           is hard to make.
                        */
                        if (_invoiceInfo.CustomerType == CustomerType.Individual)
                        {
                            invoiceBuilder
                                .CreateForIndividual()
                                .WithName(_invoiceInfo.FirstName, _invoiceInfo.LastName)
                                .Select(_selectedProgram)
                                .Choose(_invoiceInfo.PreferredCurrency)
                                .Calculate()
                                .Print();
                        }
                        else
                        {
                            invoiceBuilder
                                .CreateForCompany()
                                .WithName(_invoiceInfo.CompanyName)
                                .Select(_selectedProgram)
                                .Choose(_invoiceInfo.PreferredCurrency)
                                .Calculate()
                                .Print();
                        }

                        container.GetService<IBackDoor>()
                                 .Open();
                    }
                    break;
                default:
                    throw new NotSupportedException("Signal type not supported!");
            };
        }
    }
}
