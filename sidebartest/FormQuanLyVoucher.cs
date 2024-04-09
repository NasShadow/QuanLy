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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class FormQuanLyVoucher : Form
    {
        Voucher voucher = new Voucher();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        public FormQuanLyVoucher()
        {
            InitializeComponent();
        }
        DataTable dataTable = new DataTable("Voucher");
        private void FormQuanLyVoucher_Load(object sender, EventArgs e)
        {
            txt_MaVoucher.Enabled = false;
            btn_sua.Enabled = false;
            this.ControlBox = false;
            //Sẽ load lên datagridview
            dataTable.Clear();
            string query = "SELECT Ma_Voucher N'Mã_Voucher', CONCAT(Gia_Tri,'%') as N'Giá_Trị',Ngay_Het_Han N'Ngày_Hết_Hạn',Ngay_Nhan_Voucher N'Ngày_Nhan_Voucher', Ma_Khach_Hang N'Mã_Khách_Hàng' FROM dbo.Voucher";
            SqlDataAdapter adapter = new SqlDataAdapter(query, TKBLL.Load());
            adapter.Fill(dataTable);
            dgv_voucher.DataSource = dataTable;
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {

                voucher.Ma_Voucher = txt_MaVoucher.Text;
                Match match = Regex.Match(txt_GiamGia.Text, @"\d+");
                int number = int.Parse(match.Value);
                if (match.Success)
                {
                    number = int.Parse(match.Value);
                }
                voucher.Gia_Tri = number.ToString();
                string getuser = TKBLL.CheckUpdateVoucher(voucher);

                switch (getuser)
                {
                    case "requeid_khongnhap":
                        {
                            MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_saigiatri":
                        {
                            MessageBox.Show("GIÁ TRỊ CAO NHẤT LÀ 50%!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_saigiatri_2":
                        {
                            MessageBox.Show("GIÁ TRỊ THẤP NHẤT LÀ 1%!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("SỬA THÀNH CÔNG", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormQuanLyVoucher_Load(null, null);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_voucher_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgv_voucher.CurrentRow.Index;
            txt_MaVoucher.Text = dgv_voucher.Rows[i].Cells[0].Value.ToString();
            txt_GiamGia.Text = dgv_voucher.Rows[i].Cells[1].Value.ToString();
            btn_sua.Enabled = true;
        }

        private void txt_GiamGia_TextChanged(object sender, EventArgs e)
        {
            // Nếu TextBox không trống, kiểm tra xem văn bản có kết thúc bằng "%" không
            if (!txt_GiamGia.Text.EndsWith("%"))
            {
                // Nếu không, thêm "%" vào cuối văn bản
                txt_GiamGia.Text += "%";
            }
        }

        private void txt_GiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Ngăn chặn việc ghi ký tự vào TextBox
                e.Handled = true;
            }
        }
    }
}
