using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Models.Communications
{
    [Serializable]
    public class MessageError
    {
        public System.Type ExceptionType { get; set; }
        public System.Exception Exception { get; set; }
        public string Message { get; set; }

        public MessageError(string message)
        {
            Message = message;
        }

        public MessageError(Type type, Exception exception, string message)
        {
            ExceptionType = type;
            Exception = exception;
            Message = message;
        }
    }
}
