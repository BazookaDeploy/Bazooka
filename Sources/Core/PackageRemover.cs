namespace Bazooka.Core
{
    using NuGet;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///     Removes installed packages
    /// </summary>
    public class PackageRemover : IPackageRemover
    {
        /// <summary>
        ///     Logger
        /// </summary>
        public ILogger Logger { get; set; }

        public static void Remove(string package, string repository, string directory, string version)
        {
            var logger = new ConsoleLogger();
            var info = new PackageInfo()
            {
                InstallationDirectory = directory,
                Name = package,
                Version = version
            };

            logger.Log(string.Format("Uninstalling application {0} version {1}", info.Name, info.Version));


            DeleteFiles(info, new List<string>() { repository }, logger);

            logger.Log(string.Format("Uninstalled application {0} version {1}", info.Name, info.Version));
        }

        /// <summary>
        ///     Removes an installed package from the system
        /// </summary>
        /// <param name="info"></param>
        public void Remove(PackageInfo info, ICollection<string> repositories, Dictionary<string, string> parameters, string optionalScript)
        {
            Logger.Log(string.Format("Uninstalling application {0} version {1}", info.Name, info.Version));

            ExecuteRemoveScript(info,parameters, optionalScript, this.Logger);

            DeleteFiles(info,repositories, this.Logger);

            Logger.Log(string.Format("Uninstalled application {0} version {1}", info.Name, info.Version));
        }

        /// <summary>
        ///     Deletes all files from a package
        /// </summary>
        /// <param name="installed">Insyalled package</param>
        /// <param name="repositories">Repositories where to find the package</param>
        private static void DeleteFiles(PackageInfo installed, ICollection<string> repositories, ILogger logger)
        {
            logger.Log("Deleting installed files... ");

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

            logger.Log("Installed files deleted");
        }

        /// <summary>
        ///     Excutes the uninstall script
        /// </summary>
        /// <param name="installed">Installed package</param>
        /// <param name="parameters">Parameters to pass to the script</param>
        /// <param name="optionalScript">optional additiona script to execute</param>
        private static void ExecuteRemoveScript(PackageInfo installed, Dictionary<string, string> parameters, string optionalScript, ILogger logger)
        {
            if (optionalScript != null && optionalScript.Trim().Length > 0)
            {
                logger.Log("Executing uninstallation script specified as parameter...");
                PowershellHelpers.ExecuteScript(installed.InstallationDirectory, optionalScript, logger, parameters);
                return;
            }

            if (File.Exists(Path.Combine(installed.InstallationDirectory, "Uninstall.ps1")))
            {
                logger.Log("Executing uninstall script contained in package ... ");

                PowershellHelpers.Execute(installed.InstallationDirectory, "Uninstall.ps1", installed.Configuration, logger, parameters);
            }
        }
    }
}
