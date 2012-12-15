using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchFlight.Models
{
    public class FlightInfo
    {
        public string From {get;set;}
        public string To{get;set;}
        public string TimeFrom{get;set;}
        public string TimeTo{get;set;}
        public string Image { get; set; }
        public string MaChuyenBay { get; set; }
        public string LoaiMayBay { get; set; }

        public string ThuongGia{get;set;}
        public string sttThuongGia { get; set; }
        public string PTLinhHoat{get;set;}
        public string sttPTLinhHoat { get; set; }
        public string TKLinhHoat{get;set;}
        public string sttTKLinhHoat { get; set; }
        public string TietKiem{get;set;}
        public string sttTietKiem { get; set; }
        public string SieuTK { get; set; }
        public string sttSieuTK { get; set; }
        
    }
}