using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BanaHubWeb.Models;

namespace BanaHubWeb.Controllers
{
    public class GioHangController : Controller
    {
        static int check = 0;
        BanaHubDataContext data = new BanaHubDataContext();
        
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int masp, string url)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(n => n.iMaSP == masp);
            if(sp == null)
            {
                sp = new GioHang(masp);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }

        
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            
            return dTongTien;
        }

        public ActionResult GioHang(string discount)
        {
            List<GioHang> lstGioHang = LayGioHang();
            if(lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "BanaHub");
            }
            ViewBag.TongSoLuong = TongSoLuong();

            if(discount != null && discount == "GiamGia")
            {
                ViewBag.TongTien = TongTien() - TongTien() * 10 / 100;
                check = 1;
            }
            else
            {
                ViewBag.TongTien = TongTien();
            }

            ViewBag.discount = discount;
            return View(lstGioHang);
        }

        public ActionResult CapNhatGioHang(int masp, FormCollection f)
        {
            List<GioHang> list = LayGioHang();
            GioHang sp = list.SingleOrDefault(n => n.iMaSP == masp);

            if(sp != null)
            {
                sp.iSoLuong = int.Parse(f["iSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CartPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult XoaKhoiGioHang(int masp)
        {
            List<GioHang> list = LayGioHang();
            GioHang sp = list.SingleOrDefault(n => n.iMaSP == masp);

            if(sp != null)
            {
                list.RemoveAll(n => n.iMaSP == masp);
                if(list.Count == 0)
                {
                    return RedirectToAction("SanPham", "BanaHub");
                }
            }
            return RedirectToAction("GioHang", "GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if(Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }

            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "BanaHub");
            }
            List<GioHang> list = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(list);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection f, string email)
        {
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<GioHang> list = LayGioHang();

            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = 1;
            ddh.DaThanhToan = false;
            data.DONDATHANGs.InsertOnSubmit(ddh);
            data.SubmitChanges();

            var ttkh = data.THONGTINKHs.SingleOrDefault(n => n.MaKH == kh.MaKH);
            ViewBag.DiaChi = f["DiaChi"];
            ViewBag.Tinh = f["Tinh"];
            ViewBag.SDT = f["SDT"];

            
            if (ttkh != null)
            {
                ttkh.DiaChi = f["DiaChi"];
                ttkh.Tinh = f["Tinh"];
                ttkh.SDT = f["SDT"];
                data.SubmitChanges();
            }
            else
            {
                THONGTINKH ti = new THONGTINKH();
                ti.MaKH = kh.MaKH;
                ti.DiaChi = f["DiaChi"];
                ti.Tinh = f["Tinh"];
                ti.SDT = f["SDT"];
                data.THONGTINKHs.InsertOnSubmit(ti);
                data.SubmitChanges();
            }

            foreach (var item in list)
            {
                CTDH ctdh = new CTDH();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaSP = item.iMaSP;
                ctdh.SoLuong = item.iSoLuong;
                if (check == 1)
                {
                    ctdh.DonGia = (long)item.dDonGia - (long)item.dDonGia * 10 / 100;
                    ctdh.ThanhTien = long.Parse((item.iSoLuong * (item.dDonGia - item.dDonGia * 10 / 100)).ToString());
                }
                else
                {
                    ctdh.DonGia = (long)item.dDonGia;
                    ctdh.ThanhTien = long.Parse((item.iSoLuong * item.dDonGia).ToString());
                }
                
               
                data.CTDHs.InsertOnSubmit(ctdh);

                var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == item.iMaSP);
                int conlai = sp.SoLuongBan.Value;
                sp.SoLuongBan = conlai - item.iSoLuong;
                data.SubmitChanges();
            }
            
            data.SubmitChanges();
            Session["GioHang"] = null;
            ViewBag.TongSoLuong = 0;

            var thanhtien = (from s in data.CTDHs where s.MaDonHang == ddh.MaDonHang select s.ThanhTien).Sum();
            var soluong = (from s in data.CTDHs where s.MaDonHang == ddh.MaDonHang select s.SoLuong).Sum();

            string body = "Mã đơn hàng: " + ddh.MaDonHang + " | Ngày đặt: " + ddh.NgayDat + " | Số lượng = " + soluong + " | Tổng hóa đơn: " + string.Format("{0:##,##0,0}", thanhtien) + " VNĐ";
            string subject = "Cảm ơn quý khách vì đã đặt hàng bên BanaHub";

            WebMail.Send(email, subject, body, null, null, null, true, null, null, null, null, null, null);
            check = 0;
            return RedirectToAction("XacNhanDatHang", "GioHang");
        }

        public ActionResult XacNhanDatHang()
        {
            List<GioHang> list = LayGioHang();
            return View(list);
        }
    }
}