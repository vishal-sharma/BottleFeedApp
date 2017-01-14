﻿using BottleFeedingApp.DataLayer.BusinessDataServices;
using BottleFeedingApp.DataLayer.Models;
using BottleFeedingApp.Utils;
using BottleFeedingApp.Views;
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

        private INavigation navigation;

        public ICommand AddNewFeed { get; private set; }

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

        public ObservableCollection<BabyFeed> FeedsForToday { get; set; }

        public BabyFeed LastFeedWithMilk { get; set; }
        public BabyFeed LastNappyChange { get; set; }
        public BabyFeed LastPooh { get; set; }
        public BabyFeed LastWee { get; set; }

        public string LastFeedWithMilkText
        {
            get
            {
                return LastFeedWithMilk != null
                        ? $"{LastFeedWithMilk.StartQuantity - LastFeedWithMilk.FinishQuantity} ml @{LastFeedWithMilk.FinishTime.ToTime()}"
                        : notAvailable;
            }
        }
        public string LastNappyChangeText
        {
            get
            {
                return LastNappyChange != null ? $"@{LastNappyChange.FinishTime.ToTime()}" : notAvailable;
            }
        }
        public string LastPoohText
        {
            get
            {
                return LastPooh != null ? $"@{LastPooh.FinishTime.ToTime()}" : notAvailable;
            }
        }
        public string LastWeeText
        {
            get
            {
                return LastWee != null ? $"@{LastWee.FinishTime.ToTime()}" : notAvailable;
            }
        }
        public string TotalMilkForTodayText
        {
            get
            {
                return FeedsForToday != null
                        ? $"{FeedsForToday.Sum(f => f.StartQuantity - f.FinishQuantity)} ml"
                        : notAvailable;
            }
        }
        public string TotalNappyChangeForTodayText
        {
            get
            {
                return FeedsForToday != null
                        ? $"{FeedsForToday.Count(f => f.WasNappyChanged)}"
                        : notAvailable;
            }
        }
        public string TotalPoohForTodayText
        {
            get
            {
                return FeedsForToday != null
                        ? $"{FeedsForToday.Count(f => f.HadPooh)}"
                        : notAvailable;
            }
        }
        public string TotalWeeForTodayText
        {
            get
            {
                return FeedsForToday != null
                        ? $"{FeedsForToday.Count(f => f.HadWee)}"
                        : notAvailable;
            }
        }

        public BabyFeed LastFeed { get; set; }

        public async Task LoadData()
        {
            if (FeedsForToday == null)
                FeedsForToday = new ObservableCollection<BabyFeed>(
                    await FeedDataService.GetAllFeedToday());
            else if (HasANewFeed)
                FeedsForToday.Insert(0, LastFeed);

            SetCurrentFeedData();
        }

        private void SetCurrentFeedData()
        {
            LastFeedWithMilk = FeedsForToday.FirstOrDefault(f => f.StartQuantity > 0);
            LastNappyChange = FeedsForToday.FirstOrDefault(f => f.WasNappyChanged);
            LastPooh = FeedsForToday.FirstOrDefault(f => f.HadPooh);
            LastWee = FeedsForToday.FirstOrDefault(f => f.HadWee);

            RaiseAllPropertiesChanged();
        }

        private bool HasANewFeed => LastFeed != null;
    }

}
