using System;
using System.Runtime.Serialization;
using Crankshaft.Logging;

#nullable enable

namespace Crankshaft.Exceptions
{
    [Serializable]
    internal class SceneNotFoundException : Exception
    {
        public SceneNotFoundException()
        {
        }

        public SceneNotFoundException(string message) : base(message)
        {
        }

        public SceneNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SceneNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}