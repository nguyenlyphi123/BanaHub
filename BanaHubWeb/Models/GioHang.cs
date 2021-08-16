using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanaHubWeb.Models
{
    public class GioHang
    {
        BanaHubDataContext data = new BanaHubDataContext();

        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien { get { return iSoLuong * dDonGia; } }

        public GioHang(int masp)
        {
            iMaSP = masp;
            SANPHAM sp = data.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = sp.TenSP;
            sAnhBia = sp.AnhBia;
            dDonGia = double.Parse(sp.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}