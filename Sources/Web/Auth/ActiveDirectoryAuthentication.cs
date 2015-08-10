namespace Web.Auth
{
    using System.Configuration;
    using System.DirectoryServices.AccountManagement;

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
                return pc.ValidateCredentials(domain + "\\" + username, password) || pc.ValidateCredentials(username, password);
            }
        }

    }
}
