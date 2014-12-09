namespace Agent
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AgentSmithProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.AgentSmithInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // AgentSmithProcessInstaller
            // 
            this.AgentSmithProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.AgentSmithProcessInstaller.Password = null;
            this.AgentSmithProcessInstaller.Username = null;
            // 
            // AgentSmithInstaller
            // 
            this.AgentSmithInstaller.DelayedAutoStart = true;
            this.AgentSmithInstaller.Description = "Bazooka deploy agent";
            this.AgentSmithInstaller.DisplayName = "Agent Smith";
            this.AgentSmithInstaller.ServiceName = "AgentSmith";
            this.AgentSmithInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.AgentSmithProcessInstaller,
            this.AgentSmithInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller AgentSmithProcessInstaller;
        private System.ServiceProcess.ServiceInstaller AgentSmithInstaller;
    }
}