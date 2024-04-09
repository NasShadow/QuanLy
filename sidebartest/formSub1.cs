using DTO;
using BLL;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sidebartest
{
    public partial class formSub1 : Form
    {
        LoaiHang loaihang = new LoaiHang();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();

        public formSub1()
        {
            InitializeComponent();
        }

        //Thêm loại hàng
        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                loaihang.Ma_loai_hang = txt_maloai.Text;
                loaihang.Ten_loai_hang = txt_tenloai.Text;
                loaihang.Mo_ta = txt_mota.Text;


                string getuser = TKBLL.CheckInsert(loaihang);

                switch (getuser)
                {
                    case "requeid_khongnhap":
                        {
                            MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tensai_1":
                        {
                            MessageBox.Show("TÊN LOẠI KHÔNG ĐƯỢC NHẬP KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tensai_2":
                        {
                            MessageBox.Show("TÊN LOẠI KHÔNG ĐƯỢC NHẬP SỐ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tentrung":
                        {
                            MessageBox.Show("TÊN LOẠI ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("THÊM THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formSub1_Load(null, null);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Sửa loại hàng
        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {

                loaihang.Ma_loai_hang = txt_maloai.Text;
                loaihang.Ten_loai_hang = txt_tenloai.Text;
                loaihang.Mo_ta = txt_mota.Text;


                string getuser = TKBLL.CheckUpdate(loaihang);

                switch (getuser)
                {
                    case "requeid_khongnhap":
                        {
                            MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tensai_1":
                        {
                            MessageBox.Show("TÊN LOẠI KHÔNG ĐƯỢC NHẬP KÝ TỰ ĐẶC BIỆT!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tensai_2":
                        {
                            MessageBox.Show("TÊN LOẠI KHÔNG ĐƯỢC NHẬP SỐ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_tentrung":
                        {
                            MessageBox.Show("TÊN LOẠI ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("SỬA THÀNH CÔNG", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formSub1_Load(null, null);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Xóa loại hàng
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                loaihang.Ma_loai_hang = txt_maloai.Text;

                DialogResult result = MessageBox.Show("BẠN CÓ MUỐN XÓA LOẠI HÀNG NÀY KHÔNG???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    string getuser = TKBLL.CheckDelete(loaihang);

                    switch (getuser)
                    {
                        case "requeid_khongnhap":
                            {
                                MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        case "requeid_KHOANGOAI":
                            {
                                MessageBox.Show("KHÔNG ĐƯỢC XÓA KHÓA NGOẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("XÓA THÀNH CÔNG", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                formSub1_Load(null, null);
                                break;
                            }
                    }
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Tạo hàm khi load form
        private void load_form1()
        {
            btn_luu.Enabled = false;
            btn_sua.Enabled = false;
            btn_xoa.Enabled = false;
            btn_them.Enabled = true;

            //txt
            txt_maloai.Text = "";
            txt_maloai.Enabled = false;
            txt_tenloai.Text = "";
            txt_mota.Text = "";
        }

        private void load_form2()
        {
            btn_luu.Enabled = true;
            btn_sua.Enabled = false;
            btn_them.Enabled = false;
            btn_xoa.Enabled = false;

            //txt
            txt_maloai.Text = "";
            txt_tenloai.Text = "";
            txt_mota.Text = "";
        }

        private void load_form3()
        {
            btn_sua.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = true;
            btn_luu.Enabled = false;
        }


        DataTable dt = new DataTable("LoaiHang");
        private void Showdatagridview_LoaiHang()
        {
            try
            {
                dt.Clear();
                //Sẽ load lên datagridview
                string query = "SELECT Ma_loai_hang N'Mã_Loại_Hàng', Ten_loai_hang N'Tên_Loại_Hàng', Mo_ta N'Mô_tả' FROM dbo.LoaiHang";
                SqlDataAdapter adapter = new SqlDataAdapter(query, TKBLL.Load());
                adapter.Fill(dt);
                dgv_loaihang.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Tìm kiếm datagirdview loại hàng
        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13) // Check for Enter key
                {
                    DataView dv = dt.DefaultView;
                    // Access Text property of txt_search
                    dv.RowFilter = string.Format("Mã_Loại_Hàng LIKE '%{0}%' OR Tên_Loại_Hàng LIKE '%{0}%'", txt_search.Text);
                    dgv_loaihang.DataSource = dv.ToTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Nút thêm
        private void btn_them_Click_1(object sender, EventArgs e)
        {
            load_form2();
        }
        //Form Load loại hàng
        private void formSub1_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            //Hiển thị loại hàng trên datagrid view
            Showdatagridview_LoaiHang();

            //Gọi hàm load form
            load_form1();
        }

        //Datagirdview_Click
        private void dgv_loaihang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgv_loaihang.CurrentRow.Index;

            txt_maloai.Text = dgv_loaihang.Rows[i].Cells[0].Value.ToString();
            txt_tenloai.Text = dgv_loaihang.Rows[i].Cells[1].Value.ToString();
            txt_mota.Text = dgv_loaihang.Rows[i].Cells[2].Value.ToString();

            //Khi ấn vào datagridview sẽ cho phép update
            load_form3();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
        private void txt_search_TextChanged(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
