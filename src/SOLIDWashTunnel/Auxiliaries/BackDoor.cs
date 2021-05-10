using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Vehicles;
using System;

namespace SOLIDWashTunnel.Auxiliaries
{
    public class BackDoor : IVehicleStatusSubscriber
    {
        public bool IsOpen { get; private set; }

        public void Ready(IVehicle vehicle)
        {
            IsOpen = true;
            Console.WriteLine("Door open!");
        }
    }

    public class InvoiceGenerator : IVehicleStatusSubscriber
    {
        private readonly IInvoiceBuilder _invoiceBuilder;

        public InvoiceGenerator(IInvoiceBuilder invoiceBuilder)
        {
            _invoiceBuilder = invoiceBuilder;
        }

        public void Ready(IVehicle vehicle)
        {
            Console.WriteLine("Invoice generated");
        }
    }
}
