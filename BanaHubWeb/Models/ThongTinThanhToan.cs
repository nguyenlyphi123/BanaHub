using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanaHubWeb.Models
{
    public class ThongTinThanhToan
    {
        BanaHubDataContext data = new BanaHubDataContext();
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

        /*public ThongTinThanhToan(int makh) {
            ID = makh;
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == makh);
            THONGTINKH ttkh = data.THONGTINKHs.SingleOrDefault(n => n.MaKH == makh);

            Name = kh.TenKH;
            Email = kh.Email;
            Password = kh.Passowrd;
            Address = ttkh.DiaChi;
            City = ttkh.Tinh;
            PhoneNumber = ttkh.SDT;
        }*/
    }
}