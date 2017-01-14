using BottleFeedingApp.ViewModel;

using Xamarin.Forms;

namespace BottleFeedingApp.Views
{
    public partial class CurrentFeedView : ContentPage
    {
        public CurrentFeedView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var vm = (CurrentFeedViewModel)BindingContext;
            await vm.LoadData();
        }
    }
}
