using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task3
{
    [Serializable]
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException() : base() { }
        public EntityAlreadyExistsException(string message) : base(message) { }
        public EntityAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
        protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
