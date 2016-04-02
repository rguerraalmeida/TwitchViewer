using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Services.EventAggregator
{
    public interface ISubscription<TMessage> : IDisposable where TMessage : IMessage
    {
        Action<TMessage> Action { get; }
        IEventAggregator EventAggregator { get; }
    }

}
