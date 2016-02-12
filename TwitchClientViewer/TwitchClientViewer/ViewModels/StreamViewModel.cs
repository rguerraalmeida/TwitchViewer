using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TwitchClientViewer.ViewModels
{
    public class StreamViewModel : BindableBase
    {
        public StreamViewModel()
        {
            this.SelectedLiveStream = new LiveStreamViewModel();
            this.LiveStreams = new ObservableCollection<LiveStreamViewModel>();
        }

        public ObservableCollection<LiveStreamViewModel> LiveStreams { get; set; }
        public LiveStreamViewModel SelectedLiveStream { get; set; }
    }
}
