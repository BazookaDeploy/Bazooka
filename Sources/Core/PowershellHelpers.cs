namespace Bazooka.Core
{
    using System.IO;
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;

    public static class PowershellHelpers
    {
        public static void Execute(string folder, string file, string configuration)
        {
            RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
            Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
            runspace.Open();
            runspace.SessionStateProxy.Path.SetLocation(folder);

            RunspaceInvoke scriptInvoker = new RunspaceInvoke(runspace);
            scriptInvoker.Invoke("Set-ExecutionPolicy Unrestricted");
            Pipeline pipeline = runspace.CreatePipeline();

            Command myCommand = new Command(Path.Combine(folder, file));
            CommandParameter testParam = new CommandParameter("-configuration", configuration);
            myCommand.Parameters.Add(testParam);
            pipeline.Commands.Add(myCommand);

            pipeline.Invoke();
        }
    }
}
