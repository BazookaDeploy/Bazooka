using Bazooka.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazooka.CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            string invokedVerb = "";
            object invokedVerbInstance = null;

            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options,
              (verb, subOptions) =>
              {
                  // if parsing succeeds the verb name and correct instance
                  // will be passed to onVerbCommand delegate (string,object)
                  invokedVerb = verb;
                  invokedVerbInstance = subOptions;
              }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            switch (invokedVerb)
            {
                case "install":
                    {
                        var packageInfo = new PackageInfo();

                        packageInfo.Configuration = ((InstallOptions)invokedVerbInstance).Configuration;
                        packageInfo.InstallationDirectory = ((InstallOptions)invokedVerbInstance).Directory;
                        packageInfo.Name = ((InstallOptions)invokedVerbInstance).Application;
                        packageInfo.Version = ((InstallOptions)invokedVerbInstance).Version;

                        var PackageInstaller = new PackageInstaller();
                        PackageInstaller.Logger = new ConsoleLogger();
                        PackageInstaller.Install(packageInfo, new string[] { ((InstallOptions)invokedVerbInstance).Repository });

                    }
                    break;
                case "uninstall":
                    {
                        var packageInfo = new PackageInfo();
                        packageInfo.Name = ((UninstallOptions)invokedVerbInstance).Application;
                        packageInfo.Version = ((UninstallOptions)invokedVerbInstance).Version;

                        var packageRemover = new PackageRemover();
                        packageRemover.Logger = new ConsoleLogger();
                        packageRemover.Remove(packageInfo, new string[] { ((InstallOptions)invokedVerbInstance).Repository });
                    }
                    break;
            }
        }
    }
}
