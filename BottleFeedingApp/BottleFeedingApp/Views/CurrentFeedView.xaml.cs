using BottleFeedingApp.ViewModel;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace BottleFeedingApp.Views
{
    public partial class CurrentFeedView : ContentPage
    {
        private CurrentFeedViewModel vm;

        public CurrentFeedView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CrossConnectivity.Current.ConnectivityChanged += ConnecitvityChanged;
            OfflineStack.IsVisible = !CrossConnectivity.Current.IsConnected;

            vm = (CurrentFeedViewModel)BindingContext;
            vm.LoadFeedsCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossConnectivity.Current.ConnectivityChanged -= ConnecitvityChanged;
        }

        void ConnecitvityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                OfflineStack.IsVisible = !e.IsConnected;

                if(e.IsConnected && vm.FeedsForToday.Count == 0)
                    vm.LoadFeedsCommand.Execute(null);
            });
        }
    }
}
