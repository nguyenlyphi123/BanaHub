using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanaHubWeb.Models;
using PagedList;
using PagedList.Mvc;

namespace BanaHubWeb.Controllers
{
    public class SanPhamDetailController : Controller
    {
        BanaHubDataContext data = new BanaHubDataContext();
        // GET: SanPhamDetail
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult XuLyPartial(int id, int ? page)
        {
            int iPageSize = 16;
            int iPageNum = (page ?? 1);
            ViewBag.MaLoai = id;
            var list = from l in data.SANPHAMs where l.MaLoai == id select l;
            return View(list.ToPagedList(iPageNum, iPageSize));
        }

        public ActionResult SanPhamLinkPartial()
        {
            var list = from l in data.LOAIs select l;
            return View(list);
        }


        public ActionResult ChiTietSanPham(int id, int maloai)
        {
            var list = from l in data.SANPHAMs where l.MaSP == id select l;
            ViewBag.MaLoai = maloai;
            return View(list);
        }

        public ActionResult MoreImage(int id)
        {
            var list = from l in data.MOREIMAGEs where l.MaSP == id select l;
            return View(list);
        }
    }
}