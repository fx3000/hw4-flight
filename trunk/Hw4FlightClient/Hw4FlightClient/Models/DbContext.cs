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
        private static List<AirPort> airPorts = new List<AirPort>();
        private static List<FlightInfo> lstFlightInfos = new List<FlightInfo>();

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
                    "http://localhost:1260/api/values/?timefrom={0}&timeto={1}&direction={2}&from={3}&to={4}&dayfrom={5}&dayto={6}",
                    timefrom, timeto, direction, from, to, datefrom.ToShortDateString(), dateto.ToShortDateString());
            string json = HttpGet(api);
            var js = new JavaScriptSerializer();
            var flightInfos = js.Deserialize<FlightInfo[]>(json);
            lstFlightInfos = flightInfos.ToList();

            for (var i =0;i<lstFlightInfos.Count; i++)
            {
                lstFlightInfos[i].Id = i;
            }

            return lstFlightInfos;
        }
    
        public static List<AirPort>  GetAllAirPort()
        {
            var list = new List<AirPort>
                               {
                                   new AirPort {Id = "BMT", Name = "Buôn Mê Thuột"},
                                   new AirPort {Id = "CAH", Name = "Cà Mau (CAH)"},
                                   new AirPort {Id = "VCS", Name = "Côn Đảo (VCS)"},
                                   new AirPort {Id = "VCA", Name = "Cần Thơ (VCA)"},
                                   new AirPort {Id = "HUI", Name = "Huế (HUI)"},
                                   new AirPort {Id = "HAN", Name = "Hà Nội (HAN)"},
                                   new AirPort {Id = "NHA", Name = "Hải Phòng (HPH)"},
                                   new AirPort {Id = "NHA", Name = "Nha Trang (NHA)"},
                                   new AirPort {Id = "PQC", Name = "Phú Quốc (PQC)"}
                               };

            return list;
        }

        static public List<AirPort> SearchAirPort(string str)
        {
            if(airPorts == null || airPorts.Count <= 0)
            {
                airPorts = GetAllAirPort();
            }

            return airPorts.Where(airPort => airPort.Name.Contains(str)).ToList();
        }
    
        static public List<FlightInfo> Sort(int type)
        {
            if(type == 1)
                return lstFlightInfos.Where(p=>p.GiaTien != "không").OrderBy(p => p.GiaTien != null ? Convert.ToInt32(p.GiaTien.Substring(0, p.GiaTien.Length - 3)) : 0).ToList();

            return lstFlightInfos.OrderByDescending(p => p.GiaTien != null ? Convert.ToInt32(p.GiaTien) : 0).ToList();
        }

        static public List<FlightInfo> Filter(string airlines)
        {
            return lstFlightInfos.Where(p => p.Hang.Contains(airlines)).ToList();
        }

        static public FlightInfo GetFlight(int id)
        {
            return lstFlightInfos.SingleOrDefault(p => p.Id == id);
        }
    }
}