namespace Agent.Controllers
{
    using Bazooka.Core;
    using Bazooka.Core.Dto;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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

                if (files.Count() == 0)
                {
                    return new ExecutionResult()
                    {
                        Success = false,
                        Exception = "no package found",
                        Log = logger.Logs
                    };
                }


                if (files.Count() > 1)
                {
                    return new ExecutionResult()
                    {
                        Success = false,
                        Exception = "multiple packages found",
                        Log = logger.Logs
                    };
                }

                var packageInfo = new PackageInfo();
                var filename = PackageHelpers.ExtractPackageName(files.First()).Trim();
                packageInfo.Name = filename.Substring(filename.LastIndexOf("\\") + 1);
                packageInfo.Version = PackageHelpers.ExtractPackageVersion(files.First()).Trim();
                packageInfo.InstallationDirectory = installOptions.Directory;
                packageInfo.Configuration = installOptions.Configuration;

                var packageRemover = new PackageRemover();
                packageRemover.Logger = logger;

                var additionalParams = installOptions.AdditionalParameters;
                packageRemover.Remove(packageInfo, new string[] { installOptions.Directory }, additionalParams,installOptions.UninstallScript);



                var packageInfo2 = new PackageInfo();
                packageInfo2.Configuration = installOptions.Configuration;
                packageInfo2.InstallationDirectory = installOptions.Directory;
                packageInfo2.Name = installOptions.Application;
                packageInfo2.Version = installOptions.Version;
                var PackageInstaller = new PackageInstaller();
                PackageInstaller.Logger = logger;
                PackageInstaller.Install(packageInfo2, new string[] { installOptions.Repository }, installOptions.AdditionalParameters, installOptions.InstallScript, installOptions.ConfigurationFile, installOptions.ConfigurationTransform);
                return new ExecutionResult()
                {
                    Success = true,
                    Log = logger.Logs
                };
            }
            catch (Exception e)
            {
                return new ExecutionResult()
                {
                    Success = false,
                    Exception = e.Message,
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
                    Success = true,
                    Log = logger.Logs
                };
            }
            catch (Exception e)
            {
                return new ExecutionResult()
                {
                    Success = false,
                    Exception = e.Message,
                    Log = logger.Logs
                };
            }
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
                    Success = true,
                    Log = logger.Logs
                };
            }
            catch (Exception e)
            {
                return new ExecutionResult()
                {
                    Success = false,
                    Exception = e.Message,
                    Log = logger.Logs
                };
            }
        }

    }
}
