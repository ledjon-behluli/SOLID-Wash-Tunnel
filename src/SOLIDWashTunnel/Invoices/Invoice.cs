using SOLIDWashTunnel.WashPrograms;

namespace SOLIDWashTunnel.Invoices
{
    public class Invoice
    {
        public string Recepient { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public IWashProgram WashProgram { get; set; }
    }
}
