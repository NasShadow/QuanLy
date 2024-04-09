using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TaiKhoanAccess:DatabaseAccess
    {
        public static SqlConnection check()
        {
            SqlConnection conn = SqlConnectionData.Connect();
            return conn;
        }
        public string ChecktUpdateVoucher(Voucher voucher)
        {
            string info = UpdateVoucher(voucher);
            return info;
        }

        //////////////////////////////// LOẠI HÀNG ////////////////////////////////

        //Lấy giá trị thêm loại hàng
        public string CheckInsertLoaiHang(LoaiHang loaihang)
        {
            string info = InsertLoaiHang(loaihang);
            return info;
        }
        //Lấy giá trị sửa loại hàng
        public string ChecktUpdateLoaiHang(LoaiHang loaihang)
        {
            string info = UpdateLoaiHang(loaihang);
            return info;
        }
        //Lấy giá trị xóa loại hàng
        public string CheckDeleteLoaiHang(LoaiHang loaihang)
        {
            string info = DeleteLoaiHang(loaihang);
            return info;
        }

        //////////////////////////////// SẢN PHẨM ////////////////////////////////
        //Lấy giá trị thêm sản phẩm
        public string CheckInsertSanPham123(SanPham sanpham)
        {
            string info = InsertSanPham(sanpham);
            return info;
        }

        //Lấy giá trị sửa sản phẩm
        public string CheckUpdateSanPham123(SanPham sanpham)
        {
            string info = UpdateSanPham(sanpham);
            return info;
        }
        //Lấy giá trị xóa sản phẩm
        public string CheckDeleteSanPham123(SanPham sanpham)
        {
            string info = DeleteSanPham(sanpham);
            return info;
        }


        //////////////////////////////// KHÁCH HÀNG ////////////////////////////////

        //Lấy giá trị sửa khachhang sản phẩm
        public string CheckUpdateKhachHang123(KhachHang khachhang)
        {
            string info = UpdateKhachHang(khachhang);
            return info;
        }

        //Lấy giá trị vô hiệu hóa khachhang sản phẩm
        public string CheckVHHKhachHang123(KhachHang khachhang)
        {
            string info = VoHieuHoaKhachHang(khachhang);
            return info;
        }


        //////////////////////////////// ĐƠN HÀNG ////////////////////////////////

        //Lấy giá trị sửa donhang 
        public string CheckHoanTatDonHang123(DonHang donhang)
        {
            string info = HoanTatDonHang(donhang);
            return info;
        }

        //////////////////////////////// ĐĂNG NHẬP ADMIN ////////////////////////////////


        //Lấy giá trị sửa donhang 
        public string CheckLoginAdmin123(NhanVien nhanvien)
        {
            string info = CheckLoginAdmin(nhanvien);
            return info;
        }

        //////////////////////////////// NHÂN VIÊN ////////////////////////////////
        public string CheckInsertNhanVien(NhanVien nhanvien)
        {
            string infoNV = InsertNhanVien(nhanvien);
            return infoNV;
        }
        public string CheckUpdateNhanVien(NhanVien nhanvien)
        {
            string infoNV = UpdateNhanVien(nhanvien);
            return infoNV;
        }
        public string CheckDisableNhanVien(NhanVien nhanvien)
        {
            string infoNV = DisableNhanVien(nhanvien);
            return infoNV;
        }
        //////////////////////////////// NHÂN VIÊN PROFILE ////////////////////////////////
        public string UpdateProfile123(NhanVien nhanvien)
        {
            string infoNV = UpdateProfile(nhanvien);
            return infoNV;
        }

        public string ChangePassAdmin123(NhanVien nhanvien)
        {
            string infoNV = ChangePassAdmin(nhanvien);
            return infoNV;
        }

        public string FindPassAdmin123(NhanVien nhanvien)
        {
            string infoNV = FindPassAdmin(nhanvien);
            return infoNV;
        }


        public string CheckInsertNhanVienGH(NhanVienGiaoHang nhanviengiaohang)
        {
            string infoNV = InsertNhanVienGH(nhanviengiaohang);
            return infoNV;
        }


        public string CheckUpdateShipper(NhanVienGiaoHang nhanvien)
        {
            string infoNV = UpdateShipper(nhanvien);
            return infoNV;
        }

        public string CheckDisableShipper(NhanVienGiaoHang nhanvien)
        {
            string infoNV = DisableShipper(nhanvien);
            return infoNV;
        }

    }
}
