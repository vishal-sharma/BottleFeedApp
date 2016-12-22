using BottleFeedingApp.Services.Interfaces.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BottleFeedingApp.UnitTest.DataLayer
{
    class DbContextForTest : IDbContext
    {
        SQLiteAsyncConnection _conn;

        public SQLiteAsyncConnection DbConnection
        {
            get
            {
                if (_conn == null)
                {
                    _conn = new SQLiteAsyncConnection("feedBabyTest.db");
                }
                return _conn;
            }
        }
    }
}
