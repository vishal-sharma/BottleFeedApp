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
    }
}
