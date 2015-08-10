using System.Collections.Generic;

namespace Bazooka.Core
{
    /// <summary>
    ///     Inteface implemented by systems able to remove an installed package
    /// </summary>
    public interface IPackageRemover
    {
        /// <summary>
        ///     Removes an installed package from the system
        /// </summary>
        /// <param name="info">Package installation info</param>
        void Remove(PackageInfo info, ICollection<string> repositories, Dictionary<string, string> additionalParams, string optionalScript);
    }
}
