using Microsoft.Extensions.Configuration;
using SimpleImpersonation;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("xUnit.UserContext.Tests")]

namespace Xunit.UserContext.Services
{
    internal class UserSecretsProvider
    {
        private readonly string usernameKey = "Username";
        private readonly string passwordKey = "Password";
        private readonly string domainKey = "Domain";
        private readonly IConfiguration _configuration;

        public UserSecretsProvider(string userSecretsId)
        {
            _configuration = new ConfigurationBuilder()
                .AddUserSecrets(userSecretsId)
                .Build();
        }

        public NetworkCredential GetCredentials()
        {
            var username = _configuration[usernameKey] ?? string.Empty;
            var password = _configuration[passwordKey] ?? string.Empty;
            var domain = _configuration[domainKey];

            return new NetworkCredential(username, password, domain);
        }
    }
}
