using Microsoft.Extensions.Configuration;
using System.Net;

namespace Xunit.UserContext.Services
{
    /// <summary>
    /// Provides access to read user secrets
    /// </summary>
    internal class UserSecretsProvider
    {
        private readonly string usernameKey = "Username";
        private readonly string passwordKey = "Password";
        private readonly string domainKey = "Domain";
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initialises a new instance of the <see cref="UserSecretsProvider"/> class.
        /// </summary>
        /// <param name="userSecretsId">Id user secrets were stored with</param>
        public UserSecretsProvider(string userSecretsId)
        {
            _configuration = new ConfigurationBuilder()
                .AddUserSecrets(userSecretsId)
                .Build();
        }

        /// <summary>
        /// Read credentials from user secrets into NetworkCredential
        /// </summary>
        /// <returns>Users credentials</returns>
        public NetworkCredential GetCredentials()
        {
            var username = _configuration[usernameKey] ?? string.Empty;
            var password = _configuration[passwordKey] ?? string.Empty;
            var domain = _configuration[domainKey];

            return new NetworkCredential(username, password, domain);
        }
    }
}
