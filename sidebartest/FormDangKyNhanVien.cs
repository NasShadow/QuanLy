using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;

namespace sidebartest
{
    public partial class FormDangKyNhanVien : Form
    {
        NhanVien nhanvien = new NhanVien();
        NhanVienGiaoHang nhanviengiaohang = new NhanVienGiaoHang();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        public FormDangKyNhanVien()
        {
            InitializeComponent();
            MK_Defaut();
            ComboBoxItems();
        }
        //Hàm gửi mã
        private void Send_OTP_Email()
        {
            string form, to, pass, content;
            form = "kienhttd00367@fpt.edu.vn";
            to = txt_Email.Text;
            pass = "ldjw pdqo tbnj rftk";
            //Nội dung gửi mail
            content = $"THÔNG TIN TÀI KHOẢN NHÂN VIÊN {txt_tenNV.Text}: \r\n Email: {txt_Email.Text} \r\n Mật Khẩu: {txt_MatKhau.Text} \r\n Vai Trò: {cbo_VaiTro.Text}";

            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(form);
            //Tiêu đề cho nội dung gửi
            mail.Subject = $"THÔNG TIN TÀI KHOẢN CỦA NHÂN VIÊN {txt_tenNV.Text}";
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


        private void ComboBoxItems()
        {
            //Vai trò
            cbo_VaiTro.Items.Add("Quan Ly");
            cbo_VaiTro.Items.Add("Nhan Vien");
            cbo_VaiTro.Items.Add("Shipper");
        }
        private void FormDangKyNhanVien_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void btn_DangKy_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ĐĂNG KÝ TÀI KHOẢN NÀY???", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                //Nhân viên bthg
                nhanvien.Ten_NV = txt_tenNV.Text;
                nhanvien.Email = txt_Email.Text;
                nhanvien.MatKhau = txt_MatKhau.Text;
                nhanvien.XacNhanMatKhau = txt_XacNhanMK.Text;

                if (rdo_Nu.Checked)
                {
                    nhanvien.Gioi_Tinh = "0";
                }
                else if (rdo_Nam.Checked)
                {
                    nhanvien.Gioi_Tinh = "1";
                }

                nhanvien.VaiTro = cbo_VaiTro.Text;
                nhanvien.TinhTrang = "1"; //mặc định là đang hoạt động
                nhanvien.DiaChi = txt_DiaChi.Text;


                //Nhân viên giao hàng
                nhanviengiaohang.Ten_NV_GH = txt_tenNV.Text;
                nhanviengiaohang.Email = txt_Email.Text;
                nhanviengiaohang.MatKhau = txt_MatKhau.Text;
                nhanviengiaohang.XacNhanMatKhau = txt_XacNhanMK.Text;

                if (rdo_Nu.Checked)
                {
                    nhanviengiaohang.Gioi_Tinh = "0";
                }
                else if (rdo_Nam.Checked)
                {
                    nhanviengiaohang.Gioi_Tinh = "1";
                }

                nhanviengiaohang.VaiTro = cbo_VaiTro.Text;
                nhanviengiaohang.TinhTrang = "1"; //mặc định là đang hoạt động
                nhanviengiaohang.DiaChi = txt_DiaChi.Text;

                if (cbo_VaiTro.Text == "")
                {
                    MessageBox.Show("VUI LÒNG KHÔNG ĐỂ TRỐNG THÔNG TIN!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Thêm vào bảng nhân viên
                if (cbo_VaiTro.Text == "Nhan Vien" || cbo_VaiTro.Text == "Quan Ly")
                {
                    string getuser = TKBLL.CheckInsert(nhanvien);

                    switch (getuser)
                    {
                        case "requeid_tennhanvien":
                            {
                                MessageBox.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "wrong_name":
                            {
                                MessageBox.Show("VUI LÒNG TÊN CHỈ MẬT CHỮ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email1":
                            {
                                MessageBox.Show("ĐỊA CHỈ EMAIL ĐÃ SAI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email":
                            {
                                MessageBox.Show("EMAIL KHÔNG ĐƯỢC BỎ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau":
                            {
                                MessageBox.Show("MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau1":
                            {
                                MessageBox.Show("MẬT KHẨU PHẢI CÓ ÍT NHẤT 1 KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "select_sex":
                            {
                                MessageBox.Show("VUI LÒNG CHỌN GIỚI TÍNH!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                MessageBox.Show("MẬT KHẨU KHÔNG TRÙNG KHỚP!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "trung_email":
                            {
                                MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "trung_email1":
                            {
                                MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "trung_email2":
                            {
                                MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("THÊM THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Send_OTP_Email();
                                FormDangKyNhanVien_Load(null, null);
                                break;
                            }
                    }
                    return;
                }

                else if (cbo_VaiTro.Text == "Shipper")
                {
                    string getuser1 = TKBLL.Check_InsertNVGH(nhanviengiaohang);

                    switch (getuser1)
                    {
                        case "requeid_tennhanvien":
                            {
                                MessageBox.Show("TÊN NHÂN VIÊN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "wrong_name":
                            {
                                MessageBox.Show("VUI LÒNG TÊN CHỈ MẬT CHỮ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email1":
                            {
                                MessageBox.Show("ĐỊA CHỈ EMAIL ĐÃ SAI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_email":
                            {
                                MessageBox.Show("EMAIL KHÔNG ĐƯỢC BỎ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau":
                            {
                                MessageBox.Show("MẬT KHẨU KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "requeid_matkhau1":
                            {
                                MessageBox.Show("MẬT KHẨU PHẢI CÓ ÍT NHẤT 1 KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "select_sex":
                            {
                                MessageBox.Show("VUI LÒNG CHỌN GIỚI TÍNH!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                MessageBox.Show("MẬT KHẨU KHÔNG TRÙNG KHỚP!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "trung_email":
                            {
                                MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "trung_email1":
                            {
                                MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        case "trung_email2":
                            {
                                MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("THÊM THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Send_OTP_Email();
                                FormDangKyNhanVien_Load(null, null);
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

        private void MK_Defaut()
        {
            txt_MatKhau.Text = "123456789ABCD@";
            txt_XacNhanMK.Text = "123456789ABCD@";
            txt_MatKhau.Enabled = false;
            txt_XacNhanMK.Enabled = false;
        }

    }
}
