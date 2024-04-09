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
    public partial class FormTimKiemMatKhauDaMat : Form
    {
        NhanVien nhanvien = new NhanVien();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();

        public FormTimKiemMatKhauDaMat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llbl_quenmatkhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            FormDangNhapNhanVien formDangNhapNhanVien = new FormDangNhapNhanVien();
            formDangNhapNhanVien.ShowDialog();
        }

        //Thực hiện tìm kiếm thông qua email
        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            nhanvien.Email = txt_email.Text;

            string get = TKBLL.Find_PassAdmin(nhanvien);

            switch (get)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("VUI LÒNG NHẬP EMAIL!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_email":
                    {
                        MessageBox.Show("EMAIL NHẬP SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "Email của bạn không tồn tại!":
                    {
                        MessageBox.Show("EMAIL KHÔNG TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("TÌM KIẾM THÀNH CÔNG", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_matkhau.Text = TKBLL.Find_PassAdmin(nhanvien);
                        break;
                    }
            }
        }
    }
}
