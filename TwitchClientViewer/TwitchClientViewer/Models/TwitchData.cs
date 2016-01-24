using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer.Models
{
    public class TwitchData<T>
    {
        public bool HasError = false;
        public T Data = default(T);
        public ErrorMessage ErrorMessage = null;

        public TwitchData() {}

        public TwitchData(T data)
        {
            this.Data = data;
        }

        public TwitchData(ErrorMessage errorMessage)
        {
            this.ErrorMessage = errorMessage;
            this.HasError = true;
        }

        public TwitchData(T data, ErrorMessage errorMessage)
        {
            this.Data = data;
            this.ErrorMessage = errorMessage;
            this.HasError = true;
        }
    }
}
