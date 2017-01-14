using BottleFeedingApp.DataLayer.DataService;
using BottleFeedingApp.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottleFeedingApp.DataLayer.BusinessDataServices
{
    public static class FeedDataService
    {
        private static AzureService dataService = new AzureService();

        public async static Task SaveNewFeed(BabyFeed feed)
        {
            await dataService.AddFeed(feed).ConfigureAwait(false);
        }
       
        public async static Task<List<BabyFeed>> GetAllFeedToday()
        {
            var records = await dataService.GetFeeds().ConfigureAwait(false);
            return await records
                .Where(f => f.StartTime >= DateTime.Today)
                .OrderByDescending(f => f.StartTime)
                .ToListAsync()
                .ConfigureAwait(false);
        }

    }
}