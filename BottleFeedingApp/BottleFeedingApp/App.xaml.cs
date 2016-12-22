using BottleFeedingApp.Views;
using Xamarin.Forms;

namespace BottleFeedingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            SetMainPage();            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SetMainPage()
        {
            // The root page of your application
            var nextPage = new CurrentFeedView();
            var nextPageVM = new ViewModel.CurrentFeedViewModel(nextPage.Navigation);
            nextPage.BindingContext = nextPageVM;

            MainPage = new NavigationPage(nextPage)
            {
                BarBackgroundColor = Color.FromHex("#77D065"),
                BarTextColor = Color.Red,
            };
        }
    }
}
