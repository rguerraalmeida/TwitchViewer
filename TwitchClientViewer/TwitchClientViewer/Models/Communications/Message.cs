using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Models.Communications
{
    [Serializable]
    public class Message<T>
    {
        public MessageState MessageState { get; set; }
        public MessageError Error { get; set; }
        public T Data { get; set; }

        public Message(MessageState state, T data) : this(state, null, data) { }

        public Message(MessageState state, MessageError error) : this(state, error, default(T)) { }

        private Message(MessageState state, MessageError error, T data)
        {
            MessageState = state;
            Error = error;
            Data = data;
        }
    }
}