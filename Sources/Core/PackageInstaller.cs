namespace Bazooka.Core
{
    using Microsoft.Web.XmlTransform;
    using NuGet;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///     Default PackageInstaller implementation
    /// </summary>
    public class PackageInstaller : IPackageInstaller
    {
        public static void Install(string package, string version, string repository, string directory, string configuration)
        {
            var logger = new ConsoleLogger();
            var info = new PackageInfo()
            {
                Configuration = configuration,
                InstallationDirectory = directory,
                Name = package,
                Version = version
            };

            DownloadPackage(info, new List<string>() { repository }, logger);

            ApplyTransformations(info, null, null, logger);
        }

        /// <summary>
        ///     Logger to use
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///     Installs the specified package into the specified directory
        /// </summary>
        /// <param name="info"></param>
        public void Install(PackageInfo info, ICollection<string> repositories, Dictionary<string, string> parameters)
        {
            Logger.Log(string.Format("Installing application {0} version {1}", info.Name, info.Version));

            DownloadPackage(info, repositories, this.Logger);

            ApplyTransformations(info, null, null, this.Logger);

            ExecuteInstallScript(info, parameters, null, this.Logger);

            Logger.Log(string.Format("Installed application {0} version {1}", info.Name, info.Version));

        }

        /// <summary>
        ///     Searches for an install script and executes it passing the configuration as parameter
        /// </summary>
        /// <param name="info">Pacakge installation informations</param>
        private static void ExecuteInstallScript(PackageInfo info, Dictionary<string, string> parameters, string installScript, ILogger logger)
        {
            if (installScript != null && installScript.Trim().Length > 0)
            {
                logger.Log("Executing install script passed as parameter");

                PowershellHelpers.ExecuteScript(info.InstallationDirectory, installScript, logger, parameters);
            }

            var file = Path.Combine(info.InstallationDirectory, "install.ps1");

            if (File.Exists(file))
            {
                logger.Log("Executing install script inside package...");
                PowershellHelpers.Execute(info.InstallationDirectory, "install.ps1", info.Configuration, logger, parameters);
            }
        }

        /// <summary>
        ///     Searches for a suitable config transformation and applies it 
        /// </summary>
        /// <param name="info">Package info</param>
        private static void ApplyTransformations(PackageInfo info, string configFile, string transform, ILogger logger)
        {
            if (configFile != null && transform != null)
            {
                logger.Log("Applying transform passed as parameter");

                using (var transformation = new XmlTransformation(transform, isTransformAFile: false, logger: null))
                {
                    var dest = Path.Combine(info.InstallationDirectory, configFile);

                    using (var document = new XmlTransformableDocument())
                    {
                        document.PreserveWhitespace = true;

                        // make sure we close the input stream immediately so that we can override 
                        // the file below when we save to it.
                        using (var inputStream = File.OpenRead(dest))
                        {
                            document.Load(inputStream);
                        }

                        bool succeeded = transformation.Apply(document);
                        if (succeeded)
                        {
                            File.Delete(dest);
                            using (var fileStream = File.OpenWrite(dest))
                            {
                                document.Save(fileStream);
                                logger.Log("Transformation applied successfully");
                            }
                        }
                        else
                        {
                            throw new TransformException(String.Format("Unable to apply {0} transformation to {1}", dest.Replace(".config", "." + info.Configuration + ".config"), dest));
                        }
                    }
                }

                return;
            }


            var files = Directory.GetFiles(info.InstallationDirectory, "*.config", SearchOption.AllDirectories)
                                 .Where(file => File.Exists(file.Replace(".config", "." + info.Configuration + ".config")));

            foreach (var file in files)
            {
                logger.Log(String.Format("Applying transformation {0} contained in package ...", Path.GetFileName(file)));

                var sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(file.Replace(".config", "." + info.Configuration + ".config")))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.AppendLine(line);
                    }
                }

                string content = sb.ToString();

                using (var transformation = new XmlTransformation(content, isTransformAFile: false, logger: null))
                {
                    using (var document = new XmlTransformableDocument())
                    {
                        document.PreserveWhitespace = true;

                        // make sure we close the input stream immediately so that we can override 
                        // the file below when we save to it.
                        using (var inputStream = File.OpenRead(file))
                        {
                            document.Load(inputStream);
                        }

                        bool succeeded = transformation.Apply(document);
                        if (succeeded)
                        {
                            File.Delete(file);
                            using (var fileStream = File.OpenWrite(file))
                            {
                                document.Save(fileStream);
                                logger.Log("Transformation applied successfully");
                            }
                        }
                        else
                        {
                            throw new TransformException(String.Format("Unable to apply {0} transformation to {1}", file.Replace(".config", "." + info.Configuration + ".config"), file));
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Downloads the specified package and installs it in the configured folder
        /// </summary>
        /// <param name="info">Pacakge informations</param>
        private static void DownloadPackage(PackageInfo info, ICollection<string> repositories, ILogger logger)
        {
            logger.Log(String.Format("Downloading package for {0} version {1} ... ", info.Name, info.Version));

            var factory = new PackageRepositoryFactory();

            IPackage package;

            var globalRepo = new AggregateRepository(factory, repositories, true);

            package = globalRepo.FindPackage(info.Name, SemanticVersion.Parse(info.Version), true, true);

            if (package == null)
            {
                throw new InexistentPackageException(string.Format("Unable to find package {0} version {1}", info.Name, info.Version));
            }

            var manager = new PackageManager(
                globalRepo,
                new CustomPathResolver() { BasePath = info.InstallationDirectory },
                new OverWritingPhysicalFilesystem(info.InstallationDirectory));

            manager.InstallPackage(package, false, true);

            logger.Log(String.Format("Package for {0} version {1} downloaded ... ", info.Name, info.Version));
        }

        /// <summary>
        ///     Installs the package
        /// </summary>
        /// <param name="info">Package informations</param>
        /// <param name="repositories">Repositories to use</param>
        /// <param name="parameters">parameters to pass to the script</param>
        /// <param name="installScript">Installation script to execute</param>
        /// <param name="configFile">Name of the configuration file</param>
        /// <param name="configTrasform">Configuration transform to apply</param>
        public void Install(PackageInfo info, ICollection<string> repositories, Dictionary<string, string> parameters, string installScript, string configFile, string configTrasform)
        {
            Logger.Log(String.Format("Starting installation of {0} version {1} ... ", info.Name, info.Version));

            DownloadPackage(info, repositories, this.Logger);

            ApplyTransformations(info, configFile, configTrasform, this.Logger);

            ExecuteInstallScript(info, parameters, installScript, this.Logger);

            Logger.Log(String.Format("{0} version {1} Installed successfully ", info.Name, info.Version));
        }
    }
}
