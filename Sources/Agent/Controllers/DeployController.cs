namespace Agent.Controllers
{
    using Bazooka.Core;
    using Bazooka.Core.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class DeployController : ApiController
    {
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

                PackageInstaller.Install(packageInfo, new string[] { installOptions.Repository }, installOptions.AdditionalParameters);

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
                packageRemover.Remove(packageInfo, new string[] { uninstallOptions.Directory }, uninstallOptions.AdditionalParameters);

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
