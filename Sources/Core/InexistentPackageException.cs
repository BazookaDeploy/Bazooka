using System;

namespace Bazooka.Core
{
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
