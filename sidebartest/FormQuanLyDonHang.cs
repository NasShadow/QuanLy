using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class FormQuanLyDonHang : Form
    {

        static DonHang donhang = new DonHang();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        SqlConnection conn = TKBLL.Load();

        //Lấy gtri trong Table
        static Db db = new Db();

        //static string strConnectionInfo = db.strConnection;
        static DataContext dc = new DataContext(db.strConnection);

        //Table
        static Table<DonHang> DonHang = dc.GetTable<DonHang>();

        public FormQuanLyDonHang()
        {
            InitializeComponent();
        }

        private void Load1()
        {
            //btn
            btn_hoantat.Enabled = false;

            //txt
            txt_makhachhang.Enabled = false;
            txt_madonhang.Enabled = false;
            txt_manhanvien.Enabled = false;
            txt_ngayxuatdon.Enabled = false;
            txt_thanhtien.Enabled = false;
            txt_trangthai.Enabled = false;

            //txt.text
            txt_makhachhang.Text = "";
            txt_madonhang.Text = "";
            txt_manhanvien.Text = "";
            txt_ngayxuatdon.Text = "";
            txt_thanhtien.Text = "";
            txt_trangthai.Text = "";
        }

        private void Load2()
        {
            //btn
            btn_hoantat.Enabled = true;
        }



        //Hàm kiểm tra vô hiệu hóa
        private void CheckVoHieuHoa()
        {
            if (txt_trangthai.Text == "Hoàn Tất")
            {
                btn_hoantat.Text = "Chưa Hoàn Tất";
            }
            else if (txt_trangthai.Text == "Chưa Hoàn Tất")
            {
                btn_hoantat.Text = "Hoàn Tất";
            }
        }




        //form load
        private void FormQuanLyDonHang_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            //Load dữ liệu vào lưới
            Load_DgvDonHang();

            //Load1
            Load1();
        }



        //Nút hoàn tất
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN HOÀN TẤT ĐƠN HÀNG NÀY???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            donhang.Ma_Don_Hang = txt_madonhang.Text;
            donhang.Thanh_Tien = txt_thanhtien.Text;
            donhang.Ngay_Xuat_Don = txt_ngayxuatdon.Text;
            donhang.Trang_Thai_Don = txt_trangthai.Text;
            donhang.Ma_Khach_Hang = txt_makhachhang.Text;
            donhang.Ma_Nhan_Vien = txt_manhanvien.Text;


            if (result == DialogResult.Yes)
            {

                if (btn_hoantat.Text == "Chưa Hoàn Tất")
                {
                    donhang.Trang_Thai_Don = "0";

                    string getuser = TKBLL.CheckHoanTat_DonHang(donhang);

                    switch (getuser)
                    {
                        case "requeid_botrong":
                            {
                                MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("CHƯA HOÀN TẤT ĐƠN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyDonHang_Load(null, null);
                                break;
                            }
                    }
                }
                else if (btn_hoantat.Text == "Hoàn Tất")
                {
                    donhang.Trang_Thai_Don = "1";

                    string getuser = TKBLL.CheckHoanTat_DonHang(donhang);

                    switch (getuser)
                    {
                        case "requeid_botrong":
                            {
                                MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("HOÀN TẤT ĐƠN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyDonHang_Load(null, null);
                                break;
                            }
                    }
                }





            }
            else if (result == DialogResult.No)
            {
                return;
            }


        }


        //Hàm load lên datagrid view
        DataTable dataTable = new DataTable("DonHang");
        private void Load_DgvDonHang()
        {
            dataTable.Clear();
            string query = "SELECT DonHang.Ma_Don_Hang N'Mã_Đơn_Hàng', Thanh_Tien N'Thành_Tiền', Ngay_Xuat_Don N'Ngày_Xuất_Đơn', IIF(Trang_Thai_Don = 1, N'Hoàn Tất', N'Chưa Hoàn Tất') AS N'Trạng_Thái_Đơn', Ma_Khach_Hang N'Mã_Khách_Hàng', Ma_Nhan_Vien N'Mã_Nhân_Viên', iif(Phuong_thuc_thanh_toan = 1, N'Thanh_Toan_Online', N'Thanh_Toan_Truc_Tiep')AS N'Hinh_Thuc_Thanh_Toan' FROM dbo.DonHang\r\n  JOIN dbo.PhuongThucThanhToan ON PhuongThucThanhToan.Ma_Don_Hang = DonHang.Ma_Don_Hang";
            SqlDataAdapter adapter = new SqlDataAdapter(query,conn);
            adapter.Fill(dataTable);
            dgv_donhang.DataSource = dataTable;
        }

        //Ấn và hiển thị lên lưới
        private void dgv_donhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgv_donhang.CurrentRow.Index;

            txt_madonhang.Text = dgv_donhang.Rows[i].Cells[0].Value.ToString();
            txt_thanhtien.Text = dgv_donhang.Rows[i].Cells[1].Value.ToString();
            txt_ngayxuatdon.Text = dgv_donhang.Rows[i].Cells[2].Value.ToString();
            txt_trangthai.Text = dgv_donhang.Rows[i].Cells[3].Value.ToString();
            txt_makhachhang.Text = dgv_donhang.Rows[i].Cells[4].Value.ToString();
            txt_manhanvien.Text = dgv_donhang.Rows[i].Cells[5].Value.ToString();

            //Kiểm tra btn click
            CheckVoHieuHoa();

            //Mở BTN
            Load2();
        }





        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }
        

        private void btn_sua_Click(object sender, EventArgs e)
        {

        }

        private void txt_trangthai_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_trangthai_Click(object sender, EventArgs e)
        {

        }

        private void txt_ngayxuatdon_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_ngayxuatdon_Click(object sender, EventArgs e)
        {

        }

        private void txt_thanhtien_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_thanhtien_Click(object sender, EventArgs e)
        {

        }

        private void txt_manhanvien_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_manhanvien_Click(object sender, EventArgs e)
        {

        }

        private void txt_makhachhang_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_makhachhang_Click(object sender, EventArgs e)
        {

        }

        private void txt_madonhang_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_madonhang_Click(object sender, EventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dataTable.DefaultView;
            // Access Text property of txt_search
            dv.RowFilter = string.Format("Mã_Đơn_Hàng LIKE '%{0}%'", txt_search.Text);
            dgv_donhang.DataSource = dv.ToTable();
        }
    }
}
