using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Diagnostics;
using System.IO;
using Plugin.Connectivity;
using BottleFeedingApp.DataLayer.Models;

namespace BottleFeedingApp.DataLayer.DataService
{ 
    public class AzureService
    {

        public MobileServiceClient Client { get; set; } = null;
        IMobileServiceSyncTable<BabyFeed> feedTable;

        public async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            var appUrl = "http://babyappdemo.azurewebsites.net";

            //Create our client
            Client = new MobileServiceClient(appUrl);


            //InitialzeDatabase for path
            var path = "babyFeedStore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            //Define table
            store.DefineTable<BabyFeed>();


            //Initialize SyncContext
            await Client.SyncContext.InitializeAsync(store);

            //Get our sync table that will call out to azure
            feedTable = Client.GetSyncTable<BabyFeed>();


        }

        public async Task SyncFeed()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                await feedTable.PullAsync("allBabyFeed", feedTable.CreateQuery());

                await Client.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync Feeds, that is alright as we have offline capabilities: " + ex);
            }

        }

        public async Task<IMobileServiceSyncTable<BabyFeed>> GetFeeds()
        {
            //Initialize & Sync
            await Initialize();
            await SyncFeed();
            return feedTable;

        }

        public async Task<BabyFeed> AddFeed(BabyFeed babyFeed)
        {
            await Initialize();
            await feedTable.InsertAsync(babyFeed);
            await SyncFeed();
            return babyFeed;
        }
    }
}
