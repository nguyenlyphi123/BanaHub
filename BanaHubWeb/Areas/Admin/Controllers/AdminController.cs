using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanaHubWeb.Models;

namespace BanaHubWeb.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        BanaHubDataContext data = new BanaHubDataContext();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if(Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Login(FormCollection f)
        {
            var sTenDN = f["TenDN"];
            var sMatKhau = f["MatKhau"];

            ADMIN ad = data.ADMINs.SingleOrDefault(n => n.TenAdmin == sTenDN && n.Passowrd == sMatKhau);

            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Admin");
            }
            else
                ViewBag.ThongBao = "Email hoặc mật khẩu không đúng";

            return View();
        } 
    }
}