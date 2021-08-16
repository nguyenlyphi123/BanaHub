using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanaHubWeb.Models;
using PagedList;

namespace BanaHubWeb.Controllers
{
    public class FilterController : Controller
    {
        BanaHubDataContext data = new BanaHubDataContext();
        // GET: Filter
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Filter(int? page)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string str = Convert.ToString(Request.QueryString["fil"]);
            int iPageSize = 16;
            int iPageNum = (page ?? 1);
            ViewBag.MaLoai = id;
            if (String.Compare(str, "a-z", true) == 0)
            {
                var list = from l in data.SANPHAMs where l.MaLoai == id orderby l.TenSP select l;
                return View(list.ToPagedList(iPageNum, iPageSize));
            }
            else if (String.Compare(str, "price-asc", true) == 0)
            {
                var list = from l in data.SANPHAMs where l.MaLoai == id orderby l.GiaBan ascending select l;
                return View(list.ToPagedList(iPageNum, iPageSize));
            }
            else
            {
                var list = from l in data.SANPHAMs where l.MaLoai == id orderby l.GiaBan descending select l;
                return View(list.ToPagedList(iPageNum, iPageSize));
            }

        }
    }
}