namespace Bazooka.Core
{
    using System;
    using System.Globalization;
    using System.Management.Automation.Host;
    using System.Threading;

    /// <summary>
    ///     Fake host to execute powershell script
    /// </summary>
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
}
