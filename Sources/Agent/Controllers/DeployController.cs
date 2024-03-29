﻿namespace Agent.Controllers
{
    using Bazooka.Core;
    using Bazooka.Core.Dto;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Http;

    public class DeployController : ApiController
    {
        [HttpPost]
        public ExecutionResult Update(InstallDto installOptions)
        {
            var logger = new StringLogger();
            try
            {
                var files = Directory.GetFiles(installOptions.Directory, "*.nupkg");

                if (files.Count() == 0 )
                {
                    logger.Log("No package to uninstall found, proceeding with a normal install.");
                }

                if (files.Count() >= 1)
                {
                    var filename = PackageHelpers.ExtractPackageName(files.First()).Trim();
                    var packageInfo = new PackageInfo()
                    {
                        Name = filename.Substring(filename.LastIndexOf("\\") + 1),
                        Version = PackageHelpers.ExtractPackageVersion(files.First()).Trim(),
                        InstallationDirectory = installOptions.Directory,
                        Configuration = installOptions.Configuration
                    };
                    var packageRemover = new PackageRemover();
                    packageRemover.Logger = logger;

                    var additionalParams = installOptions.AdditionalParameters;
                    packageRemover.Remove(packageInfo, new string[] { installOptions.Directory }, additionalParams, installOptions.UninstallScript);
                }


                var packageInfo2 = new PackageInfo()
                {
                    Configuration = installOptions.Configuration,
                    InstallationDirectory = installOptions.Directory,
                    Name = installOptions.Application,
                    Version = installOptions.Version
                };

                var PackageInstaller = new PackageInstaller();
                PackageInstaller.Logger = logger;
                PackageInstaller.Install(packageInfo2, new string[] { installOptions.Repository }, installOptions.AdditionalParameters, installOptions.InstallScript, installOptions.ConfigurationFile, installOptions.ConfigurationTransform);
                return new ExecutionResult()
                {
                    Success = logger.Logs.All(x => !x.Error),
                    Log = logger.Logs
                };
            }
            catch (Exception e)
            {
                logger.Log(e.InnerException != null ? e.InnerException.Message : e.Message, true);

                return new ExecutionResult()
                {
                    Success = false,
                    Exception = e.InnerException != null ? e.InnerException.Message : e.Message,
                    Log = logger.Logs
                };
            }
        }

        [HttpPost]
        public ExecutionResult Install(InstallDto installOptions)
        {
            var logger = new StringLogger();
            try
            {
                var packageInfo = new PackageInfo();

                packageInfo.Configuration = installOptions.Configuration;
                packageInfo.InstallationDirectory = installOptions.Directory;
                packageInfo.Name = installOptions.Application;
                packageInfo.Version = installOptions.Version;
                var PackageInstaller = new PackageInstaller();
                PackageInstaller.Logger = logger;

                PackageInstaller.Install(packageInfo, new string[] { installOptions.Repository }, installOptions.AdditionalParameters, installOptions.InstallScript, installOptions.ConfigurationFile, installOptions.ConfigurationTransform);

                return new ExecutionResult()
                {
                    Success = logger.Logs.All(x => !x.Error),
                    Log = logger.Logs
                };
            }
            catch (Exception e)
            {
                logger.Log(e.InnerException != null ? e.InnerException.Message : e.Message, true);
                return new ExecutionResult()
                {
                    Success = false,
                    Exception = e.InnerException != null ? e.InnerException.Message : e.Message,
                    Log = logger.Logs
                };
            }
        }

        /// <summary>
        ///     Cleans all the packages caches.
        ///     Necessary and called every night by the controller
        ///     as nuget tends to use a lot of space when decompressing files
        /// </summary>
        [HttpGet]
        public void Clean()
        {
            PackageCleaner.Clean();
        }

        [HttpPost]
        public ExecutionResult Uninstall(UninstallDto uninstallOptions)
        {
            var logger = new StringLogger();
            try
            {
                var packageInfo = new PackageInfo();
                packageInfo.Name = uninstallOptions.Application;
                packageInfo.Version = uninstallOptions.Version;
                var packageRemover = new PackageRemover();
                packageRemover.Logger = logger;
                packageRemover.Remove(packageInfo, new string[] { uninstallOptions.Directory }, uninstallOptions.AdditionalParameters, uninstallOptions.UninstallScript);

                return new ExecutionResult()
                {
                    Success = logger.Logs.All(x => !x.Error),
                    Log = logger.Logs
                };
            }
            catch (Exception e)
            {
                logger.Log(e.InnerException != null ? e.InnerException.Message : e.Message, true);
                return new ExecutionResult()
                {
                    Success = false,
                    Exception = e.InnerException != null ? e.InnerException.Message : e.Message,
                    Log = logger.Logs
                };
            }
        }

        [HttpPost]
        public ExecutionResult ExecuteScript(RemoteScriptDto options)
        {
            var logger = new StringLogger();
            try
            {
                PowershellHelpers.ExecuteScript(options.Folder, options.Script, logger, new Dictionary<string, string>());
            }
            catch (Exception e)
            {
                return new ExecutionResult()
                {
                    Exception = e.InnerException == null ? e.Message : e.InnerException.Message,
                    Success = false,
                    Log = logger.Logs
                };
            }
            return new ExecutionResult()
            {
                Success = logger.Logs.All(x => !x.Error),
                Log = logger.Logs
            };
        }

        [HttpPost]
        public ExecutionResult ExecuteTemplatedTask(TemplatedTaskDto options)
        {
            var logger = new StringLogger();
            try
            {
                PowershellHelpers.ExecuteScript(".", options.Script, logger, options.Parameters.ToDictionary( x=> x.Name, x => x.Value));
            }
            catch (Exception e)
            {
                return new ExecutionResult()
                {
                    Exception = e.InnerException == null ? e.Message : e.InnerException.Message,
                    Success = false,
                    Log = logger.Logs
                };
            }
            return new ExecutionResult()
            {
                Success = logger.Logs.All(x => !x.Error),
                Log = logger.Logs
            };
        }


        [HttpPost]
        public ExecutionResult DatabaseDeploy(DatabaseDeployDto options)
        {
            var logger = new StringLogger();
            try
            {
                var stream = PackageHelpers.DownloadDacpac(options.PackageName, options.Version, options.Repository);
                DacpacHelpers.ApplyDacpac(stream, options.ConnectionString, options.DataBase, logger, options.Partial);
            }
            catch (Exception e)
            {
                return new ExecutionResult()
                {
                    Exception = e.InnerException == null ? e.Message : e.InnerException.Message,
                    Success = false,
                    Log = logger.Logs
                };
            }
            return new ExecutionResult()
            {
                Success = logger.Logs.All(x => !x.Error),
                Log = logger.Logs
            };
        }
    }
}
