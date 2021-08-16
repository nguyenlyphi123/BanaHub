using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanaHubWeb.Models;
using PagedList.Mvc;
using PagedList;

namespace BanaHubWeb.Controllers
{
    public class SeachController : Controller
    {
        BanaHubDataContext data = new BanaHubDataContext();
        // GET: Seach
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string str, int ? page)
        {
            int iPageSize = 16;
            int iPageNum = (page ?? 1);
            ViewBag.MaLoai = 1;
            ViewBag.Search = str;
            if(!string.IsNullOrEmpty(str))
            {
                var kq = from sp in data.SANPHAMs where sp.TenSP.Contains(str) || sp.MoTa.Contains(str) orderby sp.GiaBan ascending select sp;
                return View(kq.ToPagedList(iPageNum, iPageSize));
            }
            else
            {
                var kqs = from sp in data.SANPHAMs select sp;
                return View(kqs.ToPagedList(iPageNum, iPageSize));
            }
            
        }
    }
}