using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace Hw4FlightClient.Models
{
    static public class DbContext
    {
        public static string HttpGet(string url)
        {
            var req = WebRequest.Create(url) as HttpWebRequest;
            if (req != null)
            {
                req.Method = "GET";
                req.Accept = "text/json";

                string result = null;
                using (var resp = req.GetResponse() as HttpWebResponse)
                {
                    if (resp != null)
                        using (var reader = new StreamReader(resp.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                        }
                }
                return result;
            }
            return null;
        }


        public static List<FlightInfo> SearchFlight(string timefrom, string timeto, int direction, string from, string to, DateTime datefrom, DateTime dateto)
        {
            string api =
                string.Format(
                    "http://localhost:1260/api/values/?timefrom={0}&timeto={1}&direction={2}&from={3}&to={4}&dayfrom={5}&dayto={6}&monthfrom={7}&monthto={8}",
                    timefrom, timeto, direction, from, to, datefrom.Day,
                    dateto.Day, datefrom.ToString("MMM", CultureInfo.InvariantCulture).ToUpper(), dateto.ToString("MMM", CultureInfo.InvariantCulture).ToUpper());
            string json = HttpGet(api);
            var js = new JavaScriptSerializer();
            var flightInfos = js.Deserialize<FlightInfo[]>(json);
            return flightInfos.ToList();
        }
    }
}