using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HtmlAgilityPack;
using SearchFlight.Models;

namespace SearchFlight.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        public List<FlightInfo> ListInfo(int i)
        {
            List<FlightInfo> list = new List<FlightInfo>();
            string Url = "https://cat.sabresonicweb.com/SSWVN/meridia?language=vi&posid=B3QE&page=requestAirMessage_air&action=airRequest&realrequestAir=realRequestAir&actionType=nonFlex&classService=CoachClass&currency=VND&"
            + "depTime=0600"
            + "&retTime=0600"
            + "&direction=onewaytravel"
            + "&departCity=SGN"
            + "&depDay=24"
            + "&depMonth=DEC"
            + "&temp_date="
            + "&returnCity=HAN"
            + "&retDay=31"
            + "&retMonth=DEC"
            + "&temp_date=&ADT=1&CHD=0&INF=0&submit=T%C3%ACm%20chuy%E1%BA%BFn%20bay&wpf_timed_action_0VNAirline_00215IBEFastTabV2_00215bookingYourTrip_00515vn_0051513b92417f90_005158e04_1=&ShowNote=Qu%C3%BD%20kh%C3%A1ch%20c%E1%BA%A7%20nh%E1%BA%AD%20%C4%91u7847%20y%20%C4%91u7911%20%20th%C3%B4ng%20tin%20%C4%91u7875%20%20l%C3%A0m%20th%E1%BB%A7t%E1%BB%A5%20!&wclang=VI&txtFamilyName=&txtMidName=&slDeparture=&txtConfirmCode=&txtFlightNumber=&rdoDay=on&";
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(Url);
            string path = "//td[@class='matrix_cal_body matrix_cal_body_dep' or " +
                "@class='matrix_cal_body matrix_cal_body_arr' or " +
                "@class='matrix_cal_body matrix_cal_body_flifo']";
            try
            {
                FlightInfo info = new FlightInfo();
                
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes(path))
                {
                    string Xpath = link.XPath;
                    if (link.Attributes["class"].Value == "matrix_cal_body matrix_cal_body_dep")
                    {
                        HtmlNode temp = link.SelectSingleNode(Xpath+"/div[1]/div[2]/table[1]/tr[1]/td[1]");
                        info.From = temp.InnerHtml;
                        temp = link.SelectSingleNode(Xpath + "/div[1]/p");
                        info.TimeFrom = temp.InnerText;

                    }
                    if (link.Attributes["class"].Value == "matrix_cal_body matrix_cal_body_arr")
                    {
                        HtmlNode temp = link.SelectSingleNode(Xpath + "/div[1]/div[2]/table[1]/tr[1]/td[1]");
                        info.To = temp.InnerHtml;
                        temp = link.SelectSingleNode(Xpath + "/div[1]/p");
                        info.TimeTo = temp.InnerText;
                    }
                    if (link.Attributes["class"].Value == "matrix_cal_body matrix_cal_body_flifo")
                    {
                        info.Image = "https://cat.sabresonicweb.com/SSWVN/application/images/B3QE/airplane.gif";
                        HtmlNode temp = link.SelectSingleNode(Xpath+"/div[1]/span[3]/span[1]");
                        info.info = temp.InnerHtml;
                        temp = link.SelectSingleNode(Xpath + "/div[1]/span[4]");
                        info.info = info.info + "+" + temp.InnerText;
                        list.Add(info);
                        info = new FlightInfo();
                    }

                }
            }
            catch (Exception ex)
            {
                return list;
            }
            return list;
        }
    }
}