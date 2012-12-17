using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HtmlAgilityPack;
using SearchFlight.Models;
using System.Text;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SearchFlight.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        public List<FlightInfo> ListInfo(int direction, string from, string to, string dayfrom, string dayto)
        {

            List<FlightInfo> list = new List<FlightInfo>();
            List<FlightInfo> listreturn = new List<FlightInfo>();
            string AirPortFrom = "";
            string AirPortTo = "";
            string timefrom = null;
            string timeto = null;
            #region SanBay
            switch (from)
            {
                case "BMW":
                    AirPortFrom = "Buôn Ma Thuột";
                    break;
                case "CAH":
                    AirPortFrom = "Cà Mau";
                    break;
                case "VCA":
                    AirPortFrom = "Cần Thơ";
                    break;
                case "HUI":
                    AirPortFrom = "Huế";
                    break;
                case "HAN":
                    AirPortFrom = "Hà Nội";
                    break;
                case "HPH":
                    AirPortFrom = "Hải Phòng";
                    break;
                case "NHA":
                    AirPortFrom = "Nha Trang";
                    break;
                case "PQC":
                    AirPortFrom = "Phú Quốc";
                    break;
                case "PXU":
                    AirPortFrom = "Pley Ku";
                    break;
                case "UIH":
                    AirPortFrom = "Qui Nhơn";
                    break;
                case "VKG":
                    AirPortFrom = "Rạch Giá";
                    break;
                case "VCL":
                    AirPortFrom = "Tam Kỳ";
                    break;
                case "SGN":
                    AirPortFrom = "Hồ Chí Minh";
                    break;
                case "TBB":
                    AirPortFrom = "Tuy Hòa";
                    break;
                case "VII":
                    AirPortFrom = "Vinh";
                    break;
                case "DIN":
                    AirPortFrom = "Điện Biên";
                    break;
                case "DLI":
                    AirPortFrom = "Đà Lạt";
                    break;
                case "DAD":
                    AirPortFrom = "Đà nẵng";
                    break;
                case "VDH":
                    AirPortFrom = "Đồng Hới";
                    break;

            }
            switch (to)
            {
                case "BMW":
                    AirPortTo = "Buôn Ma Thuột";
                    break;
                case "CAH":
                    AirPortTo = "Cà Mau";
                    break;
                case "VCA":
                    AirPortTo = "Cần Thơ";
                    break;
                case "HUI":
                    AirPortTo = "Huế";
                    break;
                case "HAN":
                    AirPortTo = "Hà Nội";
                    break;
                case "HPH":
                    AirPortTo = "Hải Phòng";
                    break;
                case "NHA":
                    AirPortTo = "Nha Trang";
                    break;
                case "PQC":
                    AirPortTo = "Phú Quốc";
                    break;
                case "PXU":
                    AirPortTo = "Pley Ku";
                    break;
                case "UIH":
                    AirPortTo = "Qui Nhơn";
                    break;
                case "VKG":
                    AirPortTo = "Rạch Giá";
                    break;
                case "VCL":
                    AirPortTo = "Tam Kỳ";
                    break;
                case "SGN":
                    AirPortTo = "Hồ Chí Minh";
                    break;
                case "TBB":
                    AirPortTo = "Tuy Hòa";
                    break;
                case "VII":
                    AirPortTo = "Vinh";
                    break;
                case "DIN":
                    AirPortTo = "Điện Biên";
                    break;
                case "DLI":
                    AirPortTo = "Đà Lạt";
                    break;
                case "DAD":
                    AirPortTo = "Đà nẵng";
                    break;
                case "VDH":
                    AirPortTo = "Đồng Hới";
                    break;

            }
            #endregion
            try
            {
                FlightInfo info = new FlightInfo();
                List<string> fee = new List<string>();

                // direction=1 - 1chieu, direction=2 - 2chieu
                string dic = "";
                if (direction == 1)
                    dic = "OneWay";
                else
                    dic = "RoundTrip";
                #region Jet
                string Url1 =
        "&__EVENTARGUMENT=" +
        "&__VIEWSTATE=/wEPDwUBMGQYAQUeX19Db250cm9sc1JlcXVpcmVQb3N0QmFja0tleV9fFgEFJ01lbWJlckxvZ2luU2VhcmNoVmlldyRtZW1iZXJfUmVtZW1iZXJtZTxtWS/I2BXFBfalk96y3LBuGXXD" +
        "&pageToken=" +
        "&total_price=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$RadioButtonMarketStructure=" + dic +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin1=(" + from + ")" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination1=(" + to + ")" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate1=" + dayfrom +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate1=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$DropDownListCurrency=VND" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin2=(" + to + ")" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination2=(" + from + ")" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate2=" + dayto +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate2=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin3=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination3=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate3=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate3=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin4=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination4=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate4=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate4=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin5=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination5=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate5=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate5=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketOrigin6=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextBoxMarketDestination6=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDepartureDate6=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$TextboxDestinationDate6=" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$DropDownListPassengerType_ADT=1" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$DropDownListPassengerType_CHD=0" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$DropDownListPassengerType_INFANT=0" +
        "&ControlGroupSearchView$AvailabilitySearchInputSearchView$ButtonSubmit=";


                try
                {

                    byte[] postBytes = Encoding.UTF8.GetBytes(Url1);

                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://booknow.jetstar.com/Search.aspx?culture=vi-VN");
                    webRequest.Method = "POST";
                    CookieContainer a = new CookieContainer();

                    webRequest.CookieContainer = a;
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    webRequest.ContentLength = postBytes.Length;

                    Stream postStream = webRequest.GetRequestStream();
                    postStream.Write(postBytes, 0, postBytes.Length);
                    postStream.Close();

                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

                    Console.WriteLine(webResponse.StatusCode);
                    Console.WriteLine(webResponse.Server);

                    Stream responseStream = webResponse.GetResponseStream();
                    StreamReader responseStreamReader = new StreamReader(responseStream, Encoding.UTF8);
                    string result = responseStreamReader.ReadToEnd();

                    String sDiskFile = HttpContext.Current.Server.MapPath("~/result.htm");
                    //string root = HttpContext.Current.Server.MapPath;
                    StreamWriter oWriter = new StreamWriter(sDiskFile);
                    oWriter.Write(result);

                    oWriter.Close();
                    responseStreamReader.Close();
                    webResponse.Close();

                    HtmlAgilityPack.HtmlWeb webjet = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument docJet = webjet.Load(HttpContext.Current.Server.MapPath("~/result.htm"));
                    string XpathJet = "//tbody/tr[not(@*)]";
                    foreach (HtmlNode link in docJet.DocumentNode.SelectNodes(XpathJet))
                    {
                        FlightInfo JetInfo = new FlightInfo();
                        JetInfo.Hang = "Jet Star";
                        string t;
                        HtmlNode JetTimeFrom = link.SelectSingleNode(link.XPath + "/td[1]/strong[1]");
                        if (JetTimeFrom != null)
                        {
                            t = JetTimeFrom.InnerText;
                            JetInfo.TimeFrom = t;
                            JetInfo.Image = "http://timsanpham.com/images/pictures/18763_20080911112741_jetstar_logo[1]_.png";
                            HtmlNode JetFrom = link.SelectSingleNode(link.XPath + "/td[1]/a[1]");
                            t = JetFrom.InnerText;
                            if (t.Contains(AirPortFrom))
                            {
                                t = AirPortFrom + " (" + from + ")";
                            }
                            if (t.Contains(AirPortTo))
                            {
                                t = AirPortTo + " (" + to + ")";
                            }
                            JetInfo.From = t;
                            HtmlNode JetTo = link.SelectSingleNode(link.XPath + "/td[2]/a[1]");
                            t = JetTo.InnerText;
                            if (t.Contains(AirPortFrom))
                            {
                                t = AirPortFrom + " (" + from + ")";
                            }
                            if (t.Contains(AirPortTo))
                            {
                                t = AirPortTo + " (" + to + ")";
                            }
                            JetInfo.To = t;
                            HtmlNode Jtemp = link.SelectSingleNode(link.XPath + "/td[1]/a[1]");
                            HtmlNode JetTimeTo = link.SelectSingleNode(link.XPath + "/td[2]/strong[1]");
                            t = JetTimeTo.InnerText;
                            JetInfo.TimeTo = t;
                            HtmlNode JetFee = link.SelectSingleNode(link.XPath + "/td[4]/div[1]/label[1]");
                            if (JetFee != null)
                            {
                                t = JetFee.InnerText;
                                t = t.Remove(0, 3) + "VND";
                                JetInfo.GiaTien = t;
                            }
                            else
                            {
                                t = "không có";
                                JetInfo.GiaTien = t;
                            }
                            list.Add(JetInfo);
                        }
                    }
                }
                catch (Exception ex)
                {

                }



                #endregion
                #region VN Airlines
                if (direction == 1)
                    dic = "onewaytravel";
                else
                    dic = "returntravel";

                string[] datetimefrom = dayfrom.Split('/');
                string ngayfrom = datetimefrom[0];
                int so = Convert.ToInt32(ngayfrom);
                ngayfrom = so.ToString();
                string monthfrom = "";
                monthfrom = datetimefrom[1];
                switch (monthfrom)
                {
                    case "01":
                        monthfrom = "JAN";
                        break;
                    case "02":
                        monthfrom = "FEB";
                        break;
                    case "03":
                        monthfrom = "MAR";
                        break;
                    case "04":
                        monthfrom = "APR";
                        break;
                    case "05":
                        monthfrom = "MAY";
                        break;
                    case "06":
                        monthfrom = "JUN";
                        break;
                    case "07":
                        monthfrom = "JUL";
                        break;
                    case "08":
                        monthfrom = "AUG";
                        break;
                    case "09":
                        monthfrom = "SEP";
                        break;
                    case "10":
                        monthfrom = "OCT";
                        break;
                    case "11":
                        monthfrom = "NOV";
                        break;
                    case "12":
                        monthfrom = "DEC";
                        break;
                }
                string[] datetimeto = dayto.Split('/');
                string ngayto = datetimeto[0];
                so = Convert.ToInt32(ngayto);
                ngayto = so.ToString();
                string monthto = "";
                monthto = datetimeto[1];
                switch (monthto)
                {
                    case "01":
                        monthto = "JAN";
                        break;
                    case "02":
                        monthto = "FEB";
                        break;
                    case "03":
                        monthto = "MAR";
                        break;
                    case "04":
                        monthto = "APR";
                        break;
                    case "05":
                        monthto = "MAY";
                        break;
                    case "06":
                        monthto = "JUN";
                        break;
                    case "07":
                        monthto = "JUL";
                        break;
                    case "08":
                        monthto = "AUG";
                        break;
                    case "09":
                        monthto = "SEP";
                        break;
                    case "10":
                        monthto = "OCT";
                        break;
                    case "11":
                        monthto = "NOV";
                        break;
                    case "12":
                        monthto = "DEC";
                        break;
                }
                string Url = "https://cat.sabresonicweb.com/SSWVN/meridia?language=vi&posid=B3QE&page=requestAirMessage_air&action=airRequest&realrequestAir=realRequestAir&actionType=nonFlex&classService=CoachClass&currency=VND&"
                + "depTime=0600"
                + "&retTime=0600"
                + "&direction=" + dic
                + "&departCity=" + from
                + "&depDay=" + ngayfrom
                + "&depMonth=" + monthfrom
                + "&temp_date="
                + "&returnCity=" + to
                + "&retDay=" + ngayto
                + "&retMonth=" + monthto
                + "&temp_date=&ADT=1&CHD=0&INF=0&submit=T%C3%ACm%20chuy%E1%BA%BFn%20bay&wpf_timed_action_0VNAirline_00215IBEFastTabV2_00215bookingYourTrip_00515vn_0051513b92417f90_005158e04_1=&ShowNote=Qu%C3%BD%20kh%C3%A1ch%20c%E1%BA%A7%20nh%E1%BA%AD%20%C4%91u7847%20y%20%C4%91u7911%20%20th%C3%B4ng%20tin%20%C4%91u7875%20%20l%C3%A0m%20th%E1%BB%A7t%E1%BB%A5%20!&wclang=VI&txtFamilyName=&txtMidName=&slDeparture=&txtConfirmCode=&txtFlightNumber=&rdoDay=on&";
                HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(Url);


                int count = 0;
                string newpath = "//tr[@class='_out' or @class='_in']";
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes(newpath))
                {
                    string x = link.XPath;
                    HtmlNodeCollection flag = link.SelectNodes(x + "/td[div[@class='hoverTaxes']]");
                    foreach (HtmlNode temp in flag)
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
                "@class='matrix_cal_body matrix_cal_body_arr']";
                foreach (HtmlNode link1 in doc.DocumentNode.SelectNodes(path))
                {
                    string Xpath = link1.XPath;
                    string t;
                    if (link1.Attributes["class"].Value == "matrix_cal_body matrix_cal_body_dep")
                    {
                        #region
                        HtmlNode temp = link1.SelectSingleNode(Xpath + "/div[1]/div[2]/table[1]/tr[1]/td[1]");
                        t = temp.InnerText;
                        t = t.Replace("\r", "");
                        t = t.Replace("\n", "");
                        t = t.Replace("\t", "");
                        t = t.Replace(" ", "");
                        if (t.Contains(from))
                        {
                            t = AirPortFrom + " (" + from + ")";
                        }
                        if (t.Contains(to))
                        {
                            t = AirPortTo + " (" + to + ")"; ;
                        }
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
                        if (t.Contains(to))
                        {
                            t = AirPortTo + " (" + to + ")"; ;
                        }
                        if (t.Contains(from))
                        {
                            t = AirPortFrom + " (" + from + ")";
                        }
                        info.To = t;
                        temp = link1.SelectSingleNode(Xpath + "/div[1]/p");
                        info.TimeTo = temp.InnerText;

                        info.Image = "https://cat.sabresonicweb.com/SSWVN/application/images/B3QE/airplane.gif";
                        for (int flag = count * 5 + 4; flag >= count * 5; flag--)
                        {
                            if (fee[flag] != "Không Có")
                            {
                                info.GiaTien = fee[flag];
                                break;
                            }
                        }
                        info.Hang = "VietName Airlines";
                        list.Add(info);

                        info = new FlightInfo();

                        count++;
                        #endregion
                    }

                }
                #endregion

                #region select flight
                for (int b = 0; b < list.Count; b++)
                {
                    if (timefrom != null && timeto != null)
                    {
                        bool a = list[b].From.Contains(from);
                        if ((list[b].From.Contains(from) && list[b].TimeFrom == timefrom) || (list[b].From.Contains(to) && list[b].TimeFrom == timeto))
                        {
                            listreturn.Add(list[b]);
                        }
                    }
                    else
                    {
                        if (timefrom == null && timeto != null)
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
                        if (timefrom == null && timeto == null)
                        {
                            if (list[b].From.Contains(from) || list[b].From.Contains(to))
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
        [HttpGet]
        public List<AirPort> ListAirport(int i)
        {
            List<AirPort> list = new List<AirPort>();
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            XmlDocument Doc = new XmlDocument();
            Doc.Load(root + "/Database.XML");
            try
            {
                XmlNodeList node = Doc.SelectNodes("//Port");

                for (int ii = 0; ii < node.Count; ii++)
                {
                    AirPort port = new AirPort();
                    port.Id = node[ii].Attributes["id"].Value;
                    port.Name = node[ii].Attributes["name"].Value;
                    list.Add(port);
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