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
    public partial class FormQuanLyKhachHang : Form
    {
        static KhachHang khachhang = new KhachHang();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        SqlConnection conn = TKBLL.Load();

        public FormQuanLyKhachHang()
        {
            InitializeComponent();
            dgv_khachhang.CellFormatting += dgv_khachhang_CellFormatting;
        }

        //Thêm Khách hàng
        private void btn_luu_Click(object sender, EventArgs e)
        {
        }

        //Sửa khách hàng
        private void btn_sua_Click(object sender, EventArgs e)
        {
            khachhang.Ma_Khach_Hang = txt_makhachhang.Text;
            khachhang.Ten_Khach_Hang = txt_tenkhachhang.Text;
            khachhang.Ten_TK = txt_tentaikhoan.Text;
            khachhang.Email = txt_email.Text;
            khachhang.MatKhau = txt_matkhau.Text;
            khachhang.XacNhanMatKhau = txt_xacnhanmatkhau.Text;
            khachhang.Gioi_Tinh = cbo_gioitinh.Text;
            khachhang.DienThoai = txt_dienthoai.Text;
            txt_trangthai.Text = "Hoạt Động";
            khachhang.TrangThai = txt_trangthai.Text;

            //Kiểm tra giới tính thêm vào CSDL
            if (cbo_gioitinh.Text == "Nam")
            {
                khachhang.Gioi_Tinh = "1";
            }
            else if (cbo_gioitinh.Text == "Nữ")
            {
                khachhang.Gioi_Tinh = "0";
            }

            //Kiểm tra TRẠNG THÁI thêm vào CSDL
            if (txt_trangthai.Text == "Hoạt Động")
            {
                khachhang.TrangThai = "1";
            }



            if (txt_email.Text != trung_email)
            {
                string getuser = TKBLL.CheckUpdate_KhachHang(khachhang);
                MessageBox.Show("Hello1");
                switch (getuser)
                {
                    case "requeid_botrong":
                        {
                            MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    /*case "requeid_trungtentk":
                        {
                            MessageBox.Show("TÊN TÀI KHOẢN ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }*/
                    /*case "requeid_tenkhachhang":
                        {
                            MessageBox.Show("TÊN KHÔNG ĐƯỢC NHẬP KÝ TỰ ĐẶC BIỆT VÀ SỐ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }*/
                    case "requeid_tentaikhoan":
                        {
                            MessageBox.Show("TÊN TÀI KHOẢN KHÔNG ĐƯỢC NHẬP KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_email":
                        {
                            MessageBox.Show("EMAIL SAI ĐỊNH DẠNG @gmail.com!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_trungemail":
                        {
                            MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_trungemail2":
                        {
                            MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_trungemail3":
                        {
                            MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_matkhau_1":
                        {
                            MessageBox.Show("MẬT KHẨU PHẢI CÓ KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_matkhau_2":
                        {
                            MessageBox.Show("MẬT KHẨU QUÁ NGẮN!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_xacnhanmatkhau":
                        {
                            MessageBox.Show("MẬT KHẨU KHÔNG TRÙNG KHỚP NHAU!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_dienthoai":
                        {
                            MessageBox.Show("SỐ ĐIỆN THOẠI KHÔNG TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("CẬP NHẬT THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormQuanLyKhachHang_Load(null, null);
                            break;
                        }
                }
                return;
            }
            else if (txt_email.Text == trung_email)
            {
                string getuser1 = TKBLL.CheckUpdate_KhachHang1(khachhang);
                MessageBox.Show("Hello2");
                switch (getuser1)
                {
                    case "requeid_botrong":
                        {
                            MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    /*case "requeid_trungtentk":
                        {
                            MessageBox.Show("TÊN TÀI KHOẢN ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }*/
                    /*case "requeid_tenkhachhang":
                        {
                            MessageBox.Show("TÊN KHÔNG ĐƯỢC NHẬP KÝ TỰ ĐẶC BIỆT VÀ SỐ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }*/
                    case "requeid_tentaikhoan":
                        {
                            MessageBox.Show("TÊN TÀI KHOẢN KHÔNG ĐƯỢC NHẬP KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_email":
                        {
                            MessageBox.Show("EMAIL SAI ĐỊNH DẠNG @gmail.com!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_matkhau_1":
                        {
                            MessageBox.Show("MẬT KHẨU PHẢI CÓ KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_matkhau_2":
                        {
                            MessageBox.Show("MẬT KHẨU QUÁ NGẮN!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_xacnhanmatkhau":
                        {
                            MessageBox.Show("MẬT KHẨU KHÔNG TRÙNG KHỚP NHAU!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_dienthoai":
                        {
                            MessageBox.Show("SỐ ĐIỆN THOẠI KHÔNG TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("CẬP NHẬT THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormQuanLyKhachHang_Load(null, null);
                            break;
                        }
                }
                return;
            }
        }

        //Vô hiệu hóa tài khoản
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN VÔ HIỆU HÓA TÀI KHOẢN NÀY KHÔNG???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            
            khachhang.Ma_Khach_Hang = txt_makhachhang.Text;
            khachhang.Ten_Khach_Hang = txt_tenkhachhang.Text;
            khachhang.Ten_TK = txt_tentaikhoan.Text;
            khachhang.Email = txt_email.Text;
            khachhang.MatKhau = txt_matkhau.Text;
            khachhang.XacNhanMatKhau = txt_xacnhanmatkhau.Text;
            khachhang.Gioi_Tinh = cbo_gioitinh.Text;
            khachhang.DienThoai = txt_dienthoai.Text;
            if (result == DialogResult.Yes)
            {
                if (btn_vohieuhoa.Text == "Vô Hiệu Hóa")
                {
                    khachhang.TrangThai = "0";

                    string getuser = TKBLL.CheckVHH_KhachHang(khachhang);

                    switch (getuser)
                    {
                        case "requeid_botrong":
                            {
                                MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("VÔ HIỆU HÓA THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyKhachHang_Load(null, null);
                                break;
                            }
                    }
                }
                else if (btn_vohieuhoa.Text == "Mở Tài Khoản")
                {
                    khachhang.TrangThai = "1";

                    string getuser = TKBLL.CheckVHH_KhachHang(khachhang);

                    switch (getuser)
                    {
                        case "requeid_botrong":
                            {
                                MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("MỞ KHÓA TÀI KHOẢN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyKhachHang_Load(null, null);
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


        //Hàm loadForm
        private void FormQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            //Hiển thị thông tin lên datagridview
            Load_datagridview();

            //Loadcombobox
            Load_Combobox();

            //Loadbtn
            load_form1();
        }

        //Nút thêm
        private void btn_them_Click(object sender, EventArgs e)
        {
            load_form2();
        }

        //Tạo hàm khi load form
        private void load_form1()
        {
            btn_sua.Enabled = false;
            btn_vohieuhoa.Enabled = false;

            txt_makhachhang.Enabled = false;
            txt_trangthai.Enabled = false;

            //txt
            txt_makhachhang.Text = "";
            txt_tenkhachhang.Text = "";
            txt_tentaikhoan.Text = "";
            txt_email.Text = "";
            txt_matkhau.Text = "";
            txt_xacnhanmatkhau.Text = "";
            cbo_gioitinh.Text = "";
            txt_dienthoai.Text = "";
            txt_trangthai.Text = "";
            txt_tentaikhoan.Enabled = true;

            cbo_gioitinh.SelectedIndex = -1;
        }

        private void load_form2()
        {
            btn_sua.Enabled = false;
            btn_vohieuhoa.Enabled = false;

            //txt
            txt_makhachhang.Text = "";
            txt_tenkhachhang.Text = "";
            txt_tentaikhoan.Text = "";
            txt_email.Text = "";
            txt_matkhau.Text = "";
            txt_xacnhanmatkhau.Text = "";
            cbo_gioitinh.Text = "";
            txt_dienthoai.Text = "";
            txt_trangthai.Text = "";
            txt_tentaikhoan.Enabled = true;

            cbo_gioitinh.SelectedIndex = -1;
        }

        private void load_form3()
        {
            btn_sua.Enabled = true;
            btn_vohieuhoa.Enabled = true;

            //txt
            txt_tentaikhoan.Enabled = false;
        }


        //Sự kiện load Datagridview
        DataTable dataTable = new DataTable("KhachHang");
        private void Load_datagridview()
        {
            dataTable.Clear();
            string query = "SELECT Ma_Khach_Hang AS N'Mã_Khách_Hàng', Ten_Khach_Hang AS N'Tên_Khách_Hàng', Ten_TK AS N'Tên_Tài_Khoản', Email AS N'Email', MatKhau AS N'Mật_Khẩu', XacNhanMatKhau AS N'Xác_Nhận_Mật_Khẩu', IIF(Gioi_Tinh = 1, N'Nam', N'Nữ') AS N'Giới_Tính', DienThoai AS N'Điện_Thoại', IIF(TrangThai = 1, N'Hoạt Động', N'Vô Hiệu Hóa') AS N'Trạng_Thái' FROM dbo.KhachHang";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(dataTable);
            dgv_khachhang.DataSource = dataTable;
        }

        // Mã hóa mật khẩu sử dụng SHA-256
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
        private void dgv_khachhang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgv_khachhang.Columns["Mật_Khẩu"].Index || e.ColumnIndex == dgv_khachhang.Columns["Xác_Nhận_Mật_Khẩu"].Index)
            {
                // Kiểm tra giá trị của ô không phải là null
                if (e.Value != null)
                {
                    // Mã hóa giá trị và hiển thị giá trị đã mã hóa thay vì giá trị thực tế
                    e.Value = EncryptPassword(e.Value.ToString());
                }
            }
        }

        //Tìm Kiếm tài khoản
        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dataTable.DefaultView;
            // Access Text property of txt_search
            dv.RowFilter = string.Format("Mã_Khách_Hàng LIKE '%{0}%' OR Tên_Khách_Hàng LIKE '%{0}%'", txt_search.Text);
            dgv_khachhang.DataSource = dv.ToTable();
        }

        static string trung_email = "";

        //Ấn vào lướt và hiển thị thông tin lên textbox
        private void dgv_khachhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgv_khachhang.CurrentRow.Index;

            txt_makhachhang.Text = dgv_khachhang.Rows[i].Cells[0].Value.ToString();
            txt_tenkhachhang.Text = dgv_khachhang.Rows[i].Cells[1].Value.ToString();
            txt_tentaikhoan.Text = dgv_khachhang.Rows[i].Cells[2].Value.ToString();
            txt_email.Text = dgv_khachhang.Rows[i].Cells[3].Value.ToString();
            trung_email = dgv_khachhang.Rows[i].Cells[3].Value.ToString();
            txt_matkhau.Text = dgv_khachhang.Rows[i].Cells[4].Value.ToString();
            txt_xacnhanmatkhau.Text = dgv_khachhang.Rows[i].Cells[5].Value.ToString();
            cbo_gioitinh.Text = dgv_khachhang.Rows[i].Cells[6].Value.ToString();
            txt_dienthoai.Text = dgv_khachhang.Rows[i].Cells[7].Value.ToString();
            txt_trangthai.Text = dgv_khachhang.Rows[i].Cells[8].Value.ToString();

            //Checkbutton
            CheckVoHieuHoa();


            //loadform3
            load_form3();
        }

        //Hàm kiểm tra vô hiệu hóa
        private void CheckVoHieuHoa()
        {
            if (txt_trangthai.Text == "Hoạt Động")
            {
                btn_vohieuhoa.Text = "Vô Hiệu Hóa";
            }
            else if (txt_trangthai.Text == "Vô Hiệu Hóa")
            {
                btn_vohieuhoa.Text = "Mở Tài Khoản";
            }
        }









        //Hàm loadcombobox
        private void Load_Combobox()
        {
            //Add 2 giá trị nam nữ vào combobox_gioitinh
            cbo_gioitinh.Items.Clear();

            cbo_gioitinh.Items.Add("Nam");
            cbo_gioitinh.Items.Add("Nữ");
        }





        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
