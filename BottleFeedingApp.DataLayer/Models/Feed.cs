using System;

namespace BottleFeedingApp.DataLayer.Models
{
    public class BabyFeed
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int StartQuantity { get; set; }
        public int FinishQuantity { get; set; }
        public bool WasNappyChanged { get; set; }
        public bool HadPooh { get; set; }
        public bool HadWee { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public decimal ConsumedQuantity => StartQuantity - FinishQuantity;
        [Newtonsoft.Json.JsonIgnore]
        public string ConsumedQuantityMessage => $"{ConsumedQuantity} ml of milk,";
        [Newtonsoft.Json.JsonIgnore]
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
