namespace Updater
{
    using Ionic.Zip;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var cwd = Environment.CurrentDirectory;

            //stop the agent service
            RunProcess(" /c  \"Agent.exe stop\" ");

            //extract the new version
            using (var zip = new ZipFile(Path.Combine(cwd, "update.zip")))
            {
                zip.ExtractAll(cwd, ExtractExistingFileAction.OverwriteSilently);
            }

            //clean update package
            System.IO.File.Delete(Path.Combine(cwd, "update.zip"));

            //start the agent service again
            RunProcess(" /c  \"Agent.exe start\" ");
        }

        private static void RunProcess(string command)
        {
            var cwd = Environment.CurrentDirectory;

            System.Diagnostics.ProcessStartInfo procStartInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = cwd,
                FileName = "cmd",
                Arguments = command,
            };

            System.Diagnostics.Process proc = new System.Diagnostics.Process()
            {
                StartInfo = procStartInfo,
                EnableRaisingEvents = true
            };

            proc.Start();
            proc.WaitForExit();
            proc.Dispose();
        }
    }
}
