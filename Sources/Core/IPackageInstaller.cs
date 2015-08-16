namespace Bazooka.Core
{
    using System.Collections.Generic;

    /// <summary>
    ///     Interface to be implemented by classes that are able to install a package in
    ///     a specific directory
    /// </summary>
    public interface IPackageInstaller
    {
        /// <summary>
        ///     Installs a specified package in the specified directory
        /// </summary>
        /// <param name="info">Info about hte package beng installed</param>
        void Install(PackageInfo info, ICollection<string> repositories,Dictionary<string,string> additionalParams);

        /// <summary>
        ///     Installs a package passing in the install script, and the config trasnsform
        /// </summary>
        /// <param name="info">Package informations</param>
        /// <param name="repositories">Repositories where package is contained</param>
        /// <param name="additionalParams">Additional parameters</param>
        /// <param name="installScript">Installation script to execute</param>
        /// <param name="configFile">Configuration file</param>
        /// <param name="configTrasform">Trasnform to apply</param>
        void Install(PackageInfo info, ICollection<string> repositories, Dictionary<string, string> additionalParams, string installScript, string configFile, string configTrasform);
    }
}
