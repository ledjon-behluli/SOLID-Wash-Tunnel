using SOLIDWashTunnel.Customers;
using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel.Invoices
{
    public class Invoice
    {
        public string Recepient { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public IWashProgram WashProgram { get; set; }
    }

    public class InvoiceCustomerInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public Currency PreferredCurrency { get; set; }
        public CustomerType CustomerType => !string.IsNullOrEmpty(CompanyName) ? CustomerType.Company : CustomerType.Individual; 
    }
}
