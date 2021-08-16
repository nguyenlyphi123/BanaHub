using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanaHubWeb.Models;
using PagedList;

namespace BanaHubWeb.Controllers
{
    public class BanaHubController : Controller
    {
        BanaHubDataContext data = new BanaHubDataContext();
        // GET: BanaHub
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ViXuLyPartial()
        {
            var listViXuLy = (from vxl in data.SANPHAMs where vxl.MaLoai == 1 select vxl).Take(5);
            return PartialView(listViXuLy);
        }

        public ActionResult CasePartial()
        {
            var listCase = (from cs in data.SANPHAMs where cs.MaLoai == 2 select cs).Take(5);
            return PartialView(listCase);
        }

        public ActionResult PhuKienPartial()
        {
            var listPhuKien = (from pk in data.SANPHAMs where pk.MaLoai == 3 select pk).Take(5);
            return PartialView(listPhuKien);
        }

        public void LuotXem(int id)
        {
            var listSP = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            int luotxem;
            if (listSP != null)
            {
                if (listSP.LuotXem != null)
                {
                    luotxem = int.Parse(listSP.LuotXem.ToString());
                }
                else
                {
                    luotxem = 0;
                }
                listSP.LuotXem = luotxem++;
                data.SubmitChanges();
            }
        }
        public ActionResult KhuyenMaiPartial()
        {
            var listKhuyenMai = from km in data.KHUYENMAIs select km;
            return PartialView(listKhuyenMai);
        }

        public ActionResult SanPhamSliderPartial()
        {
            var listSanPhamS = from sp in data.LOAIs select sp;
            return PartialView(listSanPhamS);
        }

        public ActionResult PhuKienSliderPartial(int id)
        {
            var listPhuKien = (from pk in data.SANPHAMs where pk.MaLoai == id select pk).Take(4);
            return PartialView(listPhuKien);
        }

        public ActionResult BanChay()
        {
            var listPhuKien = (from pk in data.SANPHAMs orderby (pk.SoLuong - pk.SoLuongBan) descending select pk).Take(4);
            return PartialView(listPhuKien);
        }

        public ActionResult NoiBat()
        {
            var listPhuKien = (from pk in data.SANPHAMs orderby pk.LuotXem descending select pk).Take(4);
            return PartialView(listPhuKien);
        }

        public ActionResult GiaTot()
        {
            var listPhuKien = (from pk in data.SANPHAMs orderby pk.GiaBan ascending select pk).Take(4);
            return PartialView(listPhuKien);
        }

        public ActionResult SanPham(int ? page)
        {
            int iPageNumber = (page ?? 1);
            int iPageSize = 20;
            var list = from l in data.SANPHAMs select l;
            return View(list.ToPagedList(iPageNumber, iPageSize));
        }

        /*public ActionResult SanPham(int id)
        {
            ViewBag.MaLoai = id;
            var list = from l in data.SANPHAMs where l.MaLoai == id select l;
            return View(list);
        }*/

        public ActionResult DeXuat(int id)
        {
            ViewBag.MaLoai = id;
            var list = (from l in data.SANPHAMs where l.MaLoai == id orderby (l.SoLuong - l.SoLuongBan) descending select l).Take(5);
            return PartialView(list);
        }

        public ActionResult SanPhamLinkPartial()
        {
            var list = from l in data.LOAIs select l;
            return View(list);
        }

        public ActionResult XuLyPartial(int id, int ? page)
        {
            int iPageSize = 10;
            int iPageNumber = (page ?? 1);
            ViewBag.MaLoai = id;
            var list = from l in data.SANPHAMs where l.MaLoai == id select l;
            return View(list.ToPagedList(iPageNumber, iPageSize));
        }

    }
}