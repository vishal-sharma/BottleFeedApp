using BottleFeedingApp.DataLayer.BusinessDataServices;
using BottleFeedingApp.DataLayer.Models;
using BottleFeedingApp.Utils;
using BottleFeedingApp.Views;
using MvvmHelpers;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BottleFeedingApp.ViewModel
{
    public class CurrentFeedViewModel : BaseViewModel
    {
        private readonly string notAvailable = "Not Available";

        private INavigation navigation;

        public ICommand AddNewFeed { get; private set; }

        public ObservableRangeCollection<BabyFeed> FeedsForToday { get; } = new ObservableRangeCollection<BabyFeed>();

        public CurrentFeedViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            AddNewFeed = new Command(async () => await OnAddNewFeed());
        }

        private async Task OnAddNewFeed()
        {
            var nextPage = new NewFeedView();
            var nextPageVM = new ViewModel.NewFeedViewModel(nextPage.Navigation);
            nextPage.BindingContext = nextPageVM;

            await navigation.PushAsync(nextPage);
        }



        public string LastFeedWithMilkText => LastFeedWithMilk != null
                        ? $"{LastFeedWithMilk.StartQuantity - LastFeedWithMilk.FinishQuantity} ml @{LastFeedWithMilk.FinishTime.ToTime()}"
                        : notAvailable;
        public string LastNappyChangeText => $"@{LastNappyChange?.FinishTime.ToTime() ?? notAvailable}";
        public string LastPoohText => $"@{LastPooh?.FinishTime.ToTime() ?? notAvailable}";

        public string LastWeeText => $"@{LastWee?.FinishTime.ToTime() ?? notAvailable}";

        public string TotalMilkForTodayText => $"{FeedsForToday.Sum(f => f.StartQuantity - f.FinishQuantity)} ml";
        public string TotalNappyChangeForTodayText => $"{FeedsForToday.Count(f => f.WasNappyChanged)}";
        public string TotalPoohForTodayText => $"{FeedsForToday.Count(f => f.HadPooh)}";
        public string TotalWeeForTodayText => $"{FeedsForToday.Count(f => f.HadWee)}";

        ICommand loadFeedsCommand;
        public ICommand LoadFeedsCommand =>
            loadFeedsCommand ?? (loadFeedsCommand = new Command(async () => await ExecuteLoadFeedsCommandAsync()));

        async Task ExecuteLoadFeedsCommandAsync()
        {
            if (IsBusy)
                return;

            //LoadingMessage = "Loading Feeds...";
            IsBusy = true;
            FeedsForToday.ReplaceRange(await FeedDataService.GetAllFeedToday());

            SetCurrentFeedData();
            IsBusy = false;
        }

        private void SetCurrentFeedData()
        {
            LastFeedWithMilk = FeedsForToday.FirstOrDefault(f => f.StartQuantity > 0);
            LastNappyChange = FeedsForToday.FirstOrDefault(f => f.WasNappyChanged);
            LastPooh = FeedsForToday.FirstOrDefault(f => f.HadPooh);
            LastWee = FeedsForToday.FirstOrDefault(f => f.HadWee);

            // By convention, an empty string indicates all properties are invalid.
            OnPropertyChanged(string.Empty);
        }

        private BabyFeed LastFeedWithMilk { get; set; }
        private BabyFeed LastNappyChange { get; set; }
        private BabyFeed LastPooh { get; set; }
        private BabyFeed LastWee { get; set; }
    }

}
