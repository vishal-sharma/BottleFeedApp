using BottleFeedingApp.Views;
using Xamarin.Forms;

namespace BottleFeedingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // For UWP the main page has to be set in App constructor
            // otherwise it will throw Argument Null Exception
            SetMainPage();
        }

        protected override void OnStart()
        {
                      
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
                BarBackgroundColor = Color.FromHex("#F16D36")
            };
        }
    }
}
