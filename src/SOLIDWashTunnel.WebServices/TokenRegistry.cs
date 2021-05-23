using System.Collections.Generic;

namespace SOLIDWashTunnel.WebServices
{
    internal interface ITokenRegistry
    {
        void RegisterToken(string accessToken);
        bool IsValid(string accessToken);
    }

    internal class TokenRegistry : ITokenRegistry
    {
        private readonly List<string> _accessTokens;

        public void RegisterToken(string accessToken)
            => _accessTokens.Add(accessToken);
       
        public bool IsValid(string accessToken)
            => _accessTokens.Contains(accessToken);
    }
}
