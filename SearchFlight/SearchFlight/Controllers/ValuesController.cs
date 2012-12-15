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
        public List<FlightInfo> ListInfo(string timefrom,string timeto,int direction,string from,string to,string dayfrom,string dayto,string monthfrom,string monthto)
        {

            List<FlightInfo> list = new List<FlightInfo>();
            List<FlightInfo> listreturn = new List<FlightInfo>();
            try
            {             
                FlightInfo info = new FlightInfo();
                List<string> fee = new List<string>();
                // direction=1 - 1chieu, direction=2 - 2chieu
                string dic;
                if (direction == 1)
                    dic = "onewaytravel";
                else
                    dic = "returntravel";
                #region VN Airlines
                string Url = "https://cat.sabresonicweb.com/SSWVN/meridia?language=vi&posid=B3QE&page=requestAirMessage_air&action=airRequest&realrequestAir=realRequestAir&actionType=nonFlex&classService=CoachClass&currency=VND&"
                + "depTime=0600"
                + "&retTime=0600"
                + "&direction="+dic
                + "&departCity="+from
                + "&depDay="+dayfrom
                + "&depMonth="+monthfrom
                + "&temp_date="
                + "&returnCity="+to
                + "&retDay="+dayto
                + "&retMonth="+monthto
                + "&temp_date=&ADT=1&CHD=0&INF=0&submit=T%C3%ACm%20chuy%E1%BA%BFn%20bay&wpf_timed_action_0VNAirline_00215IBEFastTabV2_00215bookingYourTrip_00515vn_0051513b92417f90_005158e04_1=&ShowNote=Qu%C3%BD%20kh%C3%A1ch%20c%E1%BA%A7%20nh%E1%BA%AD%20%C4%91u7847%20y%20%C4%91u7911%20%20th%C3%B4ng%20tin%20%C4%91u7875%20%20l%C3%A0m%20th%E1%BB%A7t%E1%BB%A5%20!&wclang=VI&txtFamilyName=&txtMidName=&slDeparture=&txtConfirmCode=&txtFlightNumber=&rdoDay=on&";
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(Url);
                
                
                int count = 0;
                string newpath = "//tr[@class='_out' or @class='_in']";
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes(newpath))
                {
                    string x = link.XPath;
                    HtmlNodeCollection flag = link.SelectNodes(x+"/td[div[@class='hoverTaxes']]");
                    foreach(HtmlNode temp in flag)
                    {
                        string xx = temp.XPath;
                        HtmlNode a = temp.SelectSingleNode(xx + "/div[@class='hoverTaxes']/table[1]/tr[4]/td[2]");
                        if (a == null)
                        {
                            fee.Add("Không Có");
                            
                        }
                        else
                        {
                            string t = a.InnerText;
                            t = t.Replace("\r", "");
                            t = t.Replace("\n", "");
                            t = t.Replace("\t", "");
                            t = t.Replace(" ", "");
                            fee.Add(t);
                                                   
                        }
                            count++;                        
                    }
                    if (count < 5)
                    {
                        for (int j = 0; j < 5 - count; j++)
                        {
                            fee.Add("Không Có");
                        }
                        count = 0;
                    }
                    else
                        count = 0;
                }
           
                count = 0;
                string path = "//td[@class='matrix_cal_body matrix_cal_body_dep' or " +
                "@class='matrix_cal_body matrix_cal_body_arr' or " +
                "@class='matrix_cal_body matrix_cal_body_flifo' ]";
                foreach (HtmlNode link1 in doc.DocumentNode.SelectNodes(path))
                {
                    string Xpath = link1.XPath;
                    string t;
                    if (link1.Attributes["class"].Value == "matrix_cal_body matrix_cal_body_dep")
                    {
                        #region
                        HtmlNode temp = link1.SelectSingleNode(Xpath+"/div[1]/div[2]/table[1]/tr[1]/td[1]");
                        t = temp.InnerText;
                        t = t.Replace("\r", "");
                        t = t.Replace("\n", "");
                        t = t.Replace("\t", "");
                        t = t.Replace(" ", "");
                        info.From = t;
                        temp = link1.SelectSingleNode(Xpath + "/div[1]/p");
                        info.TimeFrom = temp.InnerText;
                        #endregion

                    }
                    if (link1.Attributes["class"].Value == "matrix_cal_body matrix_cal_body_arr")
                    {
                        #region
                        HtmlNode temp = link1.SelectSingleNode(Xpath + "/div[1]/div[2]/table[1]/tr[1]/td[1]");
                        t = temp.InnerText;
                        t = t.Replace("\r", "");
                        t = t.Replace("\n", "");
                        t = t.Replace("\t", "");
                        t = t.Replace(" ", "");
                        info.To = t;
                        temp = link1.SelectSingleNode(Xpath + "/div[1]/p");
                        info.TimeTo = temp.InnerText;
                        #endregion
                    }
                    if (link1.Attributes["class"].Value == "matrix_cal_body matrix_cal_body_flifo")
                    {
                        #region
                        info.Image = "https://cat.sabresonicweb.com/SSWVN/application/images/B3QE/airplane.gif";
                        #endregion

                        for (int flag = count * 5 + 4; flag >= count * 5; flag--)
                        {
                            if (fee[flag] != "Không Có")
                            {
                                info.GiaTien = fee[flag];
                                break;
                            }
                        }
                        list.Add(info);
                        
                        info = new FlightInfo();
                       
                        count++;
                    }

                }
                #endregion
                #region Jet
                Url = "http://booknow.jetstar.com/Search.aspx?__EVENTTARGET=" +
                "&__EVENTARGUMENT=" +
                "&__VIEWSTATE=/wEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgEFJ01lbWJlckxvZ2luU2VhcmNoVmlldyRtZW1iZXJfUmVtZW1iZXJtZTxtWS/I2BXFBfalk96y3LBuGXXD" +
                "&pageToken=" +
                "&total_price=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$RadioButtonMarketStructure=RoundTrip" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin1=Hà Nội (HAN)" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination1=Tp.Hồ Chí Minh (SGN)" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate1=24/12/2012" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate1=18/01/2013" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$DropDownListCurrency=VND" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin2=Tp.Hồ Chí Minh (SGN)" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination2=Hà Nội (HAN)" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate2=18/01/2013" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate2=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin3=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination3=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate3=25/12/2012" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate3=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin4=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination4=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate4=01/01/2013" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate4=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin5=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination5=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate5=08/01/2013" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate5=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin6=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination6=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate6=15/01/2013" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate6=" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$DropDownListPassengerType_ADT=1" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$DropDownListPassengerType_CHD=0" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$DropDownListPassengerType_INFANT=0" +
                "&ControlGroupSearchView$AvailabilitySearchInputSearchView$ButtonSubmit=";
                #endregion

                #region select flight
                for (int b = 0; b < list.Count; b++)
                {
                    if (timefrom != null && timeto != null)
                    {
                        bool a=list[b].From.Contains(from);
                        if ((list[b].From.Contains(from) && list[b].TimeFrom == timefrom) || (list[b].From.Contains(to) && list[b].TimeFrom == timeto))
                        {
                            listreturn.Add(list[b]);
                        }
                    }
                    else
                    {
                        if (timefrom == null && timeto!=null)
                        {
                            if (list[b].From.Contains(from) || (list[b].From.Contains(to) && list[b].TimeFrom == timeto))
                            {
                                listreturn.Add(list[b]);
                            }
                        }
                        if (timefrom != null && timeto == null)
                        {
                            if ((list[b].From.Contains(from) && list[b].TimeFrom == timefrom) || list[b].From.Contains(to))
                            {
                                listreturn.Add(list[b]);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                return listreturn;
            }
            return listreturn;
        }
    }
}