
create table LOAI
(
	MaLoai int primary key identity(1,1),
	TenLoai nvarchar(50) not null
)

create table SANPHAM
(
	MaSP int primary key identity(1,1),
	TenSP nvarchar(50) not null,
	MoTa ntext,
	GiaBan money check (GiaBan >= 0),
	SoLuong int,
	SoLuongBan int check(SoLuongBan > 0),
	MaLoai int,
	AnhBia varchar(30),
	constraint fk_sp_loai foreign key(MaLoai) references LOAI(MaLoai)
)


create table KHACHHANG
(
	MaKH int primary key identity(1,1),
	TenKH nvarchar(50) not null,
	Email varchar(50) not null,
	Passowrd varchar(20) not null
)

create table DONDATHANG
(
	MaDonHang int primary key identity(1,1),
	DaThanhToan BIT DEFAULT(0),
	TinhTrangGiaoHang INT DEFAULT(1),
	NgayDat SMALLDATETIME,
	MaKH INT,
	constraint fk_dondathang_khachhang foreign key(MaKH) references KHACHHANG(MaKH)
)

create table CTDH
(
	MaDonHang int,
	MaSP int,
	SoLuong int check(SoLuong > 0),
	DonGia bigint check(DonGia >= 0)
	constraint PK_CTDH primary key(MaDonHang,MaSP),
	constraint fk_ctdh_dondathang foreign key(MaDonHang) references DONDATHANG(MaDonHang),
	constraint fk_ctdh_sanpham foreign key(MaSP) references SANPHAM(MaSP)
)

create table KHUYENMAI
(
	MaSp int,
	MaLoai int,
	TenSp nvarchar(50),
	Giaban money,
	Anhbia varchar(30)
	constraint fk_khuyenmai_sanpham foreign key(MaSP) references SANPHAM(MaSP),
	constraint fk_khuyenmai_loai foreign key(MaLoai) references LOAI(MaLoai)
)

insert into LOAI(TenLoai) values (N'Vi xử lý')
insert into LOAI(TenLoai) values (N'CASE')
insert into LOAI(TenLoai) values (N'Phụ kiện')

insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU ADM Ryzen Threadripper 3970X', N'CPU Threadripper thế hệ thứ 3 được mong chờ của AMD
24 nhân & 48 luồng
Xung cơ bản: 3.8 GHz
Xung tối đa (boost): 4.5 GHz
Chạy tốt trên các mainboard socket sTRX4
Phù hợp cho những nhà sáng tạo nội dung', 51899000, 100, 100, 1, 'cpu1.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU ADM Ryzen 5 3400G', N'APU Ryzen thế hệ thứ 3, tiến trình sản xuất 12nm
4 nhân, 8 luồng, xung nhịp mặc định 3.7 GHz, xung nhịp boost tối đa 4.2 GHz
Tích hợp Radeon™ RX Vega 11 Graphics
Hỗ trợ PCI-e 3.0
Có hỗ trợ ép xung
Đi kèm tản nhiệt Wraith Spire', 4299000, 100, 100, 1, 'cpu2.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU INTEL CORE I9-9900KF', N'Phiên bản cắt giảm đi nhân đồ họa tích hợp của 9900K
Số nhân: 8
Số luồng: 16
Tốc độ cơ bản: 3.6 GHz
Tốc độ tối đa: 5.0 GHz
Cache: 16MB
Tiến trình sản xuất: 14nm', 11999000, 100, 100, 1, 'cpu3.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU INTEL CORE I9-10940X', N'CPU Core i thế hệ thứ 10 của Intel
14 nhân & 28 luồng
Xung cơ bản: 3.3 GHz
Xung tối đa (boost): 4.6 GHz
Chạy tốt trên các mainboard socket 2066
Phù hợp cho những nhà sáng tạo nội dung', 23999000, 100, 100, 1, 'cpu4.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU INTEL CORE I3-10105F', N'Dòng CPU Core i thế hệ thứ 11 của Intel
Socket: LGA 1200
Thế hệ: Rocket Lake
Số nhân: 4
Số luồng: 8
Xung nhịp: 3.7 - 4.4 Ghz
*KHÔNG CÓ GPU TÍCH HỢP', 2929000, 100, 100, 1, 'cpu5.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU INTEL XEON SILVER 4210', N'Dòng sản phẩm chuyên biệt dành cho server/máy trạm
10 nhân & 20 luồng
Xung nhịp: 2.2GHz (Cơ bản) / 3.2GHz (Boost)
Socket: LGA 3647
Hỗ trợ RAM ECC
Không kèm quạt tản nhiệt từ hãng
Không tích hợp sẵn iGPU', 13499000, 100, 100, 1, 'cpu6.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU INTEL CORE I9-10900F', N'Dòng Core i9 thế hệ thứ 10 dành cho máy bàn của Intel
10 nhân & 20 luồng
Xung nhịp: 2.8GHz (Cơ bản) / 5.2GHz (Boost)
Socket: LGA1200
Không tích hợp sẵn iGPU
Không được mở khóa hệ số nhân', 10799000, 100, 100, 1, 'cpu7.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU ADM ATHLON 3000G', N'APU Ryzen thế hệ thứ 3, tiến trình sản xuất 12nm
4 nhân, 8 luồng, xung nhịp mặc định 3.7 GHz, xung nhịp boost tối đa 4.2 GHz
Tích hợp Radeon™ RX Vega 11 Graphics
Hỗ trợ PCI-e 3.0
Có hỗ trợ ép xung
Đi kèm tản nhiệt Wraith Spire', 1799000, 100, 100, 1, 'cpu8.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU INTEL PENTIUM GOLD G5420', N'Dòng sản phẩm cơ bản đến từ Intel
2 nhân & 4 luồng
Xung nhịp: 3.8GHz (Tối đa)
Socket: LGA1200
Đã kèm sẵn tản nhiệt từ hãng
Đã tích hợp sẵn iGPU', 1599000, 100, 100, 1, 'cpu9.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CPU INTEL XEON E-2236', N'Dòng sản phẩm chuyên biệt dành cho máy trạm giá rẻ
Phù hợp cho các phần mềm render, thiết kế
6 nhân & 12 luồng
Xung nhịp: 3.4 GHz (Cơ bản) / 4.8 GHz (Boost)
Socket: LGA 1151v2 (C246)
Hỗ trợ RAM ECC
Không tích hợp sẵn iGPU', 7899000, 100, 100, 1, 'cpu10.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CASE THERMALTAKE VIEW 37', N'Hỗ trợ mainboard: E-ATX, ATX, Micro ATX, Mini ITX
Khay lắp ổ bên ngoài: 3 x 3.5" or 2.5"
Khay lắp ổ phía sau: 8 x 2.5" or 4 x 3.5”
Chiều dài tối đa GPU: 410mm ( khi không lắp quạt)
Chiều cao tối đa tản nhiệt CPU: 180mm', 3899000, 100, 100, 2, 'case1.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CASE CORSAIR 680X RGB TG', N'4 Quạt và thiết kế giúp tối ưu đường gió tới các linh kiện cần thiết
Làm tỏa sáng hệ thống của bạn với tổng cộng 48 bóng đèn từ 3 quạt LL120 RGB đi kèm
Hub Corsair Lightning Node PRO biến 680X RGB thành chiếc vỏ case thông minh, bằng phần mềm iCUE mang tới khả năng điều khiển LED đồng bộ toàn hệ thống với các thành phần khác tương thích sử dụng chung Corsair iCUE, bao gồm quạt, đèn led, Ram, bàn phím, chuột, v.v....
Thiết kế 2 khoang giúp dễ dàng lắp đặt hệ thống và giấu dây gọn gàng
3 tấm kính cường lực trong suốt ở mặt trước, nóc và hông giúp khoe trọn bộ hệ thống cao cấp của bạn
Hỗ trợ tối đa tới 8 quạt 120mm hoặc 7 quạt 140mm tăng tối đa khả năng làm mát hệ thống, cùng với thiết kế hỗ trợ tới 4 radiator - tới 360mm như Corsair Hydro Series H150i PRO
USB 3.1 Gen-2 Type-C tương thích với các sản phẩm công nghệ mới nhất trong tầm tay
Dễ dàng lắp đặt tới 3 ổ cứng 3.5" và 4 ổ cứng 2,5" mà không cần tới tua vít
Đặt GPU dọc để khoe chiếc GPU mới nhất của bạn', 5899000, 100, 100, 2, 'case2.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CASE COOLER MASTER', N'Hỗ trợ mainboard: Mini ITX, Micro ATX, ATX, E-ATX (tối đa 12 x 10.7 inch)
Radiator lắp đặt tối đa: 2x280/360mm và 1 x 120/140mm
Chiều cao tản nhiệt CPU tối đa: 190mm
Chiều dài VGA: 412mm', 4099000, 100, 100, 2, 'case3.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CASE KENOO 2810BK', N'Kiểu dáng : kiểu dáng đẹp, khung chắc chắn, tản nhiệt tốt, nút bấn nhậy độ bền cao, hệ thống bảo vệ lưới sắt mặt trước và khóa mặt sau, khay chạy Cáp , Chất liệu nhựa không tái chế bảo vệ môi trường
Chất liệu: Thép mạ SECC cách điện, nhiệt + Mặt Nhựa (ABS) + Lưới sắt chống trộm - Màu: Đen mặt xám', 299000, 100, 100, 2, 'case4.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values ('CASE CORSAIR 680B RGB TG', 'H? tr? mainboard: E-ATX, ATX, Micro ATX, Mini ITX
Khay l?p ? bên ngoài: 3 x 3.5" or 2.5"
Khay l?p ? phía sau: 8 x 2.5" or 4 x 3.5”
Chi?u dài t?i da GPU: 410mm ( khi không l?p qu?t)
Chi?u cao t?i da t?n nhi?t CPU: 180mm', 5899000, 100, 100, 2, 'case5.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CASE SAMA JAZOVU PLUS XII', N'Hỗ trợ Mainboard: ATX, MicroATX, Mini-ITX
Kích thước : 423(L)×190(W)×434(H)mm
Chất liệu : Thép SPCC 0.55mm - ABS + Lưới kim loại
Cổng kết nối: 2*USB2.0 + 1*USB 3.0 + HD Audio + Led controller
Hỗ trợ ổ đĩa : 5.25"" x0 | 3.5'' x2 | 2.5'' x2
Khe mở rộng : 7 slots
Hỗ trợ Rad: 240 / 360 mm (mặt trước)
Hỗ trợ VGA: 365mm | CPU Cooler: 158mm', 899000, 100, 100, 2, 'case6.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values ('CASE COOLER MASTER MASTERBOX', N'Loại case: Mid Tower
Hỗ trợ mainboard: ATX; Micro-ATX; Mini-ITX
Khay mở rộng tối đa: 2 x 3.5" , 4 x 2.5"
Cổng USB: 2 x USB 3.0
Số quạt tặng kèm: 3 x 120mm ARGB', 2899000, 100, 100, 2, 'case7.png')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values ('CASE EK-CLASSIC 303EK', N'H? tr? mainboard: E-ATX, ATX, Micro ATX, Mini ITX
Khay l?p ? bên ngoài: 3 x 3.5" or 2.5"
Khay l?p ? phía sau: 8 x 2.5" or 4 x 3.5”
Chi?u dài t?i da GPU: 410mm ( khi không l?p qu?t)
Chi?u cao t?i da t?n nhi?t CPU: 180mm', 10899000, 100, 100, 2, 'case9.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values ('CASE THERMALTAKE VIEW 37', N'EK-Classic InWin 303EK trang bị một tấm nước tích hợp kèm sẵn bơm DDC.
Nó thực sự đặc biệt vì với cách thiết kế khoét lỗ ở mặt trước, sản phẩm mang tính độc nhất và chắc chắn là người dùng sẽ không thể tìm thấy được sản phẩm nào khác trên thị trường.
Sản phẩm này là một phần của Dòng EK Classic được đặc trưng bởi thiết kế tối giản và sạch sẽ trong khi vẫn duy trì hiệu suất hàng đầu.', 3899000, 100, 100, 2, 'case10.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'BÀN PHÍM CƠ FILCO', N'Thương hiệu bàn phím cơ nổi tiếng Nhật Bản
Phiên bản cải tiến từ Camourflage với màu sắc khác biệt
Switch 100% Cherry cao cấp
Không đèn Led, nhưng cảm giác gõ phím là tốt nhất trên thị trường
Chất liệu nhựa lớp vỏ rất cao cấp cho độ cứng cáp tuyệt đối
Thời gian bảo hành lên tới 60 tháng', 3899000, 100, 100, 3, 'banphim1.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'BÀN PHÍM CƠ E-DRA', N'Bàn phím cơ quang Edra EK3104 Optical
Thiết kế layout Fullsize 104 phím
Keycap ABS Doubleshot bền bỉ
Led Rainbow
Switch LK (Quang cơ) cho cảm giác gõ tốt và độ bền cực cao', 1899000, 100, 100, 3, 'banphim2.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'BÀN PHÍM CƠ CORSAIR', N'Bàn phím cơ cao cấp đến từ Corsair
Thiết kế mỏng độc đáo và bắt mắt
Hệ thống Led RGB 16,8 triệu màu
Loại switch Cherry công nghệ mới Low Profile
Tín hiệu được đáp ứng nhanh và chính xác hơn
Tuổi thọ được kéo dài so với thế hệ trước', 4299000, 100, 100, 3, 'banphim3.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'BÀN PHÍM CƠ DAREU', N'Bàn phím cơ cao cấp đến từ Corsair
Thiết kế mỏng độc đáo và bắt mắt
Hệ thống Led RGB 16,8 triệu màu
Loại switch Cherry công nghệ mới Low Profile
Tín hiệu được đáp ứng nhanh và chính xác hơn
Tuổi thọ được kéo dài so với thế hệ trước', 899000, 100, 100, 3, 'banphim4.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'BÀN PHÍM CƠ VARMILO', N'Bàn phím cơ cao cấp đến từ Corsair
Thiết kế mỏng độc đáo và bắt mắt
Hệ thống Led RGB 16,8 triệu màu
Loại switch Cherry công nghệ mới Low Profile
Tín hiệu được đáp ứng nhanh và chính xác hơn
Tuổi thọ được kéo dài so với thế hệ trước', 4999000, 100, 100, 3, 'banphim5.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CHUỘT RAZER BASILIS V2', N'11 nút bấm có thể lập trình với Synapse
LED RGB Chroma, lưu 5 profile với memory onboard
Tùy chỉnh lực cản / độ trơn của nút cuộn chuột
Switch quang học Razer Optical cho 2 nút bấm chính
Mắt đọc Razer Focus+ 20000DPI/650IPS/50GAcc
Kết nối dây liền dài 2,1m
Chất liệu Speedflex bọc dù mềm mại', 2199000, 100, 100, 3, 'chuot1.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CHUỘT E-DRA EM616', N'11 nút bấm có thể lập trình với Synapse
LED RGB Chroma, lưu 5 profile với memory onboard
Tùy chỉnh lực cản / độ trơn của nút cuộn chuột
Switch quang học Razer Optical cho 2 nút bấm chính
Mắt đọc Razer Focus+ 20000DPI/650IPS/50GAcc
Kết nối dây liền dài 2,1m
Chất liệu Speedflex bọc dù mềm mại', 1199000, 100, 100, 3, 'chuot3.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'CHUỘT AKKO HAMSTER', N'Chuột AKKO Hamster Wireless Aoki
Kết nối Wireless 2.4Ghz
Khoảng cách sử dụng lên đến 10m
Độ phân giải : 1200 DPI
Thiết kế hình chú chuột ngộ nghĩnh
Thời lượng pin lên đến 6 tháng', 199000, 100, 100, 3, 'chuot5.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'TAI NGHE CORSAIR HS60', N'Tai nghe Gaming Corsair HS60 Surround 7.1 Carbon
Phiên bản cao cấp hơn của HS50 với khả năng tái tạo âm thanh vòm 7.1
Tần số đáp ứng 20 - 20000Hz
Trở kháng 50Ohm
Hỗ trợ âm thanh vòm 7.1', 2799000, 100, 100, 3, 'tainghe1.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'TAI NGHE RAZER KRAKEN KITTY', N'Tai nghe Razer Kraken Kitty Chroma
Thiết kế tai mèo độc đáo
Led RGB Chroma 16.8 triệu màu, bao gồm cả phần tai mèo
Có thể cắm vào nguồn điện sạc dự phòng để hiện led mà không cần cắm vào PC
Micro với tính năng lọc ồn
Công nghệ THX Spatial Audio', 4299000, 100, 100, 3, 'tainghe2.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'TAI NGHE ASUS ROG STRIX', N'Tai nghe ASUS ROG Strix Fusion 300 Pink
Đầu cắm: USB 3.5 mm(1/8”) đầu cắm kết hợp Âm thanh/mic
Đường kính bộ truyền âm: 50 mm
Chất liệu bộ truyền âm: Nam châm neođim
Trở kháng: 32 Ohm
Tần suất Hồi đáp (tai nghe)20 ~ 20000 Hz
Tăng âm microphone: Một hướng 50 ~ 10000 Hz
Độ nhạy cảm: -39 dB ± 3 dB
Cáp bện (Cáp USB: 2M; Cáp 3.5mm:1.5M)', 2699000, 100, 100, 3, 'tainghe3.jpg')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'TAI NGHE RAZER KRAKEN KITTY PINK', N'Tai nghe Razer Kraken Kitty Chroma
Thiết kế tai mèo độc đáo
Led RGB Chroma 16.8 triệu màu, bao gồm cả phần tai mèo
Có thể cắm vào nguồn điện sạc dự phòng để hiện led mà không cần cắm vào PC
Micro với tính năng lọc ồn
Công nghệ THX Spatial Audio
Đệm tai nghe làm mát bằng Gel', 4249000, 50, 50, 3, 'tainghe5.png')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'TAI NGHE KINGSTON HYPERX CLOUD', N'Tai nghe Kingston HyperX Cloud Stinger S
Giả lập âm thanh vòm 7.1 qua HyperX NGENUITY
Âm thanh game đa chiều
Cảm giác thoải mái đặc trưng của HyperX
Nhẹ với chụp tai xoay 90°
Thanh trượt bằng thép dễ điều chỉnh, có độ bền cao
Mic khử ồn có thể xoay để tắt tiếng', 1499000, 50, 50, 3, 'tainghe4.jpg')

insert into KHACHHANG(TenKH, Email, Passowrd) values (N'Nguyễn Lý Phi', 'nguyenlyphi@gmail.com', '123')
insert into KHACHHANG(TenKH, Email, Passowrd) values (N'Hoàng Phi Long', 'hoangphilong@gmail.com', '456')
insert into KHACHHANG(TenKH, Email, Passowrd) values (N'Phạm Van Sỹ', 'phamvansy@gmail.com', '789')
insert into KHACHHANG(TenKH, Email, Passowrd) values (N'Ung Hoài Phú', 'unghoaiphu@gmail.com', '987')
insert into KHACHHANG(TenKH, Email, Passowrd) values (N'Lý Thị Hoài', 'lythihoai@gmail.com', '654')

SET IDENTITY_INSERT [dbo].[DONDATHANG] ON
INSERT [dbo].[DONDATHANG] ([MaDonHang], [MaKH], [TinhTrangGiaoHang], [Ngaydat]) VALUES (1, 1, 4, '05/10/2021')
INSERT [dbo].[DONDATHANG] ([MaDonHang], [MaKH], [TinhTrangGiaoHang], [Ngaydat]) VALUES (2, 1, 2, '05/07/2021')
SET IDENTITY_INSERT [dbo].[DONDATHANG] OFF

INSERT [dbo].[CTDH] ([MaDonHang], [MaSP], [Soluong], [DonGia]) VALUES (1, 1, 1, 5189900)
INSERT [dbo].[CTDH] ([MaDonHang], [MaSP], [Soluong], [DonGia]) VALUES (1, 2, 2, 8600000)
INSERT [dbo].[CTDH] ([MaDonHang], [MaSP], [Soluong], [DonGia]) VALUES (2, 1, 1, 5189900)
INSERT [dbo].[CTDH] ([MaDonHang], [MaSP], [Soluong], [DonGia]) VALUES (2, 2, 1, 10019990)

insert into KHUYENMAI(MaSp, MaLoai, TenSp, Giaban, Anhbia) values (25, 3, N'CHUỘT AKKO HAMSTER', 199000.00, 'chuot5.jpg')
insert into KHUYENMAI(MaSp, MaLoai, TenSp, Giaban, Anhbia) values (23, 3, N'BÀN PHÍM CƠ DAREU', 899000.00, 'banphim4.jpg')
insert into KHUYENMAI(MaSp, MaLoai, TenSp, Giaban, Anhbia) values (29, 3, N'TAI NGHE RAZER KRAKEN KITTY', 4299000.00, 'tainghe2.jpg')
insert into KHUYENMAI(MaSp, MaLoai, TenSp, Giaban, Anhbia) values (2, 1, N'CPU ADM Ryzen 5 3400G', 4299000.00, 'cpu2.jpg')
insert into KHUYENMAI(MaSp, MaLoai, TenSp, Giaban, Anhbia) values (24, 3, N'BÀN PHÍM CƠ VARMILO', 4999000.00, 'banphim5.jpg')

create table ADMIN
(
	MaAdmin int primary key identity(1,1),
	TenAdmin nvarchar(50) not null,
	Email varchar(50) not null,
	Passowrd varchar(50) not null 
)

insert into ADMIN(TenAdmin, Email, Passowrd) values ('admin', 'banahub@gmail.com', 'admin')

create table THONGTINKH
(
	MaKH int,
	DiaChi nvarchar(50),
	Tinh Nvarchar(50),
	SDT varchar(20)
	constraint PK_ttkh primary key(MaKH)
	constraint fk_ttkh_kh foreign key(MaKH) references KHACHHANG(MaKH)
)

insert into THONGTINKH(MaKH, DiaChi, Tinh, SDT) values(1, N'Số 06, Trần Văn Ơn, Phú Hòa, TP.Thủ Dầu Một', N'Bình Dương', '0988922328')

insert into LOAI(TenLoai) values ('Laptop')

insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'LAPTOP ACER GAMING ASPIRE 7 A715-41G-R150', N'CPU: AMD Ryzen 7 3750H
RAM: 8GB
Ổ cứng: 512GB SSD
VGA: NVIDIA GTX1650Ti 4G DDR6
Màn hình: 15.6 inch FHD IPS
HĐH: Win 10
Màu: Đen', 19989000, 50, 50, 4, 'laptop1.png')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'APPLE MACBOOK PRO 13 TOUCHBAR (Z11F)', N'CPU: Apple M1
RAM: 16GB
Ổ cứng: 512GB SSD
VGA: Onboard
Màn hình: 13.3 inch Retina IPS
HĐH: Mac OS
Màu: Bạc', 43989000, 50, 50, 4, 'laptop2.png')
insert into SANPHAM(TenSP, MoTa, GiaBan, SoLuong, SoLuongBan, MaLoai, AnhBia) values (N'LAPTOP MSI GAMING GL76 LEOPARD', N'CPU: Intel Core i7 11800H
RAM: 16GB
Ổ cứng: 1TB SSD
VGA: NVIDIA RTX3060 6G
Màn hình: 17.3 inch FHD 144Hz
Bàn phím: có đèn led
HĐH: Win 10
Màu: Đen', 43989000, 50, 50, 4, 'laptop3.png')