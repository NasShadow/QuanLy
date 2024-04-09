using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class formCRUDsanpham : Form
    {
        static SanPham sanpham = new SanPham();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        SqlConnection conn = TKBLL.Load();

        //Lấy gtri trong Table
        static Db db = new Db();

        //static string strConnectionInfo = db.strConnection;
        static DataContext dc = new DataContext(db.strConnection);

        //Table
        static Table<LoaiHang> LoaiHang = dc.GetTable<LoaiHang>();
        static Table<SanPham> SanPham = dc.GetTable<SanPham>();

        public formCRUDsanpham()
        {
            InitializeComponent();
        }

        //Load Giá trị mã loại, tên loại vào combobox
        private void LoadValue_Combobox()
        {
            cbo_maloaihang.Items.Clear();
            cbo_searchsanpham.Items.Clear();
            cbo_searchsanpham.Items.Add("Tất cả");

            var result = from maloai in LoaiHang
                         select maloai.Ma_loai_hang + " | " + maloai.Ten_loai_hang;

            foreach (var item in result)
            {
                cbo_maloaihang.Items.Add(item);
            }

            var result_1 = from maloai in LoaiHang
                         select maloai.Ten_loai_hang;

            foreach (var item in result_1)
            {
                cbo_searchsanpham.Items.Add(item);
            }
        }

        //Ấn vào sản phẩm trên lưới
        private void dgv_sanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu ô được nhấp không chứa dữ liệu
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && string.IsNullOrEmpty(dgv_sanpham.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString()))
            {
                MessageBox.Show("KHÔNG CÓ DỮ LIỆU TRONG Ô NÀY!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cbo_maloaihang.Items.Clear();

            var result = from maloai in LoaiHang
                         select maloai.Ma_loai_hang;

            foreach (var item in result)
            {
                cbo_maloaihang.Items.Add(item);
            }


            int i = dgv_sanpham.CurrentRow.Index;

            txt_masanpham.Text = dgv_sanpham.Rows[i].Cells[0].Value.ToString();
            cbo_maloaihang.Text = dgv_sanpham.Rows[i].Cells[1].Value.ToString();
            txt_tensanpham.Text = dgv_sanpham.Rows[i].Cells[2].Value.ToString();
            txt_tenloaihang.Text = dgv_sanpham.Rows[i].Cells[3].Value.ToString();
            txt_soluong.Text = dgv_sanpham.Rows[i].Cells[4].Value.ToString();
            txt_gianhap.Text = dgv_sanpham.Rows[i].Cells[5].Value.ToString();
            txt_giaban.Text = dgv_sanpham.Rows[i].Cells[6].Value.ToString();
            txt_filesname.Text = dgv_sanpham.Rows[i].Cells[7].Value.ToString();

            pic_sanpham.Image = new Bitmap($@"{txt_filesname.Text}");
            load_form3();

        }

        //Hiển thị thông tin
        DataTable dataTable = new DataTable("SanPham");
        private void ShowDGV()
        {
            dataTable.Clear();
            string query = "SELECT Ma_SP N'Mã_Sản_Phẩm', LoaiHang.Ma_loai_hang N'Mã_Loại_Hàng', Ten_SP N'Tên_Sản_phẩm', Ten_loai_hang N'Tên_Loại_Hàng', So_luong N'Số_Lượng', Gia_nhap N'Giá_Nhập', Gia_ban N'Giá_Bán', FileNames N'File Names'\r\nFROM dbo.LoaiHang\r\nJOIN dbo.SanPham ON SanPham.Ma_loai_hang = LoaiHang.Ma_loai_hang";
            SqlDataAdapter adapter = new SqlDataAdapter(query,conn);
            adapter.Fill(dataTable);
            dgv_sanpham.DataSource = dataTable;
        }

        //Search sản phẩm
        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {
            cbo_searchsanpham.SelectedIndex = -1;
            DataView dv = dataTable.DefaultView;
            // Access Text property of txt_search
            dv.RowFilter = string.Format("Mã_Sản_Phẩm LIKE '%{0}%' OR Tên_Sản_phẩm LIKE '%{0}%'", txt_searchsanpham.Text);
            dgv_sanpham.DataSource = dv.ToTable();
        }
        private void txt_searchsanpham_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*try
            {
                if (e.KeyChar == (char)13) // Check for Enter key
                {
                    DataView dv = dataTable.DefaultView;
                    // Access Text property of txt_search
                    dv.RowFilter = string.Format("Mã_Sản_Phẩm LIKE '%{0}%' OR Tên_Sản_phẩm LIKE '%{0}%'", txt_searchsanpham.Text);
                    dgv_sanpham.DataSource = dv.ToTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        //Search theo loại combobox
        private void cbo_searchsanpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = dataTable.DefaultView;
            txt_searchsanpham.Text = "";
            // Access Text property of cbo_searchsanpham
            string selectedValue = cbo_searchsanpham.Text;

            if (selectedValue.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
            {
                dv.RowFilter = "";
                load_form1();
            }
            else
            {
                // Sử dụng điều kiện OR để tìm kiếm theo Mã_Sản_Phẩm hoặc Tên_Sản_Phẩm
                dv.RowFilter = string.Format("Tên_Loại_Hàng LIKE '%{0}%'", selectedValue);
                load_form1();
            }
            dgv_sanpham.DataSource = dv.ToTable();

        }


        private void cbo_searchsanpham_KeyPress(object sender, KeyPressEventArgs e)
        {
        }





        //Tạo hàm khi load form
        private void load_form1()
        {
            btn_luu.Enabled = false;
            btn_sua.Enabled = false;
            btn_xoa.Enabled = false;
            btn_them.Enabled = true;
            txt_filesname.Enabled = false;

            //txt
            txt_masanpham.Text = "";
            txt_masanpham.Enabled = false;
            txt_tensanpham.Text = "";
            txt_soluong.Text = "";
            txt_gianhap.Text = "";
            txt_giaban.Text = "";
            txt_filesname.Text = "";
            cbo_maloaihang.Text = "";
            cbo_maloaihang.SelectedIndex = -1;
            txt_tenloaihang.Text = "";
            txt_tenloaihang.Enabled = false;

        }

        private void load_form2()
        {
            btn_luu.Enabled = true;
            btn_sua.Enabled = false;
            btn_them.Enabled = false;
            btn_xoa.Enabled = false;
            txt_filesname.Enabled = false;

            //txt
            /*txt_masanpham.Enabled = true;
            txt_masanpham.Text = "";
            txt_tensanpham.Text = "";
            txt_soluong.Text = "";
            txt_gianhap.Text = "";
            txt_giaban.Text = "";
            txt_filesname.Text = "";
            cbo_maloaihang.Text = "";*/

            //img
            pic_sanpham.Image = null;

        }

        private void load_form3()
        {
            btn_sua.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = true;
            btn_luu.Enabled = false;
            txt_filesname.Enabled = false;
        }


        //Form Load
        private void formCRUDsanpham_Load(object sender, EventArgs e)
        {
            //Load giá trị vào combobox
            LoadValue_Combobox();

            //Hiển thị thông tin lên datagridview
            ShowDGV();
            pic_sanpham.Image = null;
            //LoadForm1
            load_form1();
        }

        //Thêm Sản Phẩm
        private void btn_them_Click(object sender, EventArgs e)
        {
            formCRUDsanpham_Load(null, null);
            load_form2();
        }

        //Lưu Dữ liệu
        private void btn_luu_Click(object sender, EventArgs e)
        {
            try
            {

                sanpham.Ma_SP = txt_masanpham.Text;
                sanpham.Ten_SP = txt_tensanpham.Text;
                sanpham.So_luong = txt_soluong.Text;
                sanpham.Gia_nhap = txt_gianhap.Text;
                sanpham.Gia_ban = txt_giaban.Text;
                sanpham.FileNames = txt_filesname.Text;
                sanpham.Ma_loai_hang = cbo_maloaihang.Text;

                string getuser = TKBLL.CheckInsert_SanPham(sanpham);

                switch (getuser)
                {
                    case "requeid_khongnhap":
                        {
                            MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tensai_1":
                        {
                            MessageBox.Show("TÊN SẢN PHẨM KHÔNG ĐƯỢC NHẬP KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_soluong":
                        {
                            MessageBox.Show("SỐ LƯỢNG NHẬP SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_gianhap":
                        {
                            MessageBox.Show("GIÁ NHẬP NHẬP SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_giaban":
                        {
                            MessageBox.Show("GIÁ BÁN NHẬP SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tensanphamtontai":
                        {
                            MessageBox.Show("TÊN SẢN PHẨM ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_gialai":
                        {
                            MessageBox.Show("GIÁ BÁN PHẢI >= GIÁ NHẬP!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("THÊM THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formCRUDsanpham_Load(null, null);
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Sửa Sản Phẩm
        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                sanpham.Ma_SP = txt_masanpham.Text;
                sanpham.Ten_SP = txt_tensanpham.Text;
                sanpham.So_luong = txt_soluong.Text;
                sanpham.Gia_nhap = txt_gianhap.Text;
                sanpham.Gia_ban = txt_giaban.Text;
                sanpham.FileNames = txt_filesname.Text;
                sanpham.Ma_loai_hang = cbo_maloaihang.Text;

                string getuser = TKBLL.CheckUpdate_SanPham(sanpham);

                switch (getuser)
                {
                    case "requeid_khongnhap":
                        {
                            MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tensai_1":
                        {
                            MessageBox.Show("TÊN SẢN PHẨM KHÔNG ĐƯỢC NHẬP KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_soluong":
                        {
                            MessageBox.Show("SỐ LƯỢNG NHẬP SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_gianhap":
                        {
                            MessageBox.Show("GIÁ NHẬP NHẬP SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_giaban":
                        {
                            MessageBox.Show("GIÁ BÁN NHẬP SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tensanphamtontai":
                        {
                            MessageBox.Show("TÊN SẢN PHẨM ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_gialai":
                        {
                            MessageBox.Show("GIÁ BÁN PHẢI >= GIÁ NHẬP!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("CẬP NHẬT THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formCRUDsanpham_Load(null, null);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Xóa sản phẩm
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            sanpham.Ma_SP = txt_masanpham.Text;
            sanpham.Ten_SP = txt_tensanpham.Text;
            sanpham.So_luong = txt_soluong.Text;
            sanpham.Gia_nhap = txt_gianhap.Text;
            sanpham.Gia_ban = txt_giaban.Text;
            sanpham.FileNames = txt_filesname.Text;
            sanpham.Ma_loai_hang = cbo_maloaihang.Text;

            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN XÓA SẢN PHẨM NÀY KHÔNG???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string getuser = TKBLL.CheckDelete_SanPham(sanpham);

                if (getuser == "XÓA THÀNH CÔNG")
                {
                    MessageBox.Show("XÓA THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formCRUDsanpham_Load(null, null);
                }
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }







        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_maloai_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void lbl_maloai_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        
        
        
        

        //Lấy đường dẫn của ảnh
        private void pic_sanpham_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn của ảnh được chọn
                string imagePath = openFileDialog.FileName;

                // Hiển thị ảnh trong PictureBox
                pic_sanpham.ImageLocation = imagePath;

                // Hiển thị đường dẫn trong một TextBox hoặc Label, nếu cần
                txt_filesname.Text = imagePath;
            }
        }

        private void dgv_sanpham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void guna2HtmlLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void cbo_searchsanpham_TextChanged(object sender, EventArgs e)
        {
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
