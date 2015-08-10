using System;

namespace Bazooka.Core
{

    /// <summary>
    ///     Exception thrown when unable to apply config transformations
    /// </summary>
    [Serializable]
    public class TransformException : Exception
    {
        /// <summary>
        ///     Default exception constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public TransformException(string message) : base(message) { }
    }
}
