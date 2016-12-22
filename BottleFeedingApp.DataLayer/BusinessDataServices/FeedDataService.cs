using BottleFeedingApp.DataLayer.Models;
using BottleFeedingApp.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottleFeedingApp.DataLayer.BusinessDataServices
{
    public static class FeedDataService
    {
        private static FeedRepository repo = new FeedRepository();

        public async static Task SaveNewFeed(Feed feed)
        {
            await repo.AddNewFeedAsync(feed).ConfigureAwait(false);
        }
       
        public async static Task<List<Feed>> GetAllFeedToday()
        {
            var records = await repo.GetAllFeedAsync().ConfigureAwait(false);
            return await records
                .Where(f => f.StartTime >= DateTime.Today)
                .OrderByDescending(f => f.StartTime)
                .ToListAsync()
                .ConfigureAwait(false);
        }

    }
}