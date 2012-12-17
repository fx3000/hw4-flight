using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Hw4FlightClient.Models
{
    public class FlightInfo
    {
        public int Id;
        public string From { get; set; }
        public string To { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Image { get; set; }
        public string GiaTien { get; set; }
        public string Hang { get; set; }
    }
}