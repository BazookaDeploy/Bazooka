using System.Collections.Generic;

namespace Bazooka.Core
{
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

        void Install(PackageInfo info, ICollection<string> repositories, Dictionary<string, string> additionalParams, string installScript, string configFile, string configTrasform);

    }
}
