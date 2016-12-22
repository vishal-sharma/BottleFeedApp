using BottleFeedingApp.DataLayer.Models;
using BottleFeedingApp.Services.Interfaces.DataLayer;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleFeedingApp.DataLayer.Repositories
{
    public class FeedRepository
    {
        private readonly SQLiteAsyncConnection _conn;

        private bool tableExits = false;

        public FeedRepository(IDbContext dbContext = null)
        {
            var context = dbContext ?? (IDbContext)DbContext.Instance;
            _conn = context.DbConnection;            
        }

        public async Task Initialize()
        {
            if (!tableExits)
            {
                await _conn.CreateTableAsync<Feed>().ConfigureAwait(false);
                tableExits = true;
            }
        }

        public async Task AddNewFeedAsync(Feed feed)
        {
            await Initialize().ConfigureAwait(false);
            try
            {
                var result = await _conn.InsertAsync(feed).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception ex)
            {
                //log the exception
            }
        }

        public async Task<AsyncTableQuery<Feed>> GetAllFeedAsync()
        {
            await Initialize().ConfigureAwait(false);
            return _conn.Table<Feed>();
        }

        public async Task<List<Feed>> GetFeedForLastThreeDaysAsync()
        {
            var allRecordsQuery = await GetAllFeedAsync().ConfigureAwait(false);
            return await allRecordsQuery
                .Where(f => f.StartTime >= DateTime.Now.AddDays(-3))
                .OrderBy(f => f.FinishTime)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
