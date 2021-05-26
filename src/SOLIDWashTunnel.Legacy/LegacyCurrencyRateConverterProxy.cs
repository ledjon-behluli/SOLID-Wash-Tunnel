using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLIDWashTunnel.Legacy
{
    /* 
    * Pattern:
    *   Proxy
    *   
    * Reason: 
    *  A proxy is a wrapper or agent object that is being called by the client to access the real serving object behind the scenes (LegacyCurrencyRateConverter here).
    *  The 'LegacyCurrencyRateConverterProxy' here is used as an access control point to serve the service only to registered users (represented with tokens). 
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Proxy_pattern
    */

    /* 
    * Pattern:
    *   Singelton
    *   
    * Reason: 
    *   Although this pattern is considered nowdays to be an anti-pattern, because of dependency injection and singelton lifespan.
    *   This is a simulation of an "old" legacy system that doesn't have dependency injection, so we provide the proxy object via the singelton pattern.
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Singleton_pattern
    */

    public class LegacyCurrencyRateConverterProxy
    {
        private readonly LegacyCurrencyRateConverter _converter;
        private readonly List<string> _tokens;

        private LegacyCurrencyRateConverterProxy()
        {
            _converter = new LegacyCurrencyRateConverter();
            _tokens = new List<string>()
            {
                "solid-tunnel-00F1BDE0-AC18-452B-A628-B8FB0335DAB6"
            };
        }

        private static readonly object _lock = new object();
        private static LegacyCurrencyRateConverterProxy _instance;

        public static LegacyCurrencyRateConverterProxy Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LegacyCurrencyRateConverterProxy();
                        }
                    }
                }

                return _instance;
            }
        }

        public ILegacyCurrencyRateConverter Authenticate(string token)
        {
            var user = _tokens.FirstOrDefault(x => x == token);

            if (user == null)
                throw new InvalidOperationException("Invalid token provided.");

            return _converter;
        }
    }
}
