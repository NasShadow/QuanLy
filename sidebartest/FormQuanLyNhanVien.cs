using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class FormQuanLyNhanVien : Form
    {
        NhanVien nhanvien = new NhanVien();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        public FormQuanLyNhanVien()
        {
            InitializeComponent();
            ComboBoxItems();
            txt_TinhTrang.Enabled = false;
            txt_maNV.Enabled = false;
            dgv_nhanvien.Update();
            txt_MK.Enabled = false;
            txt_XacNhanMK.Enabled = false;
        }

        private void FormQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            DisableButton();
            this.ControlBox = false;
            //Sẽ load lên datagridview
            dataTable.Clear();
            string query = "SELECT Ma_nhan_vien N'Mã_Nhân_Viên', Ten_NV N'Tên_Nhân_Viên', Email N'Email',MatKhau N'Mật_Khẩu',XacNhanMatKhau N'Xác_Nhận_Mật_Khẩu', IIF(Gioi_Tinh=1, 'Nam', N'Nữ') as N'Giới_Tính', VaiTro N'Vai_Trò',IIF(TinhTrang=1, N'Hoạt_Động', N'Ngừng_Hoạt_Động') as N'Tình_Trạng', DiaChi N'Địa_Chỉ' FROM dbo.NhanVien where VaiTro='Nhan Vien' or VaiTro='Shipper'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, TKBLL.Load());
            adapter.Fill(dataTable);
            dgv_nhanvien.DataSource = dataTable;
            dgv_nhanvien.Update();
            dgv_nhanvien.Refresh();
        }
        private void DisableButton()
        {
            btn_sua.Enabled = false;
            btn_vohieuhoa.Enabled = false;
        }
        private void EnableButton()
        {
            btn_sua.Enabled = true;
            btn_vohieuhoa.Enabled = true;
        }
        //Thêm loại hàng
        private void btn_them_Click_1(object sender, EventArgs e)
        {
            nhanvien.Ma_Nhan_Vien = txt_maNV.Text;
            nhanvien.Ten_NV = txt_tenNV.Text;
            nhanvien.Email = txt_Email.Text;
            nhanvien.MatKhau = txt_MK.Text;
            nhanvien.XacNhanMatKhau = txt_XacNhanMK.Text;
            if (rdo_Nu.Checked)
            {
                nhanvien.Gioi_Tinh = "0";
            }
            else
            {
                nhanvien.Gioi_Tinh = "1";
            }
            nhanvien.VaiTro = cbo_VaiTro.Text;
            nhanvien.TinhTrang = "1"; //mặc định là đang hoạt động
            nhanvien.DiaChi = txt_DiaChi.Text;


            string getuser = TKBLL.CheckInsert(nhanvien);

            switch (getuser)
            {
                case "requeid_manhanvien":
                    {
                        MessageBox.Show("MÃ NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_tennhanvien":
                    {
                        MessageBox.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_email":
                    {
                        MessageBox.Show("EMAIL KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_matkhau":
                    {
                        MessageBox.Show("MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_xacnhanmk":
                    {
                        MessageBox.Show("XÁC NHẬN MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_vaitro":
                    {
                        MessageBox.Show("VAI TRÒ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_diachi":
                    {
                        MessageBox.Show("ĐỊA CHỈ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                    }
                case "match_pass":
                    {
                        MessageBox.Show("MẬT KHẨU CỦA BẠN KHÔNG TRÙNG KHỚP!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("THÊM NHÂN VIÊN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        FormQuanLyNhanVien_Load(null, null);
                        break;
                    }
            }



        }


        private void ComboBoxItems()
        {
            //Vai trò
            cbo_VaiTro.Items.Add("Quan Ly");
            cbo_VaiTro.Items.Add("Nhan Vien");
        }


        DataTable dataTable = new DataTable("NhanVien");
        

        private void Reset_CurrendRoww()
        {
            {
                //Sẽ load lên datagridview
                dataTable.Clear();
                string query = "SELECT Ma_nhan_vien N'Mã_Nhân_Viên', Ten_NV N'Tên_Nhân_Viên', Email N'Email',MatKhau N'Mật_Khẩu',XacNhanMatKhau N'Xác_Nhận_Mật_Khẩu', IIF(Gioi_Tinh=1, 'Nam', N'Nữ') as N'Giới_Tính', VaiTro N'Vai_Trò',IIF(TinhTrang=1, N'Hoạt_Động', N'Ngừng_Hoạt_Động') as N'Tình_Trạng', DiaChi N'Địa_Chỉ' FROM dbo.NhanVien where VaiTro='Nhan Vien' or VaiTro='Shipper' ";
                SqlDataAdapter adapter = new SqlDataAdapter(query, TKBLL.Load());
                adapter.Fill(dataTable);
                dgv_nhanvien.DataSource = dataTable;
            }

        }

        private void dgv_nhanvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string strGioiTinh = "";
            int i = dgv_nhanvien.CurrentRow.Index;
            txt_maNV.Text = dgv_nhanvien.Rows[i].Cells[0].Value.ToString();
            txt_tenNV.Text = dgv_nhanvien.Rows[i].Cells[1].Value.ToString();
            txt_Email.Text = dgv_nhanvien.Rows[i].Cells[2].Value.ToString();
            trung_email = dgv_nhanvien.Rows[i].Cells[2].Value.ToString();
            txt_MK.Text = dgv_nhanvien.Rows[i].Cells[3].Value.ToString();
            txt_XacNhanMK.Text = dgv_nhanvien.Rows[i].Cells[4].Value.ToString();
            strGioiTinh = dgv_nhanvien.Rows[i].Cells[5].Value.ToString();
            cbo_VaiTro.Text = dgv_nhanvien.Rows[i].Cells[6].Value.ToString();
            txt_TinhTrang.Text = dgv_nhanvien.Rows[i].Cells[7].Value.ToString();
            txt_DiaChi.Text = dgv_nhanvien.Rows[i].Cells[8].Value.ToString();
            CheckDisable();
            if (string.Equals(strGioiTinh, "Nam"))
            {
                rdo_Nam.Checked = true;
            }
            else
            {
                rdo_Nu.Checked = true;
            }
            EnableButton();

        }

        static string trung_email = "";

        private void btn_sua_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("BẠN MUỐN CẬP NHẬP THÔNG TIN TÀI KHOẢN NÀY???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                nhanvien.Ma_Nhan_Vien = txt_maNV.Text;
                nhanvien.Ten_NV = txt_tenNV.Text;
                nhanvien.Email = txt_Email.Text;
                nhanvien.MatKhau = txt_MK.Text;
                nhanvien.XacNhanMatKhau = txt_XacNhanMK.Text;
                nhanvien.VaiTro = cbo_VaiTro.Text;
                if (rdo_Nu.Checked)
                {
                    nhanvien.Gioi_Tinh = "0";
                }
                else
                {
                    nhanvien.Gioi_Tinh = "1";
                }
                nhanvien.VaiTro = cbo_VaiTro.Text;
                nhanvien.DiaChi = txt_DiaChi.Text;



                if (txt_Email.Text != trung_email)
                {
                    string getuser = TKBLL.CheckUpdate(nhanvien);
                    switch (getuser)
                    {
                        case "requeid_manhanvien":
                            {
                                MessageBox.Show("MÃ NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_tennhanvien":
                            {
                                MessageBox.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "wrong_name":
                            {
                                MessageBox.Show("TÊN KHÔNG ĐƯỢC NHẬP SỐ!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email":
                            {
                                MessageBox.Show("EMAIL KHÔNG ĐƯỢC BỎ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email1":
                            {
                                MessageBox.Show("ĐỊA CHỈ EMAIL KHÔNG TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau":
                            {
                                MessageBox.Show("MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau1":
                            {
                                MessageBox.Show("MẬT KHẨU PHẢI CHỨA ÍT NHẤT MỘT KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_xacnhanmk":
                            {
                                MessageBox.Show("XÁC NHẬN MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_vaitro":
                            {
                                MessageBox.Show("VAI TRÒ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_diachi":
                            {
                                MessageBox.Show("ĐỊA CHỈ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "match_pass":
                            {
                                MessageBox.Show("MẬT KHẨU CỦA BẠN KHÔNG TRÙNG KHỚP!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "trung_email":
                            {
                                MessageBox.Show("EMAIL NÀY ĐÃ TỒN TẠI KHÔNG THỂ THAY ĐỔI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "trung_email2":
                            {
                                MessageBox.Show("EMAIL NÀY ĐÃ TỒN TẠI KHÔNG THỂ THAY ĐỔI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "trung_email3":
                            {
                                MessageBox.Show("EMAIL NÀY ĐÃ TỒN TẠI KHÔNG THỂ THAY ĐỔI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("CẬP NHẬT THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyNhanVien_Load(null, null);
                                ClearText();
                                break;
                            }
                    }
                    return;
                }
                else if(txt_Email.Text == trung_email)
                {
                    string getuser1 = TKBLL.CheckUpdate1(nhanvien);
                    switch (getuser1)
                    {
                        case "requeid_manhanvien":
                            {
                                MessageBox.Show("MÃ NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_tennhanvien":
                            {
                                MessageBox.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "wrong_name":
                            {
                                MessageBox.Show("TÊN KHÔNG ĐƯỢC NHẬP SỐ!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email":
                            {
                                MessageBox.Show("EMAIL KHÔNG ĐƯỢC BỎ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email1":
                            {
                                MessageBox.Show("ĐỊA CHỈ EMAIL KHÔNG TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau":
                            {
                                MessageBox.Show("MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau1":
                            {
                                MessageBox.Show("MẬT KHẨU PHẢI CHỨA ÍT NHẤT MỘT KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_xacnhanmk":
                            {
                                MessageBox.Show("XÁC NHẬN MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_vaitro":
                            {
                                MessageBox.Show("VAI TRÒ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_diachi":
                            {
                                MessageBox.Show("ĐỊA CHỈ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        case "match_pass":
                            {
                                MessageBox.Show("MẬT KHẨU CỦA BẠN KHÔNG TRÙNG KHỚP!!!", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("CẬP NHẬT THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyNhanVien_Load(null, null);
                                ClearText();
                                break;
                            }
                    }
                    return;
                }
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }
        private void CheckDisable()
        {
            if (txt_TinhTrang.Text == "Hoạt_Động")
            {
                btn_vohieuhoa.Text = "Vô Hiệu Hóa";
            }
            else if (txt_TinhTrang.Text == "Ngừng_Hoạt_Động")
            {
                btn_vohieuhoa.Text = "Mở Tài Khoản";
            }
        }
        private void ClearText()
        {
            txt_maNV.Text = "";
            txt_tenNV.Text = "";
            txt_Email.Text = "";
            txt_MK.Text = "";
            txt_XacNhanMK.Text = "";
            txt_TinhTrang.Text = "";
            txt_DiaChi.Text = "";
            cbo_VaiTro.SelectedIndex = -1;
        }
        private void btn_vohieuhoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("BẢN MUỐN CHỈNH SỬA TRẠNG THÁI HOẠT ĐỘNG NÀY CHỨ???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            nhanvien.Ma_Nhan_Vien = txt_maNV.Text;
            if (btn_vohieuhoa.Text == "Vô Hiệu Hóa")
            {
                nhanvien.TinhTrang = "0";
                string getuser = TKBLL.CheckDisable(nhanvien);

                switch (getuser)
                {
                    case "requeid_manhanvien":
                        {
                            MessageBox.Show("MÃ NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("VÔ HIỆU HÓA THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormQuanLyNhanVien_Load(null, null);
                            ClearText();
                            break;
                        }
                }
            }
            else if (btn_vohieuhoa.Text == "Mở Tài Khoản")
            {
                nhanvien.TinhTrang = "1";

                string getuser = TKBLL.CheckDisable(nhanvien);

                switch (getuser)
                {
                    case "requeid_manhanvien":
                        {
                            MessageBox.Show("MÃ NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("MỞ KHÓA TÀI KHOẢN THÀNH CÔNG!!!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormQuanLyNhanVien_Load(null, null);
                            ClearText();
                            break;
                        }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_mota_Click(object sender, EventArgs e)
        {

        }
        private string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    sb.Append(hashedBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
        //Add chuỗi mã hóa
        private void dgv_nhanvien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgv_nhanvien.Columns["Mật_Khẩu"].Index || e.ColumnIndex == dgv_nhanvien.Columns["Xác_Nhận_Mật_Khẩu"].Index)
            {
                // Kiểm tra giá trị của ô không phải là null
                if (e.Value != null)
                {
                    // Mã hóa giá trị và hiển thị giá trị đã mã hóa thay vì giá trị thực tế
                    e.Value = EncryptPassword(e.Value.ToString());
                }

            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbo_TinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_TinhTrang_TextChanged(object sender, EventArgs e)
        {

        }
        //search
        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dataTable.DefaultView;
            // Access Text property of txt_search
            dv.RowFilter = string.Format("Mã_Nhân_Viên LIKE '%{0}%' OR Email LIKE '%{0}%'", txt_search.Text);
            dgv_nhanvien.DataSource = dv.ToTable();
        }

        private void formSub1_Activated(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Reset_CurrendRoww();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Refresh_Click_1(object sender, EventArgs e)
        {
            Reset_CurrendRoww();
        }

        private void txt_XacNhanMK_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
