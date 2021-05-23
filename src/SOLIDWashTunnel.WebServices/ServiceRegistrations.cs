using SOLIDWashTunnel.DI.Abstractions;

namespace SOLIDWashTunnel.WebServices
{
    public static class ServiceRegistrations
    {
        public static IContainer AddLegacyComponents(this IContainer container)
        {
            TokenRegistry tokenRegistry = new TokenRegistry();

            container.RegisterSingleton<ITokenRegistry>(() => tokenRegistry);
            container.RegisterSingleton<IWebServiceAuthenticator>(() => new Authenticator(tokenRegistry));
            container.Register<ILegacyCurrencyRateConverter, LegacyCurrencyRateConverter>();
            
            return container;
        }
    }
}
