namespace SOLIDWashTunnel.Legacy
{
    public interface ILegacyCurrencyRateConverter
    {
        decimal Convert(decimal price, string currency);
    }
}
