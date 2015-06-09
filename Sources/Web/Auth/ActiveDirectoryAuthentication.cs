namespace Web.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.DirectoryServices.AccountManagement;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///     Common methods to handle authentication with active directory
    /// </summary>
    public static class ActiveDirectoryAuthentication
    {
        /// <summary>
        ///     Checks if active directory authentivcation is enabled
        /// </summary>
        /// <returns>Boolean indicating if AD auth is enabled</returns>
        public static bool IsADAuthenticationEnabled()
        {
            return bool.Parse(ConfigurationManager.AppSettings["activeDirectory"]);
        }

        /// <summary>
        ///     Authenticates a user agains ad
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="password">password to check</param>
        /// <returns>User is ìauthenticated</returns>
        public static bool Authenticate(string username, string password)
        {
            var domain = ConfigurationManager.AppSettings["adDomain"];
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                return pc.ValidateCredentials(domain + "\\"+ username, password);
            }
        }

    }
}
