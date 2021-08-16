using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanaHubWeb.Models;
using PagedList;
using PagedList.Mvc;


namespace BanaHubWeb.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        BanaHubDataContext data = new BanaHubDataContext();
        // GET: Admin/SanPham
        public ActionResult Index(int ? page, FormCollection f)
        {
            int iPageNum = (page ?? 1);
            int iPageZise = 10;

            ViewBag.Search = f["strSearch"];
            if(ViewBag.Search != null)
            {
                var view = from s in data.SANPHAMs where s.MoTa.Contains(f["strSearch"]) || s.TenSP.Contains(f["strSearch"]) select s;
                return View(view.ToPagedList(iPageNum, iPageZise));
            }

            return View(data.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageZise));
        }

        public ActionResult Customer(int ? page, FormCollection f)
        {
            int iPageNum = (page ?? 1);
            int iPageZise = 10;

            var cus = from kh in data.KHACHHANGs
                      join ttkh in data.THONGTINKHs on kh.MaKH equals ttkh.MaKH
                      select new ThongTinThanhToan
                      {
                          ID = kh.MaKH,
                          Name = kh.TenKH,
                          Email = kh.Email,
                          Password = kh.Passowrd,
                          Address = ttkh.DiaChi,
                          City = ttkh.Tinh,
                          PhoneNumber = ttkh.SDT
                      };

            ViewBag.Search = f["strSearch"];
            if (ViewBag.Search != null)
            {
                var view = from s in cus where s.Name.Contains(f["strSearch"]) || s.Email.Contains(f["strSearch"]) || s.Address.Contains(f["strSearch"]) || s.City.Contains(f["strSearch"]) || s.PhoneNumber.Contains(f["strSearch"]) select s;
                return View(view.ToPagedList(iPageNum, iPageZise));
            }

            return View(cus.ToList().OrderBy(n => n.ID).ToPagedList(iPageNum, iPageZise));
        }

        public ActionResult TypeOfProduct(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageZise = 10;

            return View(data.LOAIs.ToList().OrderBy(n => n.MaLoai).ToPagedList(iPageNum, iPageZise));
        }

        public ActionResult Order(int ? page, FormCollection f)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 10;

            var order = from dh in data.DONDATHANGs
                        join ctdh in data.CTDHs on dh.MaDonHang equals ctdh.MaDonHang
                        group ctdh by dh into g
                        
                        select new Order
                        {
                            ID = g.Key.MaDonHang,
                            DayOrder = (g.Key.NgayDat).Value,
                            cusID = int.Parse(g.Key.MaKH.ToString()),
                            Count = (from s in data.CTDHs where s.MaDonHang == g.Key.MaDonHang select s.MaSP).Count(),
                            Sum = (from s in data.CTDHs where s.MaDonHang == g.Key.MaDonHang select s.SoLuong).Sum(),
                            Price = long.Parse((from s in data.CTDHs where s.MaDonHang == g.Key.MaDonHang select s.ThanhTien).Sum().ToString())
                        };

            ViewBag.Ma = f["madh"];
            
            ViewBag.Date = f["fromDate"];
            if(f["fromDate"] != null)
            {
                ViewBag.DoanhThu = (from s in data.CTDHs where s.DONDATHANG.NgayDat >= DateTime.Parse(f["fromDate"]) && s.DONDATHANG.NgayDat <= DateTime.Now select s.ThanhTien).Sum();
                var test = from s in order where s.DayOrder >= DateTime.Parse(f["fromDate"]) && s.DayOrder <= DateTime.Now select s;
                return View(test.ToList().OrderByDescending(n => n.ID).ToPagedList(iPageNum, iPageSize));
            }
            else if(ViewBag.Ma != null)
            {
                ViewBag.DoanhThu = (from s in data.CTDHs where s.MaDonHang == int.Parse(f["madh"]) select s.ThanhTien).Sum();
                var search = from s in order where s.ID == int.Parse(f["madh"]) select s;
                return View(search.ToList().OrderByDescending(n => n.ID).ToPagedList(iPageNum, iPageSize));
            }
            ViewBag.DoanhThu = (from s in data.CTDHs select s.ThanhTien).Sum();
            return View(order.ToList().OrderByDescending(n => n.ID).ToPagedList(iPageNum, iPageSize));
        }

        public ActionResult OrderDetail(int id)
        {
            var bangtong = from dh in data.DONDATHANGs
                           join ct in data.CTDHs on dh.MaDonHang equals ct.MaDonHang
                           where dh.MaDonHang == id
                           select new
                           {
                               dh.MaDonHang,
                               dh.MaKH,
                               ct.MaSP,
                               ct.SoLuong,
                               ct.ThanhTien
                           };

            var layten = from dh in bangtong
                         join kh in data.KHACHHANGs on dh.MaKH equals kh.MaKH
                         select new
                         {
                             dh.MaDonHang,
                             kh.TenKH,
                             dh.MaSP,
                             dh.SoLuong,
                             dh.ThanhTien
                         };

            var laysanpham = from dh in layten
                             join sp in data.SANPHAMs on dh.MaSP equals sp.MaSP
                             select new OrderDetail
                             {
                                 ID = dh.MaDonHang,
                                 cusName = dh.TenKH,
                                 proName = sp.TenSP,
                                 proNum = int.Parse(dh.SoLuong.ToString()),
                                 Price = long.Parse(dh.ThanhTien .ToString())
                             };
            return View(laysanpham.ToList().OrderBy(n => n.Price));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Maloai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection f, SANPHAM sp, HttpPostedFileBase fFileUpload, HttpPostedFileBase fFileUpload2, HttpPostedFileBase fFileUpload3, HttpPostedFileBase fFileUpload4)
        {
            ViewBag.Maloai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            
            if(fFileUpload == null)
            {
                ViewBag.ThongBao = "Hãy chọn ảnh";
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.GiaBan = long.Parse(f["mGiaBan"]);
                ViewBag.Maloai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", int.Parse(f["MaLoai"]));
                return View();
            }
            else
            {
                if(ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if(!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sp.TenSP = f["sTenSP"];
                    sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                    sp.AnhBia = sFileName;
                    sp.SoLuong = int.Parse(f["iSoLuong"]);
                    sp.SoLuongBan = int.Parse(f["iSoLuong"]);
                    sp.GiaBan = long.Parse(f["mGiaBan"]);
                    sp.MaLoai = int.Parse(f["MaLoai"]);
                    data.SANPHAMs.InsertOnSubmit(sp);
                    data.SubmitChanges();

                    var sFileName2 = Path.GetFileName(fFileUpload2.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/Images"), sFileName2);
                    var sFileName3 = Path.GetFileName(fFileUpload3.FileName);
                    var path3 = Path.Combine(Server.MapPath("~/Images"), sFileName3);
                    var sFileName4 = Path.GetFileName(fFileUpload4.FileName);
                    var path4 = Path.Combine(Server.MapPath("~/Images"), sFileName4);

                    MOREIMAGE img = new MOREIMAGE();
                    img.MaSP = sp.MaSP;
                    img.HinhChinh = sFileName;
                    img.Hinh2 = sFileName2;
                    img.Hinh3 = sFileName3;
                    img.Hinh4 = sFileName4;
                    data.MOREIMAGEs.InsertOnSubmit(img);
                    data.SubmitChanges();

                    return RedirectToAction("Index", "SanPham");
                }
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if(sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConFirm(int id, FormCollection f)
        {
            var connect = data.MOREIMAGEs.SingleOrDefault(n => n.MaSP == id);
            data.MOREIMAGEs.DeleteOnSubmit(connect);
            data.SubmitChanges();

            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var ctdh = data.CTDHs.Where(n => n.MaSP == id);
            if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Khi thực thao tác này sẽ xóa luôn trong chi tiết đơn hàng";
                return View(sp);
            }

            data.SANPHAMs.DeleteOnSubmit(sp);
            data.SubmitChanges();

           
            return RedirectToAction("Index", "SanPham");
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var sps = from s in data.SANPHAMs
                     join img in data.MOREIMAGEs on s.MaSP equals img.MaSP
                     where s.MaSP == id
                     group s by img into g
                     select new ProductDetail
                     {
                         MaSP = int.Parse(g.Key.MaSP.ToString()),
                         TenSP = g.Key.SANPHAM.TenSP,
                         MoTa = g.Key.SANPHAM.MoTa,
                         GiaBan = long.Parse(g.Key.SANPHAM.GiaBan.ToString()),
                         SoLuong = int.Parse(g.Key.SANPHAM.SoLuong.ToString()),
                         SoLuongBan = int.Parse(g.Key.SANPHAM.SoLuongBan.ToString()),
                         HinhChinh = g.Key.HinhChinh,
                         Hinh2 = g.Key.Hinh2,
                         Hinh3 = g.Key.Hinh3,
                         Hinh4 = g.Key.Hinh4
                     };
            ViewBag.MaSP = sps.Select(n => n.MaSP);

            /*var sps = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);*/
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", sp.MaLoai);
            return View(sp);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload, HttpPostedFileBase fFileUpload2, HttpPostedFileBase fFileUpload3, HttpPostedFileBase fFileUpload4)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == int.Parse(f["iMaSP"]));
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", sp.MaLoai);
            var img = data.MOREIMAGEs.SingleOrDefault(n => n.MaSP == int.Parse(f["iMaSP"]));
            if (img == null)
            {
                MOREIMAGE mor = new MOREIMAGE();
                mor.MaSP = int.Parse(f["iMaSP"]);
                data.MOREIMAGEs.InsertOnSubmit(mor);
                data.SubmitChanges();
            }

            if (ModelState.IsValid)
            {
                if(fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);

                    if(!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sp.AnhBia = sFileName;                  
                }
                
                sp.TenSP = f["sTenSP"];
                sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                sp.SoLuong = int.Parse(f["iSoLuong"]);
                sp.SoLuongBan = int.Parse(f["iSoLuongBan"]);
                sp.GiaBan = decimal.Parse(f["mGiaBan"]);
                sp.MaLoai = int.Parse(f["MaLoai"]);
                /*data.SubmitChanges();*/

                var imgs = data.MOREIMAGEs.SingleOrDefault(n => n.MaSP == int.Parse(f["iMaSP"]));

                if (fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);

                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }

                    imgs.HinhChinh = sFileName;
                }
              
                if (fFileUpload2 != null)
                {
                    var sFileName2 = Path.GetFileName(fFileUpload2.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName2);

                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }

                    imgs.Hinh2 = sFileName2;
                }

                if (fFileUpload3 != null)
                {
                    var sFileName3 = Path.GetFileName(fFileUpload3.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName3);

                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }

                    imgs.Hinh3 = sFileName3;
                }

                if (fFileUpload4 != null)
                {
                    var sFileName4 = Path.GetFileName(fFileUpload4.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName4);

                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }

                    imgs.Hinh4 = sFileName4;
                }
                data.SubmitChanges();

                return RedirectToAction("Index", "SanPham");
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(FormCollection f, LOAI loai)
        {
            if(ModelState.IsValid)
            {
                loai.TenLoai = f["sTenLoai"];
                data.LOAIs.InsertOnSubmit(loai);
                data.SubmitChanges();
            }
            return RedirectToAction("TypeOfProduct", "SanPham");
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var loai = data.LOAIs.SingleOrDefault(n => n.MaLoai == id);

            return View(loai);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(FormCollection f)
        {
            var loai = data.LOAIs.SingleOrDefault(n => n.MaLoai == int.Parse(f["iMaLoai"]));

            loai.TenLoai = f["sTenLoai"];
            data.SubmitChanges();

            return RedirectToAction("TypeOfProduct", "SanPham");
        }


        public ActionResult Revenue()
        {
            long totalMoney = data.CTDHs.Sum(n => n.ThanhTien).Value;
            int totalNum = data.CTDHs.Sum(n => n.SoLuong).Value;
            int totalBill = (from s in data.DONDATHANGs select s.MaDonHang).Count();
            ViewBag.TotalMoney = totalMoney;
            ViewBag.TotalNum = totalNum;
            ViewBag.TotalBill = totalBill;
            return View();
        }
    }
}