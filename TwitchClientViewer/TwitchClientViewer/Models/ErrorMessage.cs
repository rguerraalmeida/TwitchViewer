using System;

namespace TwitchClientViewer.Models
{
    //{"error":"Not Found","status":404,"message":"User 'usernamexpto' does not exist"}
    public class RequestErrorMessage
    {
        public Int16 Status;
        public string Error;
        public string Message;

        public RequestErrorMessage(Int16 status, string error, string message)
        {
            this.Status = status;
            this.Error = error;
            this.Message = message;
        }
    }
}
