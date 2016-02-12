using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TwitchClientViewer.ViewModels;

namespace TwitchClientViewer.Views.Following
{
    public class FollowingViewModel 
    {
        //PropertySupport.ExtractPropertyName(propertyExpression);
        public FollowingViewModel()
        {
            this.SelectedLiveStream = new LiveStreamViewModel();
            this.LiveStreams = new ObservableCollection<LiveStreamViewModel>();
            this.SortOrders = new ObservableCollection<Tuple<string, string>>()
            {
                new Tuple<string, string>("Alphabetically", ReflectionUtility.GetMemberInfo((LiveStreamViewModel c) => c.DisplayName).Name ),
                new Tuple<string, string>("Viewer Count", ReflectionUtility.GetMemberInfo((LiveStreamViewModel c) => c.ViewerCount).Name),
            };
        }

        public ObservableCollection<LiveStreamViewModel> LiveStreams { get; set; }
        public LiveStreamViewModel SelectedLiveStream { get; set; }
        public ObservableCollection<Tuple<string, string>> SortOrders { get; set; }
        public string SelectedSortOrder { get; set; }
        public ObservableCollection<string> StreamsFilters { get; set; }
        public CollectionView LiveStreamsCollection { get; private set; }
        public string DisplayType { get; set; }



        private void Sort(object item)
        {
            if (String.IsNullOrEmpty(SelectedSortOrder))
                return;

            var selected = (string)item;
            var sortOrder = this.SortOrders.Where(w => w.Item1 == selected).First();

            LiveStreamsCollection = (CollectionView)new CollectionViewSource { Source = LiveStreams }.View;
            LiveStreamsCollection.SortDescriptions.Clear();
            LiveStreamsCollection.SortDescriptions.Add(new SortDescription(sortOrder.Item2, ListSortDirection.Ascending));
        }
    }
}
