namespace Bazooka.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;

    /// <summary>
    ///     Helpers for powershell script
    /// </summary>
    public static class PowershellHelpers
    {
        /// <summary>
        ///     Executes a powershell script
        /// </summary>
        /// <param name="folder">Folder where to execute the script</param>
        /// <param name="file">Script to execute</param>
        /// <param name="configuration">Configuration used</param>
        /// <param name="log">Logger to use</param>
        /// <param name="parameters">Parameters for the script</param>
        public static void Execute(string folder, string file, string configuration, ILogger log, Dictionary<string, string> parameters)
        {
            RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
            Runspace runspace = RunspaceFactory.CreateRunspace(new Host(), runspaceConfiguration);
            runspace.Open();
            runspace.SessionStateProxy.Path.SetLocation(folder);

            RunspaceInvoke scriptInvoker = new RunspaceInvoke(runspace);
            scriptInvoker.Invoke("Set-ExecutionPolicy Unrestricted");
            Pipeline pipeline = runspace.CreatePipeline();

            Command myCommand = new Command(Path.Combine(folder, file));
     
            foreach (var param in parameters.Keys)
            {
                myCommand.Parameters.Add(new CommandParameter("-" + param, parameters[param]));
            }

            myCommand.Parameters.Add(new CommandParameter("-Verb", "RunAs"));

            pipeline.Commands.Add(myCommand);

            Collection<PSObject> results = new Collection<PSObject>();
            try
            {
                results = pipeline.Invoke();
            }
            catch (RuntimeException e)
            {
                log.Log(String.Join("\r\n", results.Select(x => x.ToString())) + "\r\n" + e.Message.ToString(), true);
                return;
            }

            log.Log(String.Join("\r\n", results.Select(x => x.ToString())), pipeline.Error.Count > 0);
        }

        /// <summary>
        ///     Executes a powershell script
        /// </summary>
        /// <param name="folder">Folder where to execute the script</param>
        /// <param name="script">Script to execute</param>
        /// <param name="log">Logger to use</param>
        /// <param name="parameters">Parameters for the script</param>
        public static void ExecuteScript(string folder, string script, ILogger log, Dictionary<string, string> parameters)
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.Runspace.SessionStateProxy.Path.SetLocation(folder);
                // use "AddScript" to add the contents of a script file to the end of the execution pipeline.
                // use "AddCommand" to add individual commands/cmdlets to the end of the execution pipeline.
                PowerShellInstance.AddScript(script);
                
                foreach (var param in parameters.Keys)
                {
                    PowerShellInstance.AddParameter(param, parameters[param]);
                }

                PowerShellInstance.AddParameter("Verb", "RunAs");

                Collection<PSObject> results = new Collection<PSObject>();
                try {
                    results = PowerShellInstance.Invoke();
                }catch(RuntimeException e)
                {
                    log.Log(String.Join("\r\n", results.Select(x => x.ToString())) + "\r\n"+e.Message.ToString(), true);
                    return;
                }
                
                log.Log(String.Join("\r\n", results.Select(x => x.ToString())), PowerShellInstance.Streams.Error.Count >0);
            }
        }

        /// <summary>
        ///     Determines if a powershell script is valid( at least sintactically)
        /// </summary>
        /// <param name="script">Powershell script to parse</param>
        /// <returns>Script validity</returns>
        public static bool Validate(string script)
        {
            var list = new Collection<PSParseError>();
            var result = System.Management.Automation.PSParser.Tokenize(script, out list);
            return list.Count == 0;
        }

        /// <summary>
        ///     Get list of parse errors in a powershell script
        /// </summary>
        /// <param name="script">Script to parse</param>
        /// <returns>List of errors</returns>
        public static ICollection<string> GetParseErrors(string script)
        {
            var list = new Collection<PSParseError>();
            var result = System.Management.Automation.PSParser.Tokenize(script, out list);
            return list.Select(x => x.ToString()).ToList();
        }
    }
}

