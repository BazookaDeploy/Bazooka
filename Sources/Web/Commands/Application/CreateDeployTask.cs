using DataAccess.Write;
using System.Collections.Generic;

namespace Web.Commands
{
    public class CreateDeployTask : ICommand
    {
        public virtual int EnviromentId { get; set; }

        public virtual int ApplicationId { get; set; }

        public virtual string Name { get; set; }

        public virtual int AgentId { get; set; }

        public virtual string PackageName { get; set; }

        public virtual string Directory { get; set; }

        public virtual string Repository { get; set; }

        public virtual string CurrentlyDeployedVersion { get; set; }

        public virtual string InstallScript { get; set; }

        public virtual string UninstallScript { get; set; }

        public virtual string ConfigurationFile { get; set; }

        public virtual string ConfigurationTransform { get; set; }

        public virtual string Configuration { get; set; }

        public virtual IList<Parameter> AdditionalParameters { get; set; }
    }
}
