using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class formSettings : Form
    {
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        NhanVien nhanvien = new NhanVien();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();
        public string email { get; set; }

        public formSettings(string email)
        {
            InitializeComponent();

            this.email = email;

            //Load Combobox
            Load_ComboBox();

            Value_defaut();
        }

        private void formSettings_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            load();
            //Load dữ liệu
            Load_Data();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(panel_changepass.Visible == true)
            {
                panel_changepass.Visible = false;
            }
            else if (panel_changepass.Visible == false)
            {
                Random_OTP();
                Send_OTP_Email();
                panel_changepass.Visible = true;
            }
        }

        //Load combobox
        private void Load_ComboBox()
        {
            cbo_gioitinh.Items.Add("Nam");
            cbo_gioitinh.Items.Add("Nữ");
        }
        static string maNV = "";
        //Load Dữ liệu của mã khách hàng
        private void Load_Data()
        {
            try
            {
                using (SqlConnection conn = TKBLL.Load())
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM dbo.NhanVien WHERE Email = N'{this.email}'", conn);
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {


                        while (reader.Read())
                        {
                            // Lấy giá trị từ cột "Ma_loai_hang"
                            string TenNV = reader["Ten_NV"].ToString();
                            string MatKhau = reader["MatKhau"].ToString();
                            string Gioi_Tinh = reader["Gioi_Tinh"].ToString();
                            string VaiTro = reader["VaiTro"].ToString();
                            string TinhTrang = reader["TinhTrang"].ToString();
                            string DiaChi = reader["DiaChi"].ToString();
                            string MaNhanVien = reader["Ma_Nhan_Vien"].ToString();
                            maNV = "";
                            maNV = MaNhanVien;
                            //Check Gtinh
                            if (Gioi_Tinh == "True")
                            {
                                Gioi_Tinh = "Nam";
                            }
                            else if (Gioi_Tinh == "False")
                            {
                                Gioi_Tinh = "Nữ";
                            }

                            //Check Tình trạng
                            if (TinhTrang == "True")
                            {
                                TinhTrang = "Hoạt Động";
                            }
                            else if (TinhTrang == "False")
                            {
                                TinhTrang = "Ngừng Hoạt Động";
                            }

                            txt_tennhanvien.Text = TenNV;
                            txt_matkhau.Text = MatKhau;
                            cbo_gioitinh.Text = Gioi_Tinh;
                            txt_vaitro.Text = VaiTro;
                            txt_tinhtrang.Text = TinhTrang;
                            cbo_gioitinh.Text = Gioi_Tinh;
                            txt_email.Text = this.email;
                            txt_diachi.Text = DiaChi;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void Value_defaut()
        {
            txt_vaitro.Enabled = false;
            txt_tinhtrang.Enabled = false;
            txt_matkhau.Enabled = false;
            txt_email.Enabled = false;

            txt_matkhau.PasswordChar = '*';
        }

        //Xác nhận khi đổi mk
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if(txt_mkhientai.Text != txt_matkhau.Text)
            {
                MessageBox.Show("MẬT KHẨU HIỆN TẠI CỦA BẠN ĐÃ SAI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(txt_otp.Text != otp_email)
            {
                MessageBox.Show("MÃ OTP CỦA BẠN ĐÃ SAI HÃY KIỂM TRA LẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            nhanvien.MatKhau = txt_mkmoi.Text;
            nhanvien.XacNhanMatKhau = txt_xacnhanmkmoi.Text;
            nhanvien.Ma_Nhan_Vien = maNV;

            string get = TKBLL.Change_PassFGAdmin(nhanvien);

            switch (get)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "khongbangnhau":
                    {
                        MessageBox.Show("MẬT KHẨU KHÔNG TRÙNG KHỚP", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_pass":
                    {
                        MessageBox.Show("MẬT KHẨU CẦN CHỨA ÍT NHẤT 1 KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "Short":
                    {
                        MessageBox.Show("MẬT KHẨU CỦA BẠN QUÁ NGẮN", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formSettings_Load(null, null);
                        break;
                    }
            }
        }

        private void load()
        {
            txt_mkmoi.Text = "";
            txt_xacnhanmkmoi.Text = "";
            txt_mkhientai.Text = "";
            txt_otp.Text = "";
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            nhanvien.Ma_Nhan_Vien = maNV;
            nhanvien.Ten_NV = txt_tennhanvien.Text;
            nhanvien.Email = txt_email.Text;
            if (cbo_gioitinh.Text == "Nam")
            {
                nhanvien.Gioi_Tinh = "1";
            }
            else if(cbo_gioitinh.Text == "Nữ")
            {
                nhanvien.Gioi_Tinh = "0";
            }
            nhanvien.DiaChi = txt_diachi.Text;

            string get = TKBLL.UpdateProFileAdmin(nhanvien);
            switch (get)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "wrong_name":
                    {
                        MessageBox.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC GHI SỐ HOẶC KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_email":
                    {
                        MessageBox.Show("EMAIL SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formSettings_Load(null,null);
                        break;
                    }
            }

        }

        //Biến cục bộ chứa mã otp
        static string otp_email = "";

        //Hàm random code
        private void Random_OTP()
        {
            // Tạo một đối tượng Random để sinh số ngẫu nhiên
            Random random = new Random();

            // Tạo một chuỗi StringBuilder để xây dựng chuỗi ngẫu nhiên
            StringBuilder sb = new StringBuilder();

            // Tạo một mảng chứa tất cả các ký tự được cho phép
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Tạo một vòng lặp để thêm mỗi ký tự vào chuỗi ngẫu nhiên
            for (int i = 0; i < 6; i++)
            {
                // Lấy một ký tự ngẫu nhiên từ mảng characters
                char randomChar = characters[random.Next(characters.Length)];

                // Thêm ký tự ngẫu nhiên vào chuỗi
                sb.Append(randomChar);
            }

            otp_email = sb.ToString();
        }

        //Hàm gửi mã
        private void Send_OTP_Email()
        {
            string form, to, pass, content;
            form = "kienhttd00367@fpt.edu.vn";
            to = this.email;
            pass = "ldjw pdqo tbnj rftk";
            //Nội dung gửi mail
            content = "Mã OTP để đổi mật khẩu là: " + otp_email;

            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(form);
            //Tiêu đề cho nội dung gửi
            mail.Subject = "MÃ THAY ĐỔI MẬT KHẨU TẠI SUNNY BOOK";
            mail.Body = content;

            // khởi tạo nó với địa chỉ của máy chủ SMTP. Trong trường hợp này, địa chỉ máy chủ SMTP là "smtp.gmail.com"
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            //Bật tính năng SSL để tạo một kết nối an toàn với máy chủ SMTP. SSL (Secure Sockets Layer) cung cấp một lớp bảo mật cho việc truyền thông tin giữa ứng dụng và máy chủ.
            smtp.EnableSsl = true;
            //Thiết lập cổng kết nối SMTP. Trong trường hợp này, sử dụng cổng 587, được sử dụng phổ biến cho kết nối SMTP bảo mật.
            smtp.Port = 587;
            //Thiết lập phương thức gửi email là qua mạng (Network).
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //Cung cấp thông tin xác thực cho máy chủ SMTP. Đối tượng NetworkCredential được tạo với tên đăng nhập (form) và mật khẩu (pass).
            smtp.Credentials = new NetworkCredential(form, pass);

            try
            {
                smtp.Send(mail);
                Console.WriteLine("Gửi Mail Thành Công!!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void btn_guilai_Click(object sender, EventArgs e)
        {
            Random_OTP();
            Send_OTP_Email();
        }

        private void pic_hien_Click(object sender, EventArgs e)
        {
            if (txt_mkmoi.PasswordChar == '*')
            {
                txt_mkmoi.PasswordChar = '\0';
                pic_an.Visible = true;
                pic_hien.Visible = false;
            }
        }

        private void pic_an_Click(object sender, EventArgs e)
        {
            if (txt_mkmoi.PasswordChar == '\0' )
            {
                txt_mkmoi.PasswordChar = '*';
                pic_an.Visible = false;
                pic_hien.Visible = true;
            }
        }

        private void pichien1_Click(object sender, EventArgs e)
        {
            if (txt_xacnhanmkmoi.PasswordChar == '*')
            {
                txt_xacnhanmkmoi.PasswordChar = '\0';
                pican1.Visible = true;
                pichien1.Visible = false;
            }
        }

        private void pican1_Click(object sender, EventArgs e)
        {
            if (txt_xacnhanmkmoi.PasswordChar == '\0')
            {
                txt_xacnhanmkmoi.PasswordChar = '*';
                pican1.Visible = false;
                pichien1.Visible = true;
            }
        }
    }
}
