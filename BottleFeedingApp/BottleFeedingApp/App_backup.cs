using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BottleFeedingApp.Services.Interfaces;

using Xamarin.Forms;
using BottleFeedingApp.DataLayer;
using BottleFeedingApp.Views;

namespace BottleFeedingApp
{
    
    public class App_backup
    {
        StackLayout sl = new StackLayout();
        public App_backup()
        {

        }

        //protected override void OnStart()
        //{
        //    SetStyles();
        //    SetMainPage();
        //}        

        //protected override void OnSleep()
        //{
        //    // Handle when your app sleeps
        //}

        //protected override void OnResume()
        //{
        //    // Handle when your app resumes
        //}

        //private void SetStyles()
        //{
        //    var pageStyle = new Style(typeof(ContentPage));
        //    pageStyle.BaseResourceKey = "pageStyle";
        //    pageStyle.Setters.Add(View.BackgroundColorProperty, Color.White);
        //    //pageStyle.Setters.Add(View., Color.White);
        //    Resources = new ResourceDictionary();
        //    Resources.Add(pageStyle);
        //}

        //private void SetMainPage()
        //{
        //    // The root page of your application
        //    var nextPage = new CurrentFeedView();
        //    var nextPageVM = new ViewModel.CurrentFeedViewModel(nextPage.Navigation);
        //    nextPage.BindingContext = nextPageVM;

        //    MainPage = new NavigationPage(nextPage);
        //}
    }
}
