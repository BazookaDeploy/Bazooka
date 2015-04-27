using Microsoft.Web.XmlTransform;
using NuGet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazooka.Core
{

    /// <summary>
    ///     Default PackageInstaller implementation
    /// </summary>
    public class PackageInstaller : IPackageInstaller
    {
        /// <summary>
        ///     
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///     Installs the specified package into the specified directory
        /// </summary>
        /// <param name="info"></param>
        public void Install(PackageInfo info, ICollection<string> repositories, Dictionary<string, string> parameters)
        {
            DownloadPackage(info, repositories);

            ApplyTransformations(info, null,null);

            ExecuteInstallScript(info, parameters,null);

            Logger.Log("Application installed");
        }

        /// <summary>
        ///     Searches for an install script and executes it passing the configuration as parameter
        /// </summary>
        /// <param name="info">Pacakge installation informations</param>
        private void ExecuteInstallScript(PackageInfo info, Dictionary<string, string> parameters, string installScript)
        {
            if (installScript != null)
            {
                PowershellHelpers.ExecuteScript(info.InstallationDirectory, installScript, Logger, parameters);
            }

            var file = Path.Combine(info.InstallationDirectory, "install.ps1");

            if (File.Exists(file))
            {
                Logger.Log("Executing install script ... ");
                PowershellHelpers.Execute(info.InstallationDirectory, "install.ps1", info.Configuration, Logger, parameters);
            }
        }

        /// <summary>
        ///     Searches for a suitable config transformation and applies it 
        /// </summary>
        /// <param name="info">Package info</param>
        private void ApplyTransformations(PackageInfo info, string configFile, string transform)
        {
            if (configFile != null && transform != null)
            {
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
                                Logger.Log("Transormation applied ");
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


            var files = Directory.GetFiles(info.InstallationDirectory, "*.config")
                                 .Where(file => File.Exists(file.Replace(".config", "." + info.Configuration + ".config")));

            foreach (var file in files)
            {
                Logger.Log("Applying transformation ...");

                StringBuilder sb = new StringBuilder();
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
                                Logger.Log("Transormation applied ");
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
        private void DownloadPackage(PackageInfo info, ICollection<string> repositories)
        {
            Logger.Log("Downloading package ... ");

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

            // clear the zip cache so it will not grow too  much
            OptimizedZipPackage.PurgeCache();

            Logger.Log("Package downloaded ");
        }


        public void Install(PackageInfo info, ICollection<string> repositories, Dictionary<string, string> parameters, string installScript, string configFile, string configTrasform)
        {
            {
                DownloadPackage(info, repositories);

                ApplyTransformations(info, configFile, configTrasform);

                ExecuteInstallScript(info, parameters, installScript);

                Logger.Log("Application installed");
            }
        }
    }
}
