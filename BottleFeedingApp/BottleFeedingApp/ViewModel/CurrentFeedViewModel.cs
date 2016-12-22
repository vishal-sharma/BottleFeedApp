using BottleFeedingApp.DataLayer.BusinessDataServices;
using BottleFeedingApp.DataLayer.Models;
using BottleFeedingApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BottleFeedingApp.ViewModel
{
    public class CurrentFeedViewModel : BaseViewModel
    {
        private readonly string notAvailable = "Not Available";

        private List<Feed> feedForToday;

        private INavigation navigation;

        public ICommand AddNewFeed { get; private set; }

        public ObservableCollection<CurrentFeedDataModel> CurrentFeedData { get; private set; }

        public CurrentFeedViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            CurrentFeedData = new ObservableCollection<CurrentFeedDataModel>();
            AddNewFeed = new Command(async () => await OnAddNewFeed());
        }

        private async Task OnAddNewFeed()
        {
            var nextPage = new NewFeedView();
            var nextPageVM = new ViewModel.NewFeedViewModel(nextPage.Navigation);
            nextPage.BindingContext = nextPageVM;

            await navigation.PushAsync(nextPage);
        }

        public Feed LastFeedWithMilk { get; set; }
        public Feed LastNappyChange { get; set; }
        public Feed LastPoo { get; set; }
        public Feed LastWee { get; set; }

        public string LastFeedWithMilkText
        {
            get
            {
                return LastFeedWithMilk != null
                        ? $"{LastFeedWithMilk.StartQuantity - LastFeedWithMilk.FinishQuantity} ml @{LastFeedWithMilk.FinishTime}"
                        : notAvailable;
            }
        }
        public string LastNappyChangeText
        {
            get
            {
                return LastNappyChange != null ? $"@{LastNappyChange.FinishTime}" : notAvailable;
            }
        }
        public string LastPooText
        {
            get
            {
                return LastPoo != null ? $"@{LastPoo.FinishTime}" : notAvailable;
            }
        }
        public string LastWeeText
        {
            get
            {
                return LastWee != null ? $"@{LastWee.FinishTime}" : notAvailable;
            }
        }
        public string TotalMilkForTodayText
        {
            get
            {
                return feedForToday != null && feedForToday.Count > 0
                        ? $"{feedForToday.Sum(f => f.StartQuantity - f.FinishQuantity)} ml"
                        : notAvailable;
            }
        }
        public string TotalPooForTodayText
        {
            get
            {
                return feedForToday != null && feedForToday.Count > 0
                        ? $"{feedForToday.Count(f => f.HadPooh)}"
                        : notAvailable;
            }
        }
        public string TotalWeeForTodayText
        {
            get
            {
                return feedForToday != null && feedForToday.Count > 0
                        ? $"{feedForToday.Count(f => f.HadWee)}"
                        : notAvailable;
            }
        }

        public Feed LastFeed { get; set; }

        public async Task InitializeData()
        {
            if (feedForToday == null)
                feedForToday = await FeedDataService.GetAllFeedToday().ConfigureAwait(false);
            else if (LastFeed != null)
                feedForToday.Insert(0, LastFeed);

            SetCurrentFeedData();
        }

        private void SetCurrentFeedData()
        {
            LastFeedWithMilk = feedForToday.FirstOrDefault(f => f.StartQuantity > 0);
            LastNappyChange = feedForToday.FirstOrDefault(f => f.WasNappyChanged);
            LastPoo = feedForToday.FirstOrDefault(f => f.HadPooh);
            LastWee = feedForToday.FirstOrDefault(f => f.HadWee);

            CurrentFeedData.Clear();

            CurrentFeedData.Add(new CurrentFeedDataModel("Last Feed: ", LastFeedWithMilkText));
            CurrentFeedData.Add(new CurrentFeedDataModel("Last Nappy Chanage:", LastNappyChangeText));
            CurrentFeedData.Add(new CurrentFeedDataModel("Last Pooh:", LastPooText));
            CurrentFeedData.Add(new CurrentFeedDataModel("Last Wee:", LastWeeText));
            CurrentFeedData.Add(new CurrentFeedDataModel("Total Milk for today", TotalMilkForTodayText));
            CurrentFeedData.Add(new CurrentFeedDataModel("Total Pooh for today", TotalPooForTodayText));
            CurrentFeedData.Add(new CurrentFeedDataModel("Total Wee for today", TotalWeeForTodayText));
        }

        public class CurrentFeedDataModel
        {
            public CurrentFeedDataModel(string title, string data)
            {
                Title = title;
                Data = data;
            }
            public string Title { get; set; }
            public string Data { get; set; }
        }
    }

}
