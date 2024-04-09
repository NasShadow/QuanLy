using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class TaiKhoanBLL
    {
        TaiKhoanAccess tkAccess = new TaiKhoanAccess();

        //Lấy gtri trong Table
        static Db db = new Db();

        //static string strConnectionInfo = db.strConnection;
        static DataContext dc = new DataContext(db.strConnection);

        //Table
        static Table<LoaiHang> LoaiHang = dc.GetTable<LoaiHang>();
        static Table<SanPham> SanPham = dc.GetTable<SanPham>();
        static Table<KhachHang> KhachHang = dc.GetTable<KhachHang>();
        static Table<NhanVien> NhanVien = dc.GetTable<NhanVien>();
        static Table<NhanVienGiaoHang> NhanVienGiaoHang = dc.GetTable<NhanVienGiaoHang>();

        //////////////////////////Voucher//////////////////////////////////////
        public string CheckUpdateVoucher(Voucher voucher)
        {

            //Kiểm tra nghiệp vụ
            if (voucher.Ma_Voucher == "" || voucher.Gia_Tri == "")
            {
                return "requeid_khongnhap";
            }
            else if (int.Parse(voucher.Gia_Tri) > 50)
            {
                return "requeid_saigiatri";
            }
            else if (int.Parse(voucher.Gia_Tri) == 0)
            {
                return "requeid_saigiatri_2";
            }
            string info = tkAccess.ChecktUpdateVoucher(voucher);
            return info;
        }

        //////////////////////////////// LOẠI HÀNG ////////////////////////////////

        //Kiểm tra thêm loại hàng
        public string CheckInsert(LoaiHang loaiHang)
        {
            var check_loaihang = from tenloaihang_1 in LoaiHang
                                 select tenloaihang_1.Ten_loai_hang;
            //Kiểm tra nghiệp vụ
            if (loaiHang.Ten_loai_hang == "" || loaiHang.Mo_ta == "")
            {
                return "requeid_khongnhap";
            }
            else if (Regex.IsMatch(loaiHang.Ten_loai_hang, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_tensai_1";
            }
            else if (Regex.IsMatch(loaiHang.Ten_loai_hang, @"\d"))
            {
                return "requeid_tensai_2";
            }

            foreach(var s in check_loaihang)
            {
                if (loaiHang.Ten_loai_hang == s)
                {
                    return "requeid_tentrung";
                }
            }

            string info = tkAccess.CheckInsertLoaiHang(loaiHang);
            return info;
        }

        //Kiểm tra sửa loại hàng
        public string CheckUpdate(LoaiHang loaiHang)
        {
            var check_loaihang = from tenloaihang_1 in LoaiHang
                                 select tenloaihang_1.Ten_loai_hang;
            //Kiểm tra nghiệp vụ
            if (loaiHang.Ten_loai_hang == "" || loaiHang.Mo_ta == "")
            {
                return "requeid_khongnhap";
            }
            else if (Regex.IsMatch(loaiHang.Ten_loai_hang, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_tensai_1";
            }
            else if (Regex.IsMatch(loaiHang.Ten_loai_hang, @"\d"))
            {
                return "requeid_tensai_2";
            }

            foreach (var s in check_loaihang)
            {
                if (loaiHang.Ten_loai_hang == s)
                {
                    return "requeid_tentrung";
                }
            }
            string info = tkAccess.ChecktUpdateLoaiHang(loaiHang);
            return info;
        }

        //Kiểm tra xóa loại hàng
        public string CheckDelete(LoaiHang loaiHang)
        {
            var spham = from sanpham in SanPham
                        select sanpham.Ma_loai_hang;


            //Kiểm tra nghiệp vụ
            if (loaiHang.Ten_loai_hang == "" || loaiHang.Mo_ta == "")
            {
                return "requeid_khongnhap";
            }

            foreach (var item in spham)
            {
                if (loaiHang.Ma_loai_hang == item)
                {
                    return "requeid_KHOANGOAI";
                }
            }

            string info = tkAccess.CheckDeleteLoaiHang(loaiHang);
            return info;
        }

        //////////////////////////////// SẢN PHẨM ////////////////////////////////

        //Kiểm tra thêm sản phẩm
        public string CheckInsert_SanPham(SanPham sanpham)
        {
            var check_sanpham = from sanpham_1 in SanPham
                                 select sanpham_1.Ten_SP;
            //Kiểm tra nghiệp vụ
            if (sanpham.Ten_SP == "" || sanpham.Ma_loai_hang == "" || sanpham.Gia_ban == "" || sanpham.Gia_nhap == "" || sanpham.So_luong == "" || sanpham.FileNames == "")
            {
                return "requeid_khongnhap";
            }
            else if (Regex.IsMatch(sanpham.Ten_SP, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_tensai_1";
            }
            else if (!Regex.IsMatch(sanpham.Gia_nhap, @"^\d+$") || Regex.IsMatch(sanpham.Gia_nhap, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_gianhap";
            }
            else if (!Regex.IsMatch(sanpham.Gia_ban, @"^\d+$") || Regex.IsMatch(sanpham.Gia_nhap, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_giaban";
            }
            else if (!Regex.IsMatch(sanpham.So_luong, @"^\d+$") || Regex.IsMatch(sanpham.Gia_nhap, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_soluong";
            }
            else if (int.Parse(sanpham.Gia_ban) < int.Parse(sanpham.Gia_nhap))
            {
                return "requeid_gialai";
            }

            foreach (var s in check_sanpham)
            {
                if (sanpham.Ma_SP == s)
                {
                    return "requeid_tensanphamtontai";
                }
            }

            string info = tkAccess.CheckInsertSanPham123(sanpham);
            return info;
        }


        //Kiểm tra sửa sản phẩm
        public string CheckUpdate_SanPham(SanPham sanpham)
        {
            var check_sanpham = from sanpham_1 in SanPham
                                select sanpham_1.Ten_SP;
            //Kiểm tra nghiệp vụ
            if (sanpham.Ten_SP == "" || sanpham.Ma_loai_hang == "" || sanpham.Gia_ban == "" || sanpham.Gia_nhap == "" || sanpham.So_luong == "" || sanpham.FileNames == "")
            {
                return "requeid_khongnhap";
            }
            else if (Regex.IsMatch(sanpham.Ten_SP, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_tensai_1";
            }
            else if (!Regex.IsMatch(sanpham.Gia_nhap, @"^\d+$") || Regex.IsMatch(sanpham.Gia_nhap, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_gianhap";
            }
            else if (!Regex.IsMatch(sanpham.Gia_ban, @"^\d+$") || Regex.IsMatch(sanpham.Gia_ban, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_giaban";
            }
            else if (!Regex.IsMatch(sanpham.So_luong, @"^\d+$") || Regex.IsMatch(sanpham.So_luong, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_soluong";
            }
            else if (int.Parse(sanpham.Gia_ban) < int.Parse(sanpham.Gia_nhap))
            {
                return "requeid_gialai";
            }

            foreach (var s in check_sanpham)
            {
                if (sanpham.Ma_SP == s)
                {
                    return "requeid_tensanphamtontai";
                }
            }

            string info = tkAccess.CheckUpdateSanPham123(sanpham);
            return info;
        }

        //Kiểm tra xóa sản phẩm
        public string CheckDelete_SanPham(SanPham sanpham)
        {
            string info = tkAccess.CheckDeleteSanPham123(sanpham);
            return info;
        }


        //////////////////////////////// KHÁCH HÀNG ////////////////////////////////

        //Kiểm tra sửa khách hàng
        public string CheckUpdate_KhachHang(KhachHang khachhang)
        {
            var result1 = from email in KhachHang
                         select email.Email;
            var result2 = from email in NhanVien
                          select email.Email;
            var result3 = from email in NhanVienGiaoHang
                          select email.Email;

            if (khachhang.Ten_Khach_Hang == "" || khachhang.Ten_TK == "" || khachhang.Email == "" || khachhang.MatKhau == "" || khachhang.XacNhanMatKhau == "" || khachhang.Gioi_Tinh == "" || khachhang.DienThoai == "" || khachhang.TrangThai == "")
            {
                return "requeid_botrong";
            }
            /*else if (Regex.IsMatch(khachhang.Ten_Khach_Hang, @"\d") || Regex.IsMatch(khachhang.Ten_Khach_Hang, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_tenkhachhang";
            }*/
            else if (Regex.IsMatch(khachhang.Ten_TK, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_tentaikhoan";
            }
            else if (!Regex.IsMatch(khachhang.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email";
            }
            else if (!Regex.IsMatch(khachhang.MatKhau, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_matkhau_1";
            }
            else if (khachhang.MatKhau.Length < 5)
            {
                return "requeid_matkhau_2";
            }
            else if (khachhang.XacNhanMatKhau != khachhang.MatKhau)
            {
                return "requeid_xacnhanmatkhau";
            }
            else if (!Regex.IsMatch(khachhang.DienThoai, @"^(?:\+84|0)(\d{9,10})$"))
            {
                return "requeid_dienthoai";
            }


            //Kiểm tra trùng email tk
            foreach (var item in result1)
            {
                if (khachhang.Email == item)
                {
                    return "requeid_trungemail";
                }
            }
            foreach (var item in result2)
            {
                if (khachhang.Email == item)
                {
                    return "requeid_trungemail2";
                }
            }
            foreach (var item in result3)
            {
                if (khachhang.Email == item)
                {
                    return "requeid_trungemail3";
                }
            }

            string info = tkAccess.CheckUpdateKhachHang123(khachhang);
            return info;
        }


        public string CheckUpdate_KhachHang1(KhachHang khachhang)
        {

            if (khachhang.Ten_Khach_Hang == "" || khachhang.Ten_TK == "" || khachhang.Email == "" || khachhang.MatKhau == "" || khachhang.XacNhanMatKhau == "" || khachhang.Gioi_Tinh == "" || khachhang.DienThoai == "" || khachhang.TrangThai == "")
            {
                return "requeid_botrong";
            }
            /*else if (Regex.IsMatch(khachhang.Ten_Khach_Hang, @"\d") || Regex.IsMatch(khachhang.Ten_Khach_Hang, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_tenkhachhang";
            }*/
            else if (Regex.IsMatch(khachhang.Ten_TK, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_tentaikhoan";
            }
            else if (!Regex.IsMatch(khachhang.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email";
            }
            else if (!Regex.IsMatch(khachhang.MatKhau, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_matkhau_1";
            }
            else if (khachhang.MatKhau.Length < 5)
            {
                return "requeid_matkhau_2";
            }
            else if (khachhang.XacNhanMatKhau != khachhang.MatKhau)
            {
                return "requeid_xacnhanmatkhau";
            }
            else if (!Regex.IsMatch(khachhang.DienThoai, @"^(?:\+84|0)(\d{9,10})$"))
            {
                return "requeid_dienthoai";
            }


            //Kiểm tra trùng email tk

            string info = tkAccess.CheckUpdateKhachHang123(khachhang);
            return info;
        }



        //Kiểm tra Vô hiệu hóa khách hàng
        public string CheckVHH_KhachHang(KhachHang khachhang)
        {
            if (khachhang.Ten_Khach_Hang == "" || khachhang.Ten_TK == "" || khachhang.Email == "" || khachhang.MatKhau == "" || khachhang.XacNhanMatKhau == "" || khachhang.Gioi_Tinh == "" || khachhang.DienThoai == "" || khachhang.TrangThai == "")
            {
                return "requeid_botrong";
            }


            string info = tkAccess.CheckVHHKhachHang123(khachhang);
            return info;
        }

        //////////////////////////////// ĐƠN HÀNG ////////////////////////////////

        //Kiểm tra hoàn tất đơn hàng
        public string CheckHoanTat_DonHang(DonHang donhang)
        {
            if (donhang.Ma_Don_Hang == "" || donhang.Thanh_Tien == "" || donhang.Ngay_Xuat_Don == "" || donhang.Trang_Thai_Don == "" || donhang.Ma_Khach_Hang == "" || donhang.Ma_Nhan_Vien == "")
            {
                return "requeid_botrong";
            }


            string info = tkAccess.CheckHoanTatDonHang123(donhang);
            return info;
        }

        //////////////////////////////// ĐĂNG NHẬP ADMIN ////////////////////////////////
        //Kiểm tra khi đăng nhập
        public string Check_LoginAdmin(NhanVien nhanvien)
        {
            if (nhanvien.Email == "" || nhanvien.MatKhau == "")
            {
                return "requeid_botrong";
            }

            string info = tkAccess.CheckLoginAdmin123(nhanvien);
            return info;
        }

        //////////////////////////////// NHÂN VIÊN ////////////////////////////////

        public string CheckInsert(NhanVien nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Ten_NV == "")
            {
                return "requeid_tennhanvien";
            }
            else if (Regex.IsMatch(nhanVien.Ten_NV, @"\d") || Regex.IsMatch(nhanVien.Ten_NV, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "wrong_name";
            }
            else if (nhanVien.Email == "")
            {
                return "requeid_email";
            }
            else if (!Regex.IsMatch(nhanVien.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email1";
            }
            else if (nhanVien.MatKhau == "")
            {
                return "requeid_matkhau";
            }
            else if (!Regex.IsMatch(nhanVien.MatKhau, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_matkhau1";
            }
            else if (nhanVien.XacNhanMatKhau == "")
            {
                return "requeid_xacnhanmk";
            }
            else if (nhanVien.VaiTro == "")
            {
                return "requeid_vaitro";
            }
            else if (nhanVien.DiaChi == "")
            {
                return "requeid_diachi";
            }
            else if (nhanVien.MatKhau != nhanVien.XacNhanMatKhau)
            {
                return "match_pass";
            }
            else if (nhanVien.Gioi_Tinh == "2")
            {
                return "select_sex";
            }

            var result = from email in NhanVien
                         select email.Email;

            foreach (var iteam in result)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email";
                }
            }

            var result1 = from email in NhanVienGiaoHang
                          select email.Email;

            foreach (var iteam in result1)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email1";
                }
            }
            var result2 = from email in KhachHang
                          select email.Email;

            foreach (var iteam in result2)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email2";
                }
            }

            string info = tkAccess.CheckInsertNhanVien(nhanVien);
            return info;
        }
        public string CheckUpdate(NhanVien nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Ma_Nhan_Vien == "")
            {
                return "requeid_manhanvien";
            }
            else if (nhanVien.Ten_NV == "")
            {
                return "requeid_tennhanvien";
            }
            else if (Regex.IsMatch(nhanVien.Ten_NV, @"\d") || Regex.IsMatch(nhanVien.Ten_NV, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "wrong_name";
            }
            else if (nhanVien.Email == "")
            {
                return "requeid_email";
            }
            else if (!Regex.IsMatch(nhanVien.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email1";
            }
            else if (nhanVien.MatKhau == "")
            {
                return "requeid_matkhau";
            }
            else if (!Regex.IsMatch(nhanVien.MatKhau, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_matkhau1";
            }
            else if (nhanVien.XacNhanMatKhau == "")
            {
                return "requeid_xacnhanmk";
            }
            else if (nhanVien.VaiTro == "")
            {
                return "requeid_vaitro";
            }
            else if (nhanVien.DiaChi == "")
            {
                return "requeid_diachi";
            }
            else if (nhanVien.MatKhau != nhanVien.XacNhanMatKhau)
            {
                return "match_pass";
            }


            var result = from email in NhanVienGiaoHang
                         select email.Email;

            foreach (var iteam in result)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email";
                }
            }
/*            var result2 = from email in NhanVien
                         select email.Email;

            foreach (var iteam in result2)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email2";
                }
            }
            var result3 = from email in KhachHang
                         select email.Email;

            foreach (var iteam in result3)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email3";
                }
            }*/
            string info = tkAccess.CheckUpdateNhanVien(nhanVien);
            return info;
        }

        public string CheckUpdate1(NhanVien nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Ma_Nhan_Vien == "")
            {
                return "requeid_manhanvien";
            }
            else if (nhanVien.Ten_NV == "")
            {
                return "requeid_tennhanvien";
            }
            else if (Regex.IsMatch(nhanVien.Ten_NV, @"\d") || Regex.IsMatch(nhanVien.Ten_NV, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "wrong_name";
            }
            else if (nhanVien.Email == "")
            {
                return "requeid_email";
            }
            else if (!Regex.IsMatch(nhanVien.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email1";
            }
            else if (nhanVien.MatKhau == "")
            {
                return "requeid_matkhau";
            }
            else if (!Regex.IsMatch(nhanVien.MatKhau, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_matkhau1";
            }
            else if (nhanVien.XacNhanMatKhau == "")
            {
                return "requeid_xacnhanmk";
            }
            else if (nhanVien.VaiTro == "")
            {
                return "requeid_vaitro";
            }
            else if (nhanVien.DiaChi == "")
            {
                return "requeid_diachi";
            }
            else if (nhanVien.MatKhau != nhanVien.XacNhanMatKhau)
            {
                return "match_pass";
            }
            string info = tkAccess.CheckUpdateNhanVien(nhanVien);
            return info;
        }

        public string CheckDisable(NhanVien nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Ma_Nhan_Vien == "")
            {
                return "requeid_manhanvien";
            }
            string info = tkAccess.CheckDisableNhanVien(nhanVien);
            return info;
        }



        //////////////////////////////// NHÂN VIÊN PROFILE ////////////////////////////////


        public string UpdateProFileAdmin(NhanVien nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Ma_Nhan_Vien == "" || nhanVien.Ten_NV =="" || nhanVien.Email =="" || nhanVien.Gioi_Tinh == "" || nhanVien.DiaChi == "")
            {
                return "requeid_botrong";
            }
            else if (Regex.IsMatch(nhanVien.Ten_NV, @"\d") || Regex.IsMatch(nhanVien.Ten_NV, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "wrong_name";
            }
            else if (!Regex.IsMatch(nhanVien.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email";
            }

            string info = tkAccess.UpdateProfile123(nhanVien);
            return info;
        }

        public string Change_PassFGAdmin(NhanVien nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.MatKhau == "" || nhanVien.XacNhanMatKhau == "" || nhanVien.Ma_Nhan_Vien == "")
            {
                return "requeid_botrong";
            }
            else if (nhanVien.MatKhau != nhanVien.XacNhanMatKhau)
            {
                return "khongbangnhau";
            }
            else if (!Regex.IsMatch(nhanVien.MatKhau, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_pass";
            }
            else if (nhanVien.MatKhau.Length < 6)
            {
                return "Short";
            }


            string info = tkAccess.ChangePassAdmin123(nhanVien);
            return info;
        }

        public string Find_PassAdmin(NhanVien nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Email == "")
            {
                return "requeid_botrong";
            }
            else if (!Regex.IsMatch(nhanVien.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email";
            }


            string info = tkAccess.FindPassAdmin123(nhanVien);
            return info;
        }


        public string Check_InsertNVGH(NhanVienGiaoHang nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Ten_NV_GH == "")
            {
                return "requeid_tennhanvien";
            }
            else if (Regex.IsMatch(nhanVien.Ten_NV_GH, @"\d") || Regex.IsMatch(nhanVien.Ten_NV_GH, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "wrong_name";
            }
            else if (nhanVien.Email == "")
            {
                return "requeid_email";
            }
            else if (!Regex.IsMatch(nhanVien.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email1";
            }
            else if (nhanVien.MatKhau == "")
            {
                return "requeid_matkhau";
            }
            else if (!Regex.IsMatch(nhanVien.MatKhau, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_matkhau1";
            }
            else if (nhanVien.XacNhanMatKhau == "")
            {
                return "requeid_xacnhanmk";
            }
            else if (nhanVien.VaiTro == "")
            {
                return "requeid_vaitro";
            }
            else if (nhanVien.DiaChi == "")
            {
                return "requeid_diachi";
            }
            else if (nhanVien.MatKhau != nhanVien.XacNhanMatKhau)
            {
                return "match_pass";
            }
            else if (nhanVien.Gioi_Tinh == "2")
            {
                return "select_sex";
            }

            var result = from email in NhanVienGiaoHang
                         select email.Email;

            foreach (var iteam in result)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email";
                }
            }
            var result1 = from email in NhanVien
                         select email.Email;

            foreach (var iteam in result1)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email1";
                }
            }
            var result2 = from email in KhachHang
                         select email.Email;

            foreach (var iteam in result2)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email2";
                }
            }
            string info = tkAccess.CheckInsertNhanVienGH(nhanVien);
            return info;
        }


        public string CheckUpdateShipper(NhanVienGiaoHang nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Ma_Nhan_VienGH == "")
            {
                return "requeid_manhanvien";
            }
            else if (nhanVien.Ten_NV_GH == "")
            {
                return "requeid_tennhanvien";
            }
            else if (Regex.IsMatch(nhanVien.Ten_NV_GH, @"\d") || Regex.IsMatch(nhanVien.Ten_NV_GH, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "wrong_name";
            }
            else if (nhanVien.Email == "")
            {
                return "requeid_email";
            }
            else if (!Regex.IsMatch(nhanVien.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email1";
            }
            else if (nhanVien.MatKhau == "")
            {
                return "requeid_matkhau";
            }
            else if (!Regex.IsMatch(nhanVien.MatKhau, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_matkhau1";
            }
            else if (nhanVien.XacNhanMatKhau == "")
            {
                return "requeid_xacnhanmk";
            }
            else if (nhanVien.DiaChi == "")
            {
                return "requeid_diachi";
            }
            else if (nhanVien.MatKhau != nhanVien.XacNhanMatKhau)
            {
                return "match_pass";
            }

            var result = from email in NhanVienGiaoHang
                         select email.Email;

            foreach (var iteam in result)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email";
                }
            }
            var result2 = from email in NhanVien
                         select email.Email;

            foreach (var iteam in result2)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email2";
                }
            }
            var result3 = from email in KhachHang
                         select email.Email;

            foreach (var iteam in result3)
            {
                if (nhanVien.Email == iteam)
                {
                    return "trung_email3";
                }
            }
            string info = tkAccess.CheckUpdateShipper(nhanVien);
            return info;
        }

        public string CheckUpdateShipper1(NhanVienGiaoHang nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Ma_Nhan_VienGH == "")
            {
                return "requeid_manhanvien";
            }
            else if (nhanVien.Ten_NV_GH == "")
            {
                return "requeid_tennhanvien";
            }
            else if (Regex.IsMatch(nhanVien.Ten_NV_GH, @"\d") || Regex.IsMatch(nhanVien.Ten_NV_GH, @"[,.!@#$%^&*()<>/|}{~:]"))
            {
                return "wrong_name";
            }
            else if (nhanVien.Email == "")
            {
                return "requeid_email";
            }
            else if (!Regex.IsMatch(nhanVien.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                return "requeid_email1";
            }
            else if (nhanVien.MatKhau == "")
            {
                return "requeid_matkhau";
            }
            else if (!Regex.IsMatch(nhanVien.MatKhau, @"[!@#$%^&*()<>/|}{~:]"))
            {
                return "requeid_matkhau1";
            }
            else if (nhanVien.XacNhanMatKhau == "")
            {
                return "requeid_xacnhanmk";
            }
            else if (nhanVien.DiaChi == "")
            {
                return "requeid_diachi";
            }
            else if (nhanVien.MatKhau != nhanVien.XacNhanMatKhau)
            {
                return "match_pass";
            }
            string info = tkAccess.CheckUpdateShipper(nhanVien);
            return info;
        }



        public string CheckDisableShipper(NhanVienGiaoHang nhanVien)
        {
            //Kiểm tra nghiệp vụ
            if (nhanVien.Ma_Nhan_VienGH == "")
            {
                return "requeid_manhanvien";
            }
            string info = tkAccess.CheckDisableShipper(nhanVien);
            return info;
        }


        //CONNECTION SHOW DATAGRIDVIEW
        public SqlConnection Load()
        {
            SqlConnection conn = TaiKhoanAccess.check();
            return conn;
        }


    }
}
