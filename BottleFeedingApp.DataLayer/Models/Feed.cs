using SQLite;
using System;

namespace BottleFeedingApp.DataLayer.Models
{
    [Table("feed")]
    public class Feed
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public decimal StartQuantity { get; set; }
        public decimal FinishQuantity { get; set; }
        public bool WasNappyChanged { get; set; }
        public bool HadPooh { get; set; }
        public bool HadWee { get; set; }
        [Ignore]
        public decimal ConsumedQuantity => StartQuantity - FinishQuantity;
        [Ignore]
        public string ConsumedQuantityMessage => $"{ConsumedQuantity} ml of milk,";
        [Ignore]
        public string NappyChangeMessage {
           get
            {
                if (!WasNappyChanged) return "No Nappy change";
                else if (HadPooh && !HadWee) return "Had Pooh only";
                else if (!HadPooh && HadWee) return "Had Wee only";
                else if (HadPooh && HadWee) return "Had Pooh and Wee";
                else if (!HadPooh && !HadWee) return "No Pooh and Wee";

                return string.Empty;
            }
        }
    }
}
