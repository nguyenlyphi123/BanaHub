using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BanaHubWeb.Models;
using Facebook;

namespace BanaHubWeb.Controllers
{
    public class UserController : Controller
    {
        BanaHubDataContext data = new BanaHubDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            var sTenDN = f["TenDN"];
            var sMatKhau = f["MatKhau"];

            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Tên đăng nhập không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.Email == sTenDN && n.Passowrd == getMD5(sMatKhau) || n.Passowrd == sMatKhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    if (f["remember"].Contains("true"))
                    {
                        Response.Cookies["TenDN"].Value = sTenDN;
                        Response.Cookies["MatKhau"].Value = sMatKhau;
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(1);
                    }
                    else
                    {
                        Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(-1);
                    }

                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return RedirectToAction("DatHang", "GioHang");
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection f, KHACHHANG kh)
        {
            var sHoTen = f["TenKH"];
            var sTaiKhoan = f["TenDN"];
            var sMatKhau = f["MatKhau"];
            var sNhapLaiMK = f["NhapLaiMatKhau"];

            if (sMatKhau != sNhapLaiMK)
            {
                ViewBag.ThongBao = "Mật khẩu nhập lại không khớp";
            }
            else if (sMatKhau.Length < 6)
            {
                ViewBag.Err = "Mật khẩu phải trên 6 ký tự";
            }
            else if (data.KHACHHANGs.SingleOrDefault(n => n.TenKH == sHoTen) != null)
            {
                ViewBag.ThongBao = "Tên khách hàng đã tồn tại";
            }
            else if (data.KHACHHANGs.SingleOrDefault(n => n.Email == sTaiKhoan) != null)
            {
                ViewBag.ThongBao = "Email đã được sủ dụng";
            }
            else
            {
                kh.TenKH = sHoTen;
                kh.Email = sTaiKhoan;
                kh.Passowrd = getMD5(sMatKhau);
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("DangNhap", "User");
            }
            return this.DangKy();
        }

        public ActionResult DangXuat()
        {
            Session["kh"] = null;
            return RedirectToAction("DangNhap", "User");
        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string tenTK = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                KHACHHANG kh = new KHACHHANG();
                kh.Email = email;
                kh.TenKH = lastname + " " + middlename + " " + firstname;
                if (data.KHACHHANGs.SingleOrDefault(n => n.Email == email) == null)
                {
                    data.KHACHHANGs.InsertOnSubmit(kh);
                    data.SubmitChanges();
                }
                else
                {
                    data.SubmitChanges();
                }
                Session["TaiKhoan"] = kh;
            }
            return Redirect("/GioHang/GioHang/");
        }
        public static string getMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("X2");
            }
            return byte2String;
        }
    }
}