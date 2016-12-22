using BottleFeedingApp.Services.Interfaces.DataLayer;
using BottleFeedingApp.Services.PlatformSpecific.Interfaces;
using Splat;
using SQLite;

namespace BottleFeedingApp.DataLayer
{
    class DbContext : IDbContext
    {
        private IFileAccess _platformSpecificFileAccess;

        private string _dbName;

        private SQLiteAsyncConnection _dbConnection;

        private static readonly DbContext _dbContext = new DbContext(); 

        private DbContext() 
        {
            _platformSpecificFileAccess = Locator.Current.GetService<IFileAccess>();
            _dbName = "babyFeed.db";

        }

        public static DbContext Instance
        {
            get
            {
                return _dbContext;
            }
        }

        public SQLiteAsyncConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                {
                    var dbPath = _platformSpecificFileAccess.GetLocalFilePath(_dbName);
                    _dbConnection = new SQLiteAsyncConnection(dbPath);
                }
                return _dbConnection;
            }
        }
    }
}
