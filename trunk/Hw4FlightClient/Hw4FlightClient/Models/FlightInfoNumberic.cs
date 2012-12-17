using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Hw4FlightClient.Models
{
    public class FlightInfoNumberic
    {
        public int Id;
        public string From { get; set; }
        public string To { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Image { get; set; }
        public int GiaTien { get; set; }
        public string Hang { get; set; }

        public FlightInfoNumberic(FlightInfo flightInfo)
        {
            Id = flightInfo.Id;
            From = flightInfo.From;
            To = flightInfo.To;
            TimeFrom = flightInfo.TimeFrom;
            TimeTo = flightInfo.TimeTo;
            Image = flightInfo.Image;
            Hang = flightInfo.Hang;
            try
            {
                string tien = flightInfo.GiaTien.Substring(0, flightInfo.GiaTien.Length - 3);
                string tien2 = tien.Replace(".", "");
                GiaTien = flightInfo.GiaTien != null ? Convert.ToInt32(tien2) : 0;
            }
            catch
            {
                GiaTien = 0;
            }
        }

        public FlightInfo ToFlightInfo(FlightInfoNumberic flightNumberic)
        {
            var tamp = new FlightInfo{
            Id = flightNumberic.Id,
            From = flightNumberic.From,
            To = flightNumberic.To,
            TimeFrom = flightNumberic.TimeFrom,
            TimeTo = flightNumberic.TimeTo,
            Image = flightNumberic.Image,
            Hang = flightNumberic.Hang,
            GiaTien = flightNumberic.GiaTien.ToString("0,0", CultureInfo.InvariantCulture) + "VND"
            };
            return tamp;
        }
    }


}