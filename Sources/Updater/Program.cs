namespace Updater
{
    using Ionic.Zip;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var cwd = Environment.CurrentDirectory;

            //stop the agent service
            RunProcess(" /c  \"Agent.exe stop\" ");

            Console.WriteLine("agent stopped");

            Console.WriteLine(cwd);

            foreach (var file in Directory.EnumerateFiles(cwd).Where(x => !x.Contains("update.zip") && !x.Contains("Updater.exe") && !x.Contains("Ionic.Zip.dll"))){
                Console.WriteLine("deleteing "+file);
                File.Delete(file);
            }

            //extract the new version
            using (var zip = new ZipFile(Path.Combine(cwd, "update.zip")))
            {
                zip.TempFileFolder = System.IO.Path.GetTempPath();
                zip.ExtractAll(cwd, ExtractExistingFileAction.OverwriteSilently);
            }

            Console.WriteLine("files extracted");

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
                UseShellExecute = true,
                CreateNoWindow = false,
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
