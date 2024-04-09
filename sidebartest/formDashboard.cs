using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class formDashboard : Form
    {
        public formDashboard()
        {
            InitializeComponent();
        }
        void DataChart()
        {
            int a = 0;
            SqlConnection conn = new SqlConnection("Data Source=MSI\\MSSQLSERVER2017;Initial Catalog=QuanLyBanHang;Integrated Security=True");
            DataTable dtNV = new DataTable();
            DataTable dtLH = new DataTable();
            DataTable dtKH = new DataTable();
            DataTable dtSP = new DataTable();
            DataTable dtDG = new DataTable();
            DataTable dtDT = new DataTable();
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("Select Count(*) as N'SL',IIF(TinhTrang=1,N'Đang hoạt động',N'Ngừng hoạt động') as 'IDNV' from NhanVien where VaiTro='Nhan Vien' group by TinhTrang", conn);
            SqlDataAdapter LoaiHang = new SqlDataAdapter("Select Ten_loai_hang,Count(ID) as 'IDLH' from LoaiHang group by Ten_loai_hang", conn);
            SqlDataAdapter KhachHang = new SqlDataAdapter("Select IIF(TrangThai=1, N'Hoạt_Động', N'Ngừng_Hoạt_Động') as 'TrangThai',Count(ID) as 'IDKH'from KhachHang group by TrangThai", conn);
            SqlDataAdapter SanPham = new SqlDataAdapter("Select Ten_SP,So_luong from SanPham", conn);
            SqlDataAdapter DonGiao = new SqlDataAdapter("Select IIF(Trang_Thai_Don=1,N'Đã hoàn thành',N'Chưa hoàn thành') as 'TrangThaiDon',Count(Ma_Don_Hang) as 'IDDG' from DonHang group by Trang_Thai_Don", conn);
            SqlDataAdapter DoanhThu = new SqlDataAdapter("Select DateName(MM,Ngay_Xuat_Don) as N'Tháng', SUM(Thanh_Tien) as 'Tổng' from DonHang where Trang_Thai_Don='1' group by DateName(MM,Ngay_Xuat_Don)", conn);
            SanPham.Fill(dtSP);
            adapter.Fill(dtNV);
            LoaiHang.Fill(dtLH);
            KhachHang.Fill(dtKH);
            DonGiao.Fill(dtDG);
            DoanhThu.Fill(dtDT);
            crtLoaiHang.DataSource = dtLH;
            crtNhanVien.DataSource = dtNV;
            crtKhachHang.DataSource = dtKH;
            crtSanPham.DataSource = dtSP;
            crtDonGiao.DataSource = dtDG;
            crtDoanhThu.DataSource = dtDT;
            SqlCommand CountNVData = new SqlCommand("Select Count(*) from NhanVien where VaiTro='Nhan Vien'", conn);
            var CountNVEXE = CountNVData.ExecuteScalar();
            lbl_SLNhanVien1.Text = CountNVEXE.ToString() + " Nhân viên";
            SqlCommand CountLHData = new SqlCommand("Select Count(*) from LoaiHang", conn);
            var CountLHEXE = CountLHData.ExecuteScalar();
            lbl_SLLoaiHang1.Text = CountLHEXE.ToString() + " Loại hàng";
            SqlCommand CountKHData = new SqlCommand("Select Count(*) from KhachHang", conn);
            var CountKHEXE = CountKHData.ExecuteScalar();
            lbl_SLKhachHang1.Text = CountKHEXE.ToString() + " Khách hàng";
            SqlCommand CountDGData = new SqlCommand("Select Count(*) from DonHang", conn);
            var CountDGEXE = CountDGData.ExecuteScalar();
            lbl_SLDonGiao1.Text = CountDGEXE.ToString() + " Đơn giao";
            SqlCommand CountSPData = new SqlCommand("Select Count(*) from SanPham", conn);
            var CountSPEXE = CountSPData.ExecuteScalar();
            lbl_SLSanPham1.Text = CountSPEXE.ToString() + " Sản phẩm";
            conn.Close();
            crtNhanVien.Series["ChartNhanVien"].XValueMember = "IDNV";
            crtNhanVien.Series["ChartNhanVien"].YValueMembers = "SL";
            crtLoaiHang.Series["ChartLoaiHang"].XValueMember = "Ten_loai_hang";
            crtLoaiHang.Series["ChartLoaiHang"].YValueMembers = "IDLH";
            crtKhachHang.Series["ChartKhachHang"].XValueMember = "TrangThai";
            crtKhachHang.Series["ChartKhachHang"].YValueMembers = "IDKH";
            crtSanPham.Series["ChartSanPham"].XValueMember = "Ten_SP";
            crtSanPham.Series["ChartSanPham"].YValueMembers = "So_luong";
            crtDonGiao.Series["ChartDonGiao"].XValueMember = "TrangThaiDon";
            crtDonGiao.Series["ChartDonGiao"].YValueMembers = "IDDG";
            crtDoanhThu.Series["Doanh Thu"].XValueMember = "Tháng";
            crtDoanhThu.Series["Doanh Thu"].YValueMembers = "Tổng";
        }
        private void formDashboard_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            DataChart();
        }

        private void crtNhanVien_Click(object sender, EventArgs e)
        {

        }
    }
}
