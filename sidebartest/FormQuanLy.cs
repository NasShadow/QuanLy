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
    public partial class FormQuanLy : Form
    {
        formDashboard dashboard;
        formSub1 sub1;
        formSubmenu2 sub2;
        formAbout about;
        formSettings settings;
        FormQuanLyKhachHang khachhang;
        FormQuanLyDonHang donhang;
        FormDangKyNhanVien dangkynhanvien;
        FormQuanLyNhanVien quanlynhanvien;
        FormQuanLyShipper quanlyshipper;
        FormQuanLyVoucher quanlyvoucher;
        public FormQuanLy(string email)
        {
            InitializeComponent();

            lbl_email.Text = email;
        }
        bool menuExpand = false;
        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dashboard == null)
            {
                dashboard = new formDashboard();
                dashboard.FormClosed += Dashboard_FormClosed;
                dashboard.MdiParent = this;
                dashboard.Dock = DockStyle.Fill;
                dashboard.Show();
            }
            else
            {
                dashboard.Activate();
            }
        }
        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            dashboard = null;
        }

        private void menu_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        private void submenu1_Click(object sender, EventArgs e)
        {
            if (sub1 == null)
            {
                sub1 = new formSub1();
                sub1.FormClosed += Sub1_FormClosed;
                sub1.MdiParent = this;
                sub1.Dock = DockStyle.Fill;
                sub1.Show();
            }
            else
            {
                sub1.Activate();
            }
        }
        private void Sub1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sub1 = null;
        }

        private void submenu2_Click(object sender, EventArgs e)
        {
            {
                if (sub2 == null)
                {
                    sub2 = new formSubmenu2();
                    sub2.FormClosed += Sub2_FormClosed;
                    sub2.MdiParent = this;
                    sub2.Dock = DockStyle.Fill;
                    sub2.Show();
                }
                else
                {
                    sub2.Activate();
                }
            }
        }
        private void Sub2_FormClosed(object sender, FormClosedEventArgs e)
        {
            sub2 = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {
                if (about == null)
                {
                    about = new formAbout();
                    about.FormClosed += About_FormClosed;
                    about.MdiParent = this;
                    about.Dock = DockStyle.Fill;
                    about.Show();
                }
                else
                {
                    about.Activate();
                }
            }
        }
        private void About_FormClosed(object sender, FormClosedEventArgs e)
        {
            about = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                if (settings == null)
                {
                    settings = new formSettings(lbl_email.Text);
                    settings.FormClosed += Settings_FormClosed;
                    settings.MdiParent = this;
                    settings.Dock = DockStyle.Fill;
                    settings.Show();
                }
                else
                {
                    settings.Activate();
                }
            }
        }
        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            settings = null;
        }
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                MenuContainer.Height += 10;
                //Lon nhat
                if (MenuContainer.Height >= 512)
                {
                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                MenuContainer.Height -= 10;
                if (MenuContainer.Height <= 69)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
        }
        bool sidebarExpand = true;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 65)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                    pnDashboard.Width = sidebar.Width;
                    MenuContainer.Width = sidebar.Width;
                    pnAbout.Width = sidebar.Width;
                    pnSettings.Width = sidebar.Width;
                    pnLogout.Width = sidebar.Width;
                }
            }
            else
            {
                sidebar.Width += 10;
                //
                if (sidebar.Width >= 224)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                    pnDashboard.Width = sidebar.Width;
                    MenuContainer.Width = sidebar.Width;
                    pnAbout.Width = sidebar.Width;
                    pnSettings.Width = sidebar.Width;
                    pnLogout.Width = sidebar.Width;
                }
            }
        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("BẠN MUỐN ĐĂNG XUẤT CHỨ???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
        
            if(result == DialogResult.Yes)
            {
                Hide();
                FormDangNhapNhanVien formDangNhapNhanVien = new FormDangNhapNhanVien();
                formDangNhapNhanVien.ShowDialog();
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private void FormQuanLy_Load(object sender, EventArgs e)
        {

        }

        //KhachHang
        private void button3_Click(object sender, EventArgs e)
        {
            {
                if (khachhang == null)
                {
                    khachhang = new FormQuanLyKhachHang();
                    khachhang.FormClosed += khachhang_FormClosed;
                    khachhang.MdiParent = this;
                    khachhang.Dock = DockStyle.Fill;
                    khachhang.Show();
                }
                else
                {
                    khachhang.Activate();
                }
            }
        }

        private void khachhang_FormClosed(object sender, FormClosedEventArgs e)
        {
            khachhang = null;
        }

        //DonHang
        private void button4_Click_1(object sender, EventArgs e)
        {
            {
                if (donhang == null)
                {
                    donhang = new FormQuanLyDonHang();
                    donhang.FormClosed += donhang_FormClosed;
                    donhang.MdiParent = this;
                    donhang.Dock = DockStyle.Fill;
                    donhang.Show();
                }
                else
                {
                    donhang.Activate();
                }
            }
        }

        private void donhang_FormClosed(object sender, FormClosedEventArgs e)
        {
            donhang = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            {
                if (dangkynhanvien == null)
                {
                    dangkynhanvien = new FormDangKyNhanVien();
                    dangkynhanvien.FormClosed += dangkynhanvien_FormClosed;
                    dangkynhanvien.MdiParent = this;
                    dangkynhanvien.Dock = DockStyle.Fill;
                    dangkynhanvien.Show();
                }
                else
                {
                    dangkynhanvien.Activate();
                }
            }
        }

        private void dangkynhanvien_FormClosed(object sender, FormClosedEventArgs e)
        {
            dangkynhanvien = null;
        }

        //Quản lý nhân viên
        private void button8_Click(object sender, EventArgs e)
        {
            {
                if (quanlynhanvien == null)
                {
                    quanlynhanvien = new FormQuanLyNhanVien();
                    quanlynhanvien.FormClosed += quanlynhanvien_FormClosed;
                    quanlynhanvien.MdiParent = this;
                    quanlynhanvien.Dock = DockStyle.Fill;
                    quanlynhanvien.Show();
                }
                else
                {
                    quanlynhanvien.Activate();
                }
            }
        }

        private void quanlynhanvien_FormClosed(object sender, FormClosedEventArgs e)
        {
            quanlynhanvien = null;
        }

        //shipper
        private void button9_Click(object sender, EventArgs e)
        {
            {
                if (quanlyshipper == null)
                {
                    quanlyshipper = new FormQuanLyShipper();
                    quanlyshipper.FormClosed += quanlyshipper_FormClosed;
                    quanlyshipper.MdiParent = this;
                    quanlyshipper.Dock = DockStyle.Fill;
                    quanlyshipper.Show();
                }
                else
                {
                    quanlyshipper.Activate();
                }
            }
        }

        private void quanlyshipper_FormClosed(object sender, FormClosedEventArgs e)
        {
            quanlyshipper = null;
        }

        private void btn_Voucher_Click(object sender, EventArgs e)
        {
            {
                if (quanlyvoucher == null)
                {
                    quanlyvoucher = new FormQuanLyVoucher();
                    quanlyvoucher.FormClosed += quanlyvoucher_FormClosed;
                    quanlyvoucher.MdiParent = this;
                    quanlyvoucher.Dock = DockStyle.Fill;
                    quanlyvoucher.Show();
                }
                else
                {
                    quanlyvoucher.Activate();
                }
            }
        }

        private void quanlyvoucher_FormClosed(object sender, FormClosedEventArgs e)
        {
            quanlyvoucher = null;
        }

        /*private void button4_Click_1(object sender, EventArgs e)
        {

        }*/
    }
}
