using NuGet;
using System.Collections.Generic;
using System.IO;

namespace Bazooka.Core
{

    /// <summary>
    ///     Removes installed packages
    /// </summary>
    public class PackageRemover : IPackageRemover
    {
        /// <summary>
        ///     Logger
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///     Removes an installed package from the system
        /// </summary>
        /// <param name="info"></param>
        public void Remove(PackageInfo info, ICollection<string> repositories, Dictionary<string, string> parameters, string optionalScript)
        {
            Logger.Log(string.Format("Uninstalling application {0} version {1}", info.Name, info.Version));

            ExecuteRemoveScript(info,parameters, optionalScript);

            DeleteFiles(info,repositories);

            Logger.Log(string.Format("Uninstalled application {0} version {1}", info.Name, info.Version));
        }

        private void DeleteFiles(PackageInfo installed, ICollection<string> repositories)
        {
            Logger.Log("Deleting installed files... ");

            var factory = new PackageRepositoryFactory();

            IPackage package;

            var globalRepo = new AggregateRepository(factory, repositories, true);

            package = globalRepo.FindPackage(installed.Name, SemanticVersion.Parse(installed.Version), true, true);

            if (package == null)
            {
                throw new InexistentPackageException(string.Format("Unable to find package {0} version {1}", installed.Name, installed.Version));
            }

            var fylesystem = new PhysicalFileSystem(installed.InstallationDirectory);

            fylesystem.DeleteFiles(package.GetFiles(), installed.InstallationDirectory);

            File.Delete(Path.Combine(installed.InstallationDirectory, installed.Name + "." + installed.Version + ".nupkg"));

            foreach (var config in Directory.GetFiles(installed.InstallationDirectory, "*.config"))
            {
                File.Delete(config);
            }

            Logger.Log("Installed files deleted");
        }

        private void ExecuteRemoveScript(PackageInfo installed, Dictionary<string, string> parameters, string optionalScript)
        {
            if (optionalScript != null)
            {
                Logger.Log("Executing uninstallation script specified as parameter...");
                PowershellHelpers.ExecuteScript(installed.InstallationDirectory, optionalScript, Logger, parameters);
                return;
            }

            if (File.Exists(Path.Combine(installed.InstallationDirectory, "Uninstall.ps1")))
            {
                Logger.Log("Executing uninstall script contained in package ... ");

                PowershellHelpers.Execute(installed.InstallationDirectory, "Uninstall.ps1", installed.Configuration, Logger, parameters);
            }
        }
    }
}
