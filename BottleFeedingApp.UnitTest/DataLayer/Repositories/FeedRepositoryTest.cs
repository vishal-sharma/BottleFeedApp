using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BottleFeedingApp.UnitTest.DataLayer;
using BottleFeedingApp.Services.Interfaces.DataLayer;
using SQLite;
using BottleFeedingApp.DataLayer.Repositories;
using BottleFeedingApp.DataLayer.Models;
using BottleFeedingApp.UnitTest.Data;

namespace BottleFeedingApp.DataLayer
{
    [TestClass]
    public class FeedRepositoryTest
    {
        private SQLiteAsyncConnection _conn;

        [TestInitialize]
        public void Initialize()
        {
            IDbContext dbContext = new DbContextForTest();
            _conn = dbContext.DbConnection;

        }

        [TestMethod]
        [DeploymentItem("feedBabyTest.db")]
        public void CanAddAndRetrieveRecords()
        {
            var repo = new FeedRepository(_conn);

            repo.AddNewFeedAsync(DataGenerator.Feed).Wait();

            var result = repo.GetAllFeedAsync().Result;

            Assert.AreEqual(1, result.Count);
        }
    }
}
