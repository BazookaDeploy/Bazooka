namespace Web.Commands
{
    using System.Collections.Generic;

    public class CreateNewTemplatedTaskVersion : ICanBeRunOnlyByAdministrator
    {
        public int TemplatedTaskId { get; set; }

        public string Script { get; set; }

        public ICollection<VersionedParameter> Parameters { get; set; }
    }

    public class VersionedParameter
    {
        /// <summary>
        ///     name of the parameter
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Indicates if a parameter is optional
        /// </summary>
        public virtual bool Optional { get; set; }

        /// <summary>
        ///     Indicates if a parameter has to be Encrypted
        /// </summary>
        public virtual bool Encrypted { get; set; }
    }
}