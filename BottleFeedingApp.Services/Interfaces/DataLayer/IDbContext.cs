using SQLite;

namespace BottleFeedingApp.Services.Interfaces.DataLayer
{
    public interface IDbContext
    {
        SQLiteAsyncConnection DbConnection { get; }
    }
}
