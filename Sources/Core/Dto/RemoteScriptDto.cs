namespace Bazooka.Core.Dto
{
    /// <summary>
    ///     Dto for a remote script execution
    /// </summary>
    public class RemoteScriptDto
    {
        /// <summary>
        ///     Script to execute
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        ///     Folder where to execute the script
        /// </summary>
        public string Folder { get; set; }
    }
}
