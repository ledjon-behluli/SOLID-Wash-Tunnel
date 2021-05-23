using System.ComponentModel;

namespace SOLIDWashTunnel
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
