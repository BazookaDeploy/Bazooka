using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void Install(PackageInfo info, ICollection<string> repositories);
    }
}
