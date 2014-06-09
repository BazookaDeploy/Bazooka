using NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazooka.Core
{

    /// <summary>
    ///     Custom path resolver used to install the package in a specific directory 
    ///     without creating unneeded subdirectories
    /// </summary>
    public class CustomPathResolver : IPackagePathResolver
    {
        /// <summary>
        ///     Folder where the package has to be installed
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        ///     Gets the package install path
        /// </summary>
        /// <param name="package">Package info</param>
        /// <returns>Directorie where to install the package</returns>
        public string GetInstallPath(IPackage package)
        {
            return BasePath;
        }

        /// <summary>
        ///     Gets package installation directory
        /// </summary>
        /// <param name="packageId">Pacakge identifier</param>
        /// <param name="version">Package semantic version</param>
        /// <returns>Directory where to install the package</returns>
        public string GetPackageDirectory(string packageId, SemanticVersion version)
        {
            return BasePath;
        }

        /// <summary>
        ///     Gets package directory
        /// </summary>
        /// <param name="package">Pacakge information</param>
        /// <returns>Directory containing package</returns>
        public string GetPackageDirectory(IPackage package)
        {
            return BasePath;
        }

        /// <summary>
        ///     Gets pakage file name
        /// </summary>
        /// <param name="packageId">Pacakge identifier</param>
        /// <param name="version">Pacakge sematic version</param>
        /// <returns>Package file name</returns>
        public string GetPackageFileName(string packageId, SemanticVersion version)
        {
            return packageId + "." + version + ".nupkg";
        }

        /// <summary>
        ///     Gets pacakge file name
        /// </summary>
        /// <param name="package">Pacakge info</param>
        /// <returns>Pacakge file name</returns>
        public string GetPackageFileName(IPackage package)
        {
            return package.Id + "." + package.Version.ToString() + ".nupkg";
        }
    }
}
