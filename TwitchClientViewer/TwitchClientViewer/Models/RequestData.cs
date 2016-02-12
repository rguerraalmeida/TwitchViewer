using System;

namespace TwitchClientViewer.Models
{
    public class RequestData<T>
    {
        public bool HasError = false;
        public T Data = default(T);
        public RequestErrorMessage ErrorMessage = null;

        public RequestData() {}

        public RequestData(T data)
        {
            this.Data = data;
        }

        public RequestData(RequestErrorMessage errorMessage)
        {
            this.ErrorMessage = errorMessage;
            this.HasError = true;
        }

        public RequestData(T data, RequestErrorMessage errorMessage)
        {
            this.Data = data;
            this.ErrorMessage = errorMessage;
            this.HasError = true;
        }
    }
}