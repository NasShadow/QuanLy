using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class FormQuanLyShipper : Form
    {
        NhanVienGiaoHang nhanviengiaohang = new NhanVienGiaoHang();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        DataTable dataTable = new DataTable("NhanVienGiaoHang");

        public FormQuanLyShipper()
        {
            InitializeComponent();
        }

        private void FormQuanLyShipper_Load(object sender, EventArgs e)
        {
            rdo_Nam.Checked = false;
            rdo_Nu.Checked = false;

            dataTable.Clear();
            txt_TinhTrang.Enabled = false;
            txt_maNV.Enabled = false;
            this.ControlBox = false;
            string query = "SELECT Ma_nhan_vienGH N'Mã_Nhân_Viên', Ten_NV_GH N'Tên_Nhân_Viên', Email N'Email',MatKhau N'Mật_Khẩu',XacNhanMatKhau N'Xác_Nhận_Mật_Khẩu', IIF(Gioi_Tinh=1, 'Nam', N'Nữ') as N'Giới_Tính',IIF(TinhTrang=1, N'Hoạt_Động', N'Ngừng_Hoạt_Động') as N'Tình_Trạng', DiaChi N'Địa_Chỉ' FROM dbo.NhanVienGiaoHang";
            SqlDataAdapter adapter = new SqlDataAdapter(query, TKBLL.Load());
            adapter.Fill(dataTable);
            dgv_nhanvien.DataSource = dataTable;
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
        private void EnableButton()
        {
            btn_sua.Enabled = true;
            btn_vohieuhoa.Enabled = true;
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
            txt_TinhTrang.Text = dgv_nhanvien.Rows[i].Cells[6].Value.ToString();
            txt_DiaChi.Text = dgv_nhanvien.Rows[i].Cells[7].Value.ToString();
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

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dataTable.DefaultView;
            // Access Text property of txt_search
            dv.RowFilter = string.Format("Mã_Nhân_Viên LIKE '%{0}%' OR Email LIKE '%{0}%'", txt_search.Text);
            dgv_nhanvien.DataSource = dv.ToTable();
        }

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


        static string trung_email = "";

        private void btn_sua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN CẬP NHẬT THÔNG TIN TÀI KHOẢN NÀY", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                nhanviengiaohang.Ma_Nhan_VienGH = txt_maNV.Text;
                nhanviengiaohang.Ten_NV_GH = txt_tenNV.Text;
                nhanviengiaohang.Email = txt_Email.Text;
                nhanviengiaohang.MatKhau = txt_MK.Text;
                nhanviengiaohang.XacNhanMatKhau = txt_XacNhanMK.Text;
                if (rdo_Nu.Checked)
                {
                    nhanviengiaohang.Gioi_Tinh = "0";
                }
                else
                {
                    nhanviengiaohang.Gioi_Tinh = "1";
                }
                nhanviengiaohang.DiaChi = txt_DiaChi.Text;
                


                if (txt_Email.Text != trung_email)
                {
                    string getuser = TKBLL.CheckUpdateShipper(nhanviengiaohang);
                    switch (getuser)
                    {
                        case "requeid_manhanvien":
                            {
                                MessageBox.Show("MÃ NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_tennhanvien":
                            {
                                MessageBox.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "wrong_name":
                            {
                                MessageBox.Show("VUI LÒNG NHẬP CHỮ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email":
                            {
                                MessageBox.Show("EMAIL KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email1":
                            {
                                MessageBox.Show("ĐỊA CHỈ EMAIL KHÔNG PHÙ HỢP!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau":
                            {
                                MessageBox.Show("MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau1":
                            {
                                MessageBox.Show("MẬT KHẨU PHẢI CHỨA ÍT NHẤT 1 KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_xacnhanmk":
                            {
                                MessageBox.Show("XÁC NHẬN MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_vaitro":
                            {
                                MessageBox.Show("VAI TRÒ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_diachi":
                            {
                                MessageBox.Show("ĐỊA CHỈ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "match_pass":
                            {
                                MessageBox.Show("MẬT KHẨU CỦA BẠN KHÔNG TRÙNG KHỚP!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                MessageBox.Show("CẬP NHẬT THÔNG TIN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyShipper_Load(null, null);
                                ClearText();
                                break;
                            }
                    }
                    return;
                }
                else if (txt_Email.Text == trung_email)
                {
                    string getuser1 = TKBLL.CheckUpdateShipper1(nhanviengiaohang);

                    switch (getuser1)
                    {
                        case "requeid_manhanvien":
                            {
                                MessageBox.Show("MÃ NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_tennhanvien":
                            {
                                MessageBox.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "wrong_name":
                            {
                                MessageBox.Show("VUI LÒNG NHẬP CHỮ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email":
                            {
                                MessageBox.Show("EMAIL KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email1":
                            {
                                MessageBox.Show("ĐỊA CHỈ EMAIL KHÔNG PHÙ HỢP!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau":
                            {
                                MessageBox.Show("MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau1":
                            {
                                MessageBox.Show("MẬT KHẨU PHẢI CHỨA ÍT NHẤT 1 KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_xacnhanmk":
                            {
                                MessageBox.Show("XÁC NHẬN MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_vaitro":
                            {
                                MessageBox.Show("VAI TRÒ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_diachi":
                            {
                                MessageBox.Show("ĐỊA CHỈ KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "match_pass":
                            {
                                MessageBox.Show("MẬT KHẨU CỦA BẠN KHÔNG TRÙNG KHỚP!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("CẬP NHẬT THÔNG TIN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyShipper_Load(null, null);
                                ClearText();
                                break;
                            }
                    }
                }
                return;
            }
            else if (result == DialogResult.No)
            {
                return;
            }



        }

        private void btn_vohieuhoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN CHỈNH SỬA TRẠNG THÁI CỦA TÀI KHOẢN NÀY???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {

                nhanviengiaohang.Ma_Nhan_VienGH = txt_maNV.Text;
                if (btn_vohieuhoa.Text == "Vô Hiệu Hóa")
                {
                    nhanviengiaohang.TinhTrang = "0";
                    string getuser = TKBLL.CheckDisableShipper(nhanviengiaohang);

                    switch (getuser)
                    {
                        case "requeid_manhanvien":
                            {
                                MessageBox.Show("MÃ NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("CHỈNH SỬA TRẠNG THÁI THÁNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyShipper_Load(null, null);
                                ClearText();
                                break;
                            }
                    }
                }
                else if (btn_vohieuhoa.Text == "Mở Tài Khoản")
                {
                    nhanviengiaohang.TinhTrang = "1";

                    string getuser = TKBLL.CheckDisableShipper(nhanviengiaohang);

                    switch (getuser)
                    {
                        case "requeid_manhanvien":
                            {
                                MessageBox.Show("Mã nhân viên không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("MỞ KHÓA TÀI KHOẢN THÀNH CÔNG!!!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyShipper_Load(null, null);
                                ClearText();
                                break;
                            }
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
        }

        private void Reset_CurrendRoww()
        {
            //Sẽ load lên datagridview
            dataTable.Clear();
            string query = "SELECT Ma_nhan_vienGH N'Mã_Nhân_Viên', Ten_NV_GH N'Tên_Nhân_Viên', Email N'Email',MatKhau N'Mật_Khẩu',XacNhanMatKhau N'Xác_Nhận_Mật_Khẩu', IIF(Gioi_Tinh=1, 'Nam', N'Nữ') as N'Giới_Tính',IIF(TinhTrang=1, N'Hoạt_Động', N'Ngừng_Hoạt_Động') as N'Tình_Trạng', DiaChi N'Địa_Chỉ' FROM dbo.NhanVienGiaoHang";
            SqlDataAdapter adapter = new SqlDataAdapter(query, TKBLL.Load());
            adapter.Fill(dataTable);
            dgv_nhanvien.DataSource = dataTable;
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Reset_CurrendRoww();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txt_Email.Text != trung_email)
            {
                MessageBox.Show("CC");
            }
            else if (txt_Email.Text == trung_email)
            {
                MessageBox.Show("HIHI");
            }
        }
    }
}
