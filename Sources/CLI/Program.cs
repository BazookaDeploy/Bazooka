using Bazooka.Core;
using System;
using System.Collections.Generic;
using System.IO;
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
            var result = CommandLine.Parser.Default.ParseArgumentsStrict(args, options,
              (verb, subOptions) =>
              {
                  // if parsing succeeds the verb name and correct instance
                  // will be passed to onVerbCommand delegate (string,object)
                  invokedVerb = verb;
                  invokedVerbInstance = subOptions;
              });



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
                        var additionalParams = ((InstallOptions)invokedVerbInstance).AdditionalParameters;
                        var paramDictionary = new Dictionary<string, string>();

                        if (additionalParams.Length > 0)
                        {
                            paramDictionary = additionalParams.ToDictionary(x => x.Substring(0, x.IndexOf("=") - 1), x => x.Substring( x.IndexOf("=")+1));
                        }

                        PackageInstaller.Install(packageInfo, new string[] { ((InstallOptions)invokedVerbInstance).Repository }, paramDictionary);

                    }
                    break;
                case "blast":
                    {
                        //serach directory for packages
                        var files = Directory.GetFiles(((BlastOptions)invokedVerbInstance).Directory, "*.nupkg");

                        if (files.Count() == 0)
                        {
                            Console.WriteLine("No package found");
                            break;
                        }


                        if (files.Count() > 1)
                        {
                            Console.WriteLine("More than one package present");
                            break;
                        }

                        var packageInfo = new PackageInfo();
                        var filename = PackageHelpers.ExtractPackageName(files.First()).Trim();
                        packageInfo.Name = filename.Substring(filename.LastIndexOf("\\") + 1);
                        packageInfo.Version = PackageHelpers.ExtractPackageVersion(files.First());
                        packageInfo.InstallationDirectory = ((BlastOptions)invokedVerbInstance).Directory;
                        packageInfo.Configuration = "";

                        var packageRemover = new PackageRemover();
                        packageRemover.Logger = new ConsoleLogger();

                        var additionalParams = ((BlastOptions)invokedVerbInstance).AdditionalParameters;
                        var paramDictionary = new Dictionary<string, string>();

                        if (additionalParams.Length > 0)
                        {
                            paramDictionary = additionalParams.ToDictionary(x => x.Substring(0, x.IndexOf("=") - 1), x => x.Substring(x.IndexOf("=") + 1));
                        }

                        packageRemover.Remove(packageInfo, new string[] { ((BlastOptions)invokedVerbInstance).Directory }, paramDictionary);
                        // if multiples found erro
                        //uninstall the remaining one

                    }
                    break;
                case "update":
                    {
                        //serach directory for packages
                        var files = Directory.GetFiles(((UpdateOptions)invokedVerbInstance).Directory, "*.nupkg");

                        if (files.Count() == 0)
                        {
                            Console.WriteLine("No package found");
                            break;
                        }


                        if (files.Count() > 1)
                        {
                            Console.WriteLine("More than one package present");
                            break;
                        }

                        var packageInfo = new PackageInfo();
                        var filename = PackageHelpers.ExtractPackageName(files.First()).Trim();
                        packageInfo.Name = filename.Substring(filename.LastIndexOf("\\")+1);
                        packageInfo.Version = PackageHelpers.ExtractPackageVersion(files.First()).Trim();
                        packageInfo.InstallationDirectory = ((UpdateOptions)invokedVerbInstance).Directory;
                        packageInfo.Configuration = ((UpdateOptions)invokedVerbInstance).Configuration;     

                        var packageRemover = new PackageRemover();
                        packageRemover.Logger = new ConsoleLogger();

                        var additionalParams = ((UpdateOptions)invokedVerbInstance).AdditionalParameters;
                        var paramDictionary = new Dictionary<string, string>();

                        if (additionalParams.Length > 0)
                        {
                            paramDictionary = additionalParams.ToDictionary(x => x.Substring(0, x.IndexOf("=") - 1), x => x.Substring(x.IndexOf("=") + 1));
                        }

                        packageRemover.Remove(packageInfo, new string[] { ((UpdateOptions)invokedVerbInstance).Directory }, paramDictionary);

                        var packageInfo2 = new PackageInfo();

                        packageInfo2.Configuration = ((UpdateOptions)invokedVerbInstance).Configuration;
                        packageInfo2.InstallationDirectory = ((UpdateOptions)invokedVerbInstance).Directory;
                        packageInfo2.Name = ((UpdateOptions)invokedVerbInstance).Application;
                        packageInfo2.Version = ((UpdateOptions)invokedVerbInstance).Version;

                        var PackageInstaller = new PackageInstaller();
                        PackageInstaller.Logger = new ConsoleLogger();
                        var additionalParams2 = ((UpdateOptions)invokedVerbInstance).AdditionalParameters;
                        var paramDictionary2 = new Dictionary<string, string>();

                        if (additionalParams2.Length > 0)
                        {
                            paramDictionary2 = additionalParams2.ToDictionary(x => x.Substring(0, x.IndexOf("=") - 1), x => x.Substring(x.IndexOf("=") + 1));
                        }

                        PackageInstaller.Install(packageInfo2, new string[] { ((UpdateOptions)invokedVerbInstance).Repository }, paramDictionary2);

                    }
                    break;
                case "uninstall":
                    {
                        var packageInfo = new PackageInfo();
                        packageInfo.Name = ((UninstallOptions)invokedVerbInstance).Application;
                        packageInfo.Version = ((UninstallOptions)invokedVerbInstance).Version;
                        packageInfo.InstallationDirectory = ((UninstallOptions)invokedVerbInstance).Directory;
                        packageInfo.Configuration = ((UninstallOptions)invokedVerbInstance).Configuration;

                        var packageRemover = new PackageRemover();
                        packageRemover.Logger = new ConsoleLogger();

                        var additionalParams = ((UninstallOptions)invokedVerbInstance).AdditionalParameters;
                        var paramDictionary = new Dictionary<string, string>();

                        if (additionalParams.Length > 0)
                        {
                            paramDictionary = additionalParams.ToDictionary(x => x.Substring(0, x.IndexOf("=") - 1), x => x.Substring(x.IndexOf("=") + 1));
                        }

                        packageRemover.Remove(packageInfo, new string[] { ((UninstallOptions)invokedVerbInstance).Directory }, paramDictionary);
                    }
                    break;
            }
        }
    }
}
