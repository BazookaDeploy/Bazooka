namespace DataAccess.Write
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     A template for a task
    /// </summary>
    public class TaskTemplate
    {
        /// <summary>
        ///     Template identifier
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     name of the template
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     An optional description of the template
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        ///     List of parameters
        /// </summary>
        public virtual IList<TaskTemplateVersion> Versions { get; set; }

        /// <summary>
        ///     Creates a new templated task
        /// </summary>
        /// <param name="name">Name of the task</param>
        /// <param name="description">Description</param>
        public virtual void Create(string name, string description)
        {
            this.Name = name;
            this.Description = description;
            Versions.Add(new TaskTemplateVersion()
            {
                Script = string.Empty,
                Version = 1
            });
        }

        /// <summary>
        ///     Changes the description of the task
        /// </summary>
        /// <param name="description">DEscription of the task</param>
        public virtual void ChangeDescription(string description)
        {
            this.Description = description;
        }

        /// <summary>
        ///     Renames a task template
        /// </summary>
        /// <param name="name">New name of the task</param>
        public virtual void Rename(string name)
        {
            this.Name = name;
        }

        /// <summary>
        ///     Creates a new version of this task template
        /// </summary>
        /// <param name="script">New script</param>
        /// <param name="parameters">New set of parameters</param>
        public virtual void CreateNewVersion(string script, ICollection<TaskTemplateParameter> parameters)
        {
            this.Versions.Add(new TaskTemplateVersion()
            {
                TaskTemplateId = this.Id,
                Version = this.Versions.Last().Version + 1,
                Script = script,
                Parameters = parameters.ToList()
            });
        }
    }
}
