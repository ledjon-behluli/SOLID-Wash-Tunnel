using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel.Invoices
{
    public interface IInvoiceBuilder
    {
        IIndividualNamePicker CreateForIndividual();
        ICompanyNamePicker CreateForCompany();
    }

    public interface IIndividualNamePicker
    {
        IProgramSelector WithName(string firstName, string lastName);
    }

    public interface ICompanyNamePicker
    {
        IProgramSelector WithName(string companyName);
    }

    public interface IProgramSelector
    {
        ICurrencyPicker Select(IWashProgram program);
    }

    public interface ICurrencyPicker
    {
        IAmountCalculator Choose(Currency currency);
    }

    public interface IAmountCalculator
    {
        IInvoicePrinter Calculate();
    }

    public interface IInvoicePrinter
    {
        string Build();
    }
}
