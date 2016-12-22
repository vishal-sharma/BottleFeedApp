using BottleFeedingApp.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleFeedingApp.UnitTest.Data
{
    public static class DataGenerator
    {
        static Random random = new Random();
        public static Feed Feed
        {             
            get
            {
                var feed = new Feed
                {
                    StartQuantity = random.Next(60, 120),
                    FinishQuantity = random.Next(0, 40),
                    StartTime = DateTime.Now.AddMinutes(-20),
                    FinishTime = DateTime.Now,
                    HadPooh = RandomBool,
                    HadWee = RandomBool,
                };

                feed.WasNappyChanged = feed.HadPooh || feed.HadWee;
                return Feed;
            }
        }

        public static bool RandomBool
        {
            get { return random.Next(2) == 0 ? false : true;  }
        }
    }
}
