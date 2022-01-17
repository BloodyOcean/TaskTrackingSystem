using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BLL.Validation
{
    /// <summary>
    /// Custom exception class
    /// </summary>
    public class TaskTrackingException : Exception
    {
        public TaskTrackingException()
        {
        }

        public TaskTrackingException(string message) : base(message)
        {
        }

        public TaskTrackingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TaskTrackingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
