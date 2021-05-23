using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel.WebServices
{
    public interface IWebServiceAuthenticator
    {
        /// <inheritdoc />
        /// <summary>
        /// Authenticate against the webservices offered.
        /// </summary>
        /// <returns>A token to make futher requests.</returns>
        string Authenticate(string username, string password);
    }

    internal class Authenticator : IWebServiceAuthenticator
    {
        private readonly Dictionary<string, string> _users;
        private readonly ITokenRegistry _tokenRegistry;

        public Authenticator(ITokenRegistry tokenRegistry)
        {
            _users = new Dictionary<string, string>()
            {
                { "solid-wash-tunnel", "1234" }
            };

            _tokenRegistry = tokenRegistry;
        }

        public string Authenticate(string username, string password)
        {
            if (_users.TryGetValue(username, out string _password))
            {
                if (password == _password)
                {
                    string token = Guid.NewGuid().ToString();
                    _tokenRegistry.RegisterToken(token);

                    return token; 
                }

                throw new InvalidOperationException("Username or password is incorrect");
            }

            throw new InvalidOperationException($"Username {username} is not known.");
        }
    }
}
