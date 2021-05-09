using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.WashPrograms;

namespace SOLIDWashTunnel.Tunnels
{
    public interface IWashTunnel
    {
        void SelectProgram(IWashProgram program);
        void Wash(IVehicle vehicle);
        string GetInvoiceForIndividual(string firstName, string lastName, Currency preferedCurrecy);
        string GetInvoiceForCompany(string companyName, Currency preferedCurrecy);
    }
}
