using System;
using System.Runtime.Serialization;

namespace Practice2.Controllers {
    [Serializable]
    internal class NotValidAccountTypeException : Exception {
        public NotValidAccountTypeException() {
        }

        public NotValidAccountTypeException(string message) : base(message) {
        }

        public NotValidAccountTypeException(string message, Exception innerException) : base(message, innerException) {
        }

        protected NotValidAccountTypeException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}