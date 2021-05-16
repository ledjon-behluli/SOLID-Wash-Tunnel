using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel.Invoices
{
    public class Invoice
    {
        public string Recepient { get; set; }
        public string VehicleMark { get; set; }
        public Money Price { get; set; }
        public IWashProgram WashProgram { get; set; }
    }
}
