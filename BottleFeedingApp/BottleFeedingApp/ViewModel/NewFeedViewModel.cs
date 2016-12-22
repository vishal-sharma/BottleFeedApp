using BottleFeedingApp.DataLayer.BusinessDataServices;
using BottleFeedingApp.DataLayer.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BottleFeedingApp.ViewModel
{
    public class NewFeedViewModel : BaseViewModel
    {

        private INavigation navigation;

        private Feed feed;
        public ICommand SaveNewFeed { get; private set; }

        public NewFeedViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            feed = new Feed();
            SaveNewFeed = new Command(async () => await OnSaveNewFeed());
        }
        
        public decimal StartQuantity
        {
            get { return feed.StartQuantity;  }
            set
            {
                feed.StartQuantity = value;
                feed.StartTime = DateTime.Now;
            }
        }
        public decimal FinishQuantity
        {
            get { return feed.FinishQuantity; }
            set { feed.FinishQuantity = value; }
        }
        public bool WasNappyChanged
        {
            get { return feed.WasNappyChanged; }
            set
            {
                feed.WasNappyChanged = value;
                RaisePropertyChanged();
            }
        }
        public bool HadPooh
        {
            get { return feed.HadPooh; }
            set { feed.HadPooh = value; }
        }
        public bool HadWee
        {
            get { return feed.HadWee; }
            set { feed.HadWee = value; }
        }
        private async Task OnSaveNewFeed()
        {
            feed.FinishTime = DateTime.Now;
            await FeedDataService.SaveNewFeed(feed).ConfigureAwait(false);
            var vm = navigation.NavigationStack[0].BindingContext as CurrentFeedViewModel;
            vm.LastFeed = feed;
            Device.BeginInvokeOnMainThread(async () => await navigation.PopAsync());
        }        
    }
}
