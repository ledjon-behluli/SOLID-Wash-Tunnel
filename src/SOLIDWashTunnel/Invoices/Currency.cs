using System.ComponentModel;

namespace SOLIDWashTunnel.Invoices
{
    public enum Currency
    {
        [Description("$")]
        USD,
        [Description("€")]
        EUR,
        [Description("C$")]
        CAD,
        [Description("¥")]
        JPY
    }
}
