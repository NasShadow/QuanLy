using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class FormDangNhapNhanVien : Form
    {
        NhanVien nhanvien = new NhanVien();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();


        public FormDangNhapNhanVien()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        //nút đăng nhập
        private void button2_Click(object sender, EventArgs e)
        {
            nhanvien.Email = txt_email.Text;
            nhanvien.MatKhau = txt_matkhau.Text;

            string check = TKBLL.Check_LoginAdmin(nhanvien);

            switch (check)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                case "Tài khoản mật khẩu đã sai!":
                    {
                        MessageBox.Show("TÀI KHOẢN HOẶC MẬT KHẨU CỦA BẠN ĐÃ SAI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                case "Quan Ly":
                    {
                        MessageBox.Show("ĐĂNG NHẬP THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Hide();
                        FormQuanLy form = new FormQuanLy(txt_email.Text);
                        form.ShowDialog();
                        return;
                    }
                default:
                    {
                        MessageBox.Show("TÀI KHOẢN HOẶC MẬT KHẨU CỦA BẠN ĐÃ SAI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
            }


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void llbl_quenmatkhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            FormTimKiemMatKhauDaMat formTimKiemMatKhauDaMat = new FormTimKiemMatKhauDaMat();
            formTimKiemMatKhauDaMat.ShowDialog();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txt_matkhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pic_an_Click(object sender, EventArgs e)
        {
            if (txt_matkhau.PasswordChar == '\0')
            {
                txt_matkhau.PasswordChar = '*';
                pic_an.Visible = false;
                pic_hien.Visible = true;
            }
        }

        private void pic_hien_Click(object sender, EventArgs e)
        {
            if (txt_matkhau.PasswordChar == '*')
            {
                txt_matkhau.PasswordChar = '\0';
                pic_an.Visible = true;
                pic_hien.Visible = false;
            }
        }
    }
}
