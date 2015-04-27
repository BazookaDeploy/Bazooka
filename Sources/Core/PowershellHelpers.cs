namespace Bazooka.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Management.Automation;
    using System.Management.Automation.Host;
    using System.Management.Automation.Runspaces;
    using System.Security;
    using System.Text;
    using System.Threading;

    public static class PowershellHelpers
    {
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

            pipeline.Commands.Add(myCommand);

            var results = pipeline.Invoke();

            foreach (var item in results)
            {
                log.Log(item.ToString());
            }
        }

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

                var results = PowerShellInstance.Invoke();

                foreach (var item in results)
                {
                    log.Log(item.ToString());
                }
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

    class Host : PSHost
    {
        private PSHostUserInterface _psHostUserInterface = new HostUserInterface();

        public override void SetShouldExit(int exitCode)
        {
            Environment.Exit(exitCode);
        }

        public override void EnterNestedPrompt()
        {
            throw new NotImplementedException();
        }

        public override void ExitNestedPrompt()
        {
            throw new NotImplementedException();
        }

        public override void NotifyBeginApplication()
        {
        }

        public override void NotifyEndApplication()
        {
        }

        public override string Name
        {
            get { return "PSCX-PS1ToExeHost"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override Guid InstanceId
        {
            get { return new Guid("E4673B42-84B6-4C43-9589-95FAB8E00EB2"); }
        }

        public override PSHostUserInterface UI
        {
            get { return _psHostUserInterface; }
        }

        public override CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        public override CultureInfo CurrentUICulture
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
        }
    }

    class HostUserInterface : PSHostUserInterface, IHostUISupportsMultipleChoiceSelection
    {
        private PSHostRawUserInterface _psRawUserInterface = new HostRawUserInterface();

        public override PSHostRawUserInterface RawUI
        {
            get { return _psRawUserInterface; }
        }

        public override string ReadLine()
        {
            return Console.ReadLine();
        }

        public override SecureString ReadLineAsSecureString()
        {
            throw new NotImplementedException();
        }

        public override void Write(string value)
        {
            string output = value ?? "null";
            Console.Write(output);
        }

        public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            string output = value ?? "null";
            var origFgColor = Console.ForegroundColor;
            var origBgColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(output);
            Console.ForegroundColor = origFgColor;
            Console.BackgroundColor = origBgColor;
        }

        public override void WriteLine(string value)
        {
            string output = value ?? "null";
            Console.WriteLine(output);
        }

        public override void WriteErrorLine(string value)
        {
            string output = value ?? "null";
            var origFgColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(output);
            Console.ForegroundColor = origFgColor;
        }

        public override void WriteDebugLine(string message)
        {
            WriteYellowAnnotatedLine(message, "DEBUG");
        }

        public override void WriteVerboseLine(string message)
        {
            WriteYellowAnnotatedLine(message, "VERBOSE");
        }

        public override void WriteWarningLine(string message)
        {
            WriteYellowAnnotatedLine(message, "WARNING");
        }

        private void WriteYellowAnnotatedLine(string message, string annotation)
        {
            string output = message ?? "null";
            var origFgColor = Console.ForegroundColor;
            var origBgColor = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            WriteLine(String.Format(CultureInfo.CurrentCulture, "{0}: {1}", annotation, output));
            Console.ForegroundColor = origFgColor;
            Console.BackgroundColor = origBgColor;
        }

        public override void WriteProgress(long sourceId, ProgressRecord record)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
        {
            if (String.IsNullOrEmpty(caption) && String.IsNullOrEmpty(message) && descriptions.Count > 0)
            {
                Console.Write(descriptions[0].Name + ": ");
            }
            else
            {
                this.Write(ConsoleColor.DarkCyan, ConsoleColor.Black, caption + "\n" + message + " ");
            }
            var results = new Dictionary<string, PSObject>();
            foreach (FieldDescription fd in descriptions)
            {
                string[] label = GetHotkeyAndLabel(fd.Label);
                this.WriteLine(label[1]);
                string userData = Console.ReadLine();
                if (userData == null)
                {
                    return null;
                }

                results[fd.Name] = PSObject.AsPSObject(userData);
            }

            return results;
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
        {
            throw new NotImplementedException();
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName, PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options)
        {
            throw new NotImplementedException();
        }

        public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
        {
            // Write the caption and message strings in Blue.
            this.WriteLine(ConsoleColor.Blue, ConsoleColor.Black, caption + "\n" + message + "\n");

            // Convert the choice collection into something that is
            // easier to work with. See the BuildHotkeysAndPlainLabels 
            // method for details.
            string[,] promptData = BuildHotkeysAndPlainLabels(choices);

            // Format the overall choice prompt string to display.
            var sb = new StringBuilder();
            for (int element = 0; element < choices.Count; element++)
            {
                sb.Append(String.Format(CultureInfo.CurrentCulture, "|{0}> {1} ", promptData[0, element], promptData[1, element]));
            }

            sb.Append(String.Format(CultureInfo.CurrentCulture, "[Default is ({0}]", promptData[0, defaultChoice]));

            // Read prompts until a match is made, the default is
            // chosen, or the loop is interrupted with ctrl-C.
            while (true)
            {
                this.WriteLine(sb.ToString());
                string data = Console.ReadLine().Trim().ToUpper(CultureInfo.CurrentCulture);

                // If the choice string was empty, use the default selection.
                if (data.Length == 0)
                {
                    return defaultChoice;
                }

                // See if the selection matched and return the
                // corresponding index if it did.
                for (int i = 0; i < choices.Count; i++)
                {
                    if (promptData[0, i] == data)
                    {
                        return i;
                    }
                }

                this.WriteErrorLine("Invalid choice: " + data);
            }
        }

        #region IHostUISupportsMultipleChoiceSelection Members

        public Collection<int> PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, IEnumerable<int> defaultChoices)
        {
            this.WriteLine(ConsoleColor.Blue, ConsoleColor.Black, caption + "\n" + message + "\n");

            string[,] promptData = BuildHotkeysAndPlainLabels(choices);

            var sb = new StringBuilder();
            for (int element = 0; element < choices.Count; element++)
            {
                sb.Append(String.Format(CultureInfo.CurrentCulture, "|{0}> {1} ", promptData[0, element], promptData[1, element]));
            }

            var defaultResults = new Collection<int>();
            if (defaultChoices != null)
            {
                int countDefaults = 0;
                foreach (int defaultChoice in defaultChoices)
                {
                    ++countDefaults;
                    defaultResults.Add(defaultChoice);
                }

                if (countDefaults != 0)
                {
                    sb.Append(countDefaults == 1 ? "[Default choice is " : "[Default choices are ");
                    foreach (int defaultChoice in defaultChoices)
                    {
                        sb.AppendFormat(CultureInfo.CurrentCulture, "\"{0}\",", promptData[0, defaultChoice]);
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");
                }
            }

            this.WriteLine(ConsoleColor.Cyan, ConsoleColor.Black, sb.ToString());

            var results = new Collection<int>();
            while (true)
            {
            ReadNext:
                string prompt = string.Format(CultureInfo.CurrentCulture, "Choice[{0}]:", results.Count);
                this.Write(ConsoleColor.Cyan, ConsoleColor.Black, prompt);
                string data = Console.ReadLine().Trim().ToUpper(CultureInfo.CurrentCulture);

                if (data.Length == 0)
                {
                    return (results.Count == 0) ? defaultResults : results;
                }

                for (int i = 0; i < choices.Count; i++)
                {
                    if (promptData[0, i] == data)
                    {
                        results.Add(i);
                        goto ReadNext;
                    }
                }

                this.WriteErrorLine("Invalid choice: " + data);
            }
        }

        #endregion

        private static string[,] BuildHotkeysAndPlainLabels(Collection<ChoiceDescription> choices)
        {
            // Allocate the result array
            string[,] hotkeysAndPlainLabels = new string[2, choices.Count];

            for (int i = 0; i < choices.Count; ++i)
            {
                string[] hotkeyAndLabel = GetHotkeyAndLabel(choices[i].Label);
                hotkeysAndPlainLabels[0, i] = hotkeyAndLabel[0];
                hotkeysAndPlainLabels[1, i] = hotkeyAndLabel[1];
            }

            return hotkeysAndPlainLabels;
        }

        private static string[] GetHotkeyAndLabel(string input)
        {
            string[] result = new string[] { String.Empty, String.Empty };
            string[] fragments = input.Split('&');
            if (fragments.Length == 2)
            {
                if (fragments[1].Length > 0)
                {
                    result[0] = fragments[1][0].ToString().
                    ToUpper(CultureInfo.CurrentCulture);
                }

                result[1] = (fragments[0] + fragments[1]).Trim();
            }
            else
            {
                result[1] = input;
            }

            return result;
        }
    }

    class HostRawUserInterface : PSHostRawUserInterface
    {
        public override KeyInfo ReadKey(ReadKeyOptions options)
        {
            throw new NotImplementedException();
        }

        public override void FlushInputBuffer()
        {
        }

        public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
        {
            throw new NotImplementedException();
        }

        public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
        {
            throw new NotImplementedException();
        }

        public override BufferCell[,] GetBufferContents(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip, BufferCell fill)
        {
            throw new NotImplementedException();
        }

        public override ConsoleColor ForegroundColor
        {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        public override ConsoleColor BackgroundColor
        {
            get { return Console.BackgroundColor; }
            set { Console.BackgroundColor = value; }
        }

        public override Coordinates CursorPosition
        {
            get { return new Coordinates(Console.CursorLeft, Console.CursorTop); }
            set { Console.SetCursorPosition(value.X, value.Y); }
        }

        public override Coordinates WindowPosition
        {
            get { return new Coordinates(Console.WindowLeft, Console.WindowTop); }
            set { Console.SetWindowPosition(value.X, value.Y); }
        }

        public override int CursorSize
        {
            get { return Console.CursorSize; }
            set { Console.CursorSize = value; }
        }

        public override Size BufferSize
        {
            get { return new Size(Console.BufferWidth, Console.BufferHeight); }
            set { Console.SetBufferSize(value.Width, value.Height); }
        }

        public override Size WindowSize
        {
            get { return new Size(Console.WindowWidth, Console.WindowHeight); }
            set { Console.SetWindowSize(value.Width, value.Height); }
        }

        public override Size MaxWindowSize
        {
            get { return new Size(Console.LargestWindowWidth, Console.LargestWindowHeight); }
        }

        public override Size MaxPhysicalWindowSize
        {
            get { return new Size(Console.LargestWindowWidth, Console.LargestWindowHeight); }
        }

        public override bool KeyAvailable
        {
            get { return Console.KeyAvailable; }
        }

        public override string WindowTitle
        {
            get { return Console.Title; }
            set { Console.Title = value; }
        }
    }
}

