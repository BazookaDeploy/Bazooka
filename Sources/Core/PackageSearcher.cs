namespace Bazooka.Core
{
    using NuGet;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Helpers to search packages list
    /// </summary>
    public static class PackageSearcher
    {
        /// <summary>
        ///     Searches all available versions of a package in a list of repostiories
        /// </summary>
        /// <param name="repositories">Repositories to search</param>
        /// <param name="packageName">Pacakge identifier</param>
        /// <returns>List of available versions</returns>
        public static ICollection<string> Search(ICollection<string> repositories, string packageName)
        {
            var factory = new PackageRepositoryFactory();
            var globalRepo = new AggregateRepository(factory, repositories, true);
            var packages = globalRepo.FindPackagesById(packageName);
            return packages.Where(x => x.Listed)
                           .Select(x => x.Version)
                           .OrderByDescending(x => x)
                           .Select(x => x.ToString())
                           .ToList();
        }
    }
}
