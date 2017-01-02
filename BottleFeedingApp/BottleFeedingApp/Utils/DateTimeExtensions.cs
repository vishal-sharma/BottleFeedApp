using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleFeedingApp.Utils
{
    public static class DateTimeExtensions
    {
        public static string ToTime(this DateTime dateTime)
        {
            return dateTime.ToString("hh:mm tt");
        }
    }
}
