namespace Bazooka.Core
{
    using System;

    /// <summary>
    ///     Exceptions that indicate a specific pacckage version does not exists
    /// </summary>
    public class InexistentPackageException : Exception
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="message">Exception message that contains package id and version</param>
        public InexistentPackageException(string message)
            : base(message)
        {
        }
    }
}
