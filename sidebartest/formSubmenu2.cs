using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class formSubmenu2 : Form
    {
        static LoaiHang loaihang = new LoaiHang();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();

        //Lấy gtri trong Table
        static Db db = new Db();

        //static string strConnectionInfo = db.strConnection;
        static DataContext dc = new DataContext(db.strConnection);

        //Table
        static Table<LoaiHang> LoaiHang = dc.GetTable<LoaiHang>();
        static Table<SanPham> SanPham = dc.GetTable<SanPham>();
        public formSubmenu2()
        {
            InitializeComponent();
        }

        private void formSubmenu2_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;


            //Load giao diện
            LoadDataIntoPanel();
        }


        //Hàm load lên giao diện
        static Guna2GradientPanel productPanel = new Guna2GradientPanel();
        static FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
        private void LoadDataIntoPanel()
        {

            try
            {
                conn.Open();

                //Thẻ cha lớn nhất
                //Guna2GradientPanel productPanel = new Guna2GradientPanel();
                //productPanel.Size = new System.Drawing.Size(250, 250);
                productPanel.FillColor = Color.FromArgb(224, 224, 224);
                productPanel.FillColor2 = Color.FromArgb(224, 224, 224);
                productPanel.Dock = DockStyle.Fill;
                productPanel.Padding = new Padding(60);
                productPanel.Update();

                //Thẻ cha chứa tất cả sản phẩm
                Guna2GradientPanel productPanel_1 = new Guna2GradientPanel();
                productPanel_1.Width = 2500;
                productPanel_1.BackColor = Color.Transparent;
                productPanel_1.FillColor = Color.White;
                productPanel_1.FillColor2 = Color.White;
                productPanel_1.BorderRadius = 50;
                productPanel_1.Dock = DockStyle.Fill;
                productPanel_1.Padding = new Padding(40);
                productPanel_1.AutoSize = false;
                productPanel_1.Update();

                //FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                //Phần tử con chứa sản phẩm
                flowLayoutPanel.AutoScroll = true;
                //flowLayoutPanel.BackColor = Color.Blue;
                flowLayoutPanel.Dock = DockStyle.Fill;
                //flowLayoutPanel.Width = productPanel_1.Width / 2;
                flowLayoutPanel.Update();

                //Phần tử con chứa những btn sản phẩm
                Guna2GradientPanel productPanel_2 = new Guna2GradientPanel();
                //productPanel_2.FillColor = Color.Red;
                //productPanel_2.FillColor2 = Color.Red;
                productPanel_2.Dock = DockStyle.Top;
                //.Width = productPanel_1.Width / 8;
                productPanel_2.Update();

                //Tạo khối chứa khối search
                Guna2GradientPanel productPanel_txtsearch = new Guna2GradientPanel();
                productPanel_txtsearch.Width = 300;
                //productPanel_txtsearch.FillColor = Color.Brown;
                //productPanel_txtsearch.FillColor2 = Color.Brown;
                productPanel_txtsearch.Dock = DockStyle.Left;
                productPanel_txtsearch.Update();

                //TextBox_Search nằm trong
                Guna2TextBox txt_searchspham = new Guna2TextBox();
                txt_searchspham.Width = 300;
                txt_searchspham.BackColor = Color.Black;
                txt_searchspham.PlaceholderText = "Search...";
                txt_searchspham.Size = new System.Drawing.Size(255, 44);
                txt_searchspham.Font = new Font("SVN CINTRA", 10, FontStyle.Regular);
                int verticalPosition = (productPanel_txtsearch.Height - txt_searchspham.Height) / 2;
                txt_searchspham.Location = new Point(0, verticalPosition);
                txt_searchspham.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material; // Hoặc Guna.UI2.WinForms.Enums.TextBoxStyle.Flat
                txt_searchspham.Update();

                //Tạo khối chứa khối button
                Guna2GradientPanel productPanel_btnthem = new Guna2GradientPanel();
                productPanel_btnthem.Width = 180;
                //productPanel_btnthem.FillColor = Color.Brown;
                //productPanel_btnthem.FillColor2 = Color.Brown;
                productPanel_btnthem.Dock = DockStyle.Right;
                productPanel_btnthem.Update();


                //Tạo button
                Guna2Button btn_search = new Guna2Button();
                btn_search.Text = "THÊM";
                btn_search.Font = new Font("SVN-Cintra", 18, FontStyle.Regular);
                btn_search.FillColor = Color.FromArgb(64, 64, 64);
                btn_search.BorderRadius = 5;
                btn_search.Width = 180;
                btn_search.Height = 50;
                btn_search.Anchor = AnchorStyles.None;
                int verticalPosition_1 = (productPanel_btnthem.Height - btn_search.Height) / 2;
                btn_search.Location = new Point(0, verticalPosition_1);
                btn_search.Click += MyButtonClickHandler;
                btn_search.Update();


                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SanPham", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();


                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string MaSanPham = reader["Ma_SP"].ToString();
                    string TenSanPham = reader["Ten_SP"].ToString();
                    string SoLuong = reader["So_luong"].ToString();
                    string HinhAnh = reader["FileNames"].ToString();
                    string GiaNhap = reader["Gia_nhap"].ToString();
                    string GiaBan = reader["Gia_ban"].ToString();


                    //Thêm pannel = div
                    Guna2GradientPanel panel_SP = new Guna2GradientPanel();
                    panel_SP.Size = new System.Drawing.Size(270, 270);
                    panel_SP.FillColor = Color.FromArgb(224, 224, 224); ;
                    panel_SP.FillColor2 = Color.FromArgb(224, 224, 224); ;
                    panel_SP.BorderRadius = 10;
                    panel_SP.Update();

                    Guna2GradientPanel pannel_T1 = new Guna2GradientPanel();
                    pannel_T1.Height = 40;
                    //pannel_T1.FillColor = Color.Blue;
                    //pannel_T1.FillColor2 = Color.Blue;
                    pannel_T1.BorderRadius = 10;
                    pannel_T1.Dock = DockStyle.Top;
                    pannel_T1.Update();


                    Label lbl_MaSanPham = new Label();
                    lbl_MaSanPham.Width = 100;
                    lbl_MaSanPham.Text = MaSanPham;
                    //lbl_MaSanPham.BackColor = Color.Brown;
                    lbl_MaSanPham.Dock = DockStyle.Left;
                    lbl_MaSanPham.TextAlign = ContentAlignment.MiddleLeft;
                    lbl_MaSanPham.Padding = new Padding(10, 0, 10, 0);
                    lbl_MaSanPham.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                    lbl_MaSanPham.Update();

                    Label lbl_SoLuong = new Label();
                    lbl_SoLuong.Width = 100;
                    lbl_SoLuong.Text = "Số lượng " + SoLuong;
                    //lbl_SoLuong.BackColor = Color.Brown;
                    lbl_SoLuong.Dock = DockStyle.Right;
                    lbl_SoLuong.TextAlign = ContentAlignment.MiddleCenter;
                    lbl_SoLuong.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                    lbl_SoLuong.Update();

                    //Tầng 2
                    Guna2GradientPanel pannel_T2 = new Guna2GradientPanel();
                    pannel_T2.Height = 160;
                    //pannel_T2.FillColor = Color.Red;
                    //pannel_T2.FillColor2 = Color.Red;
                    pannel_T2.Dock = DockStyle.Top;
                    pannel_T2.Padding = new Padding(10, 0, 10, 0);
                    pannel_T2.Update();

                    Guna2PictureBox pic_sanpham = new Guna2PictureBox();
                    pic_sanpham.Height = 10;
                    pic_sanpham.Image = new Bitmap($@"{HinhAnh}");
                    pic_sanpham.SizeMode = PictureBoxSizeMode.Zoom;
                    pic_sanpham.Dock = DockStyle.Fill;
                    pic_sanpham.BorderRadius = 5;
                    pic_sanpham.Update();

                    //Tầng 3
                    Guna2GradientPanel pannel_T3 = new Guna2GradientPanel();
                    pannel_T3.Height = 35;
                    //pannel_T3.FillColor = Color.Black;
                    //pannel_T3.FillColor2 = Color.Black;
                    pannel_T3.Dock = DockStyle.Fill;
                    pannel_T3.Update();

                    //Tạo button
                    Guna2Button btn_dathang = new Guna2Button();
                    btn_dathang.AutoSize = true;
                    btn_dathang.Text = "ĐẶT HÀNG";
                    btn_dathang.Font = new Font("SVN-Cintra", 13, FontStyle.Regular);
                    btn_dathang.FillColor = Color.FromArgb(64, 64, 64);
                    btn_dathang.BorderRadius = 5;
                    btn_dathang.Location = new Point(80, 0);
                    //btn_dathang.Click += DatHangClick;
                    // Thêm tham số cho sự kiện Click
                    btn_dathang.Click += (s, e) => DatHangClick(s, e, MaSanPham, TenSanPham, HinhAnh, GiaNhap, GiaBan, SoLuong);
                    btn_dathang.Update();

                    //Tầng 4
                    Guna2GradientPanel pannel_T4 = new Guna2GradientPanel();
                    pannel_T4.Height = 25;
                    //pannel_T4.FillColor = Color.Yellow;
                    //pannel_T4.FillColor2 = Color.Yellow;
                    pannel_T4.Dock = DockStyle.Top;
                    pannel_T4.Padding = new Padding(8, 0, 8, 0);
                    pannel_T4.Update();

                    Label lbl_TenSanPham = new Label();
                    //lbl_TenSanPham.Width = 100;
                    lbl_TenSanPham.Text = TenSanPham;
                    //lbl_MaSanPham.BackColor = Color.Brown;
                    lbl_TenSanPham.Dock = DockStyle.Fill;
                    lbl_TenSanPham.TextAlign = ContentAlignment.MiddleCenter;
                    lbl_TenSanPham.Font = new Font("Times New Roman", 13, FontStyle.Bold);
                    lbl_TenSanPham.Update();

                    //Thêm sản phẩm vào layout
                    flowLayoutPanel.Controls.Add(panel_SP);

                    //Trong từng khối chứa sản phẩm sẽ hiển thị thông tin sản phẩm
                    panel_SP.Controls.Add(pannel_T3);
                    panel_SP.Controls.Add(pannel_T4);
                    panel_SP.Controls.Add(pannel_T2);
                    panel_SP.Controls.Add(pannel_T1);
                    //T1 add label vào
                    pannel_T1.Controls.Add(lbl_MaSanPham);
                    pannel_T1.Controls.Add(lbl_SoLuong);
                    pannel_T2.Controls.Add(pic_sanpham);
                    pannel_T3.Controls.Add(btn_dathang);
                    pannel_T4.Controls.Add(lbl_TenSanPham);
                }

                //MessageBox.Show(productPanel_1.Width.ToString());

                //thẻ cha sẽ add thẻ con nằm bên trong
                productPanel.Controls.Add(productPanel_1);

                //2 thẻ con được thẻ cha add vào
                productPanel_1.Controls.Add(flowLayoutPanel);
                productPanel_1.Controls.Add(productPanel_2);

                //Thẻ cha của thẻ search
                productPanel_txtsearch.Controls.Add(txt_searchspham);
                productPanel_btnthem.Controls.Add(btn_search);

                //Thẻ pannel chứa textbox cha của textbox search
                productPanel_2.Controls.Add(productPanel_txtsearch);
                productPanel_2.Controls.Add(productPanel_btnthem);

                //Winfrom sẽ thêm cái phần tử cha vào
                this.Controls.Add(productPanel);

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

        //search
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            //Load_Search();
        }
        //Khi ấn enter sẽ thực hiện câu lệnh
        private void txt_Searchtest_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    using (SqlConnection conn = TKBLL.Load())
                    {
                        conn.Open();

                        //Thẻ cha lớn nhất
                        //Guna2GradientPanel productPanel = new Guna2GradientPanel();
                        //productPanel.Size = new System.Drawing.Size(250, 250);
                        productPanel.FillColor = Color.FromArgb(224, 224, 224);
                        productPanel.FillColor2 = Color.FromArgb(224, 224, 224);
                        productPanel.Dock = DockStyle.Fill;
                        productPanel.Padding = new Padding(60);
                        productPanel.Update();

                        //Thẻ cha chứa tất cả sản phẩm
                        Guna2GradientPanel productPanel_1 = new Guna2GradientPanel();
                        productPanel_1.Width = 2500;
                        productPanel_1.BackColor = Color.Transparent;
                        productPanel_1.FillColor = Color.White;
                        productPanel_1.FillColor2 = Color.White;
                        productPanel_1.BorderRadius = 50;
                        productPanel_1.Dock = DockStyle.Fill;
                        productPanel_1.Padding = new Padding(40);
                        productPanel_1.AutoSize = false;
                        productPanel_1.Update();

                        //FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                        //Phần tử con chứa sản phẩm
                        flowLayoutPanel.AutoScroll = true;
                        //flowLayoutPanel.BackColor = Color.Blue;
                        flowLayoutPanel.Dock = DockStyle.Fill;
                        //flowLayoutPanel.Width = productPanel_1.Width / 2;
                        flowLayoutPanel.Update();

                        //Phần tử con chứa những btn sản phẩm
                        Guna2GradientPanel productPanel_2 = new Guna2GradientPanel();
                        //productPanel_2.FillColor = Color.Red;
                        //productPanel_2.FillColor2 = Color.Red;
                        productPanel_2.Dock = DockStyle.Top;
                        //.Width = productPanel_1.Width / 8;
                        productPanel_2.Update();

                        //Tạo khối chứa khối search
                        Guna2GradientPanel productPanel_txtsearch = new Guna2GradientPanel();
                        productPanel_txtsearch.Width = 300;
                        //productPanel_txtsearch.FillColor = Color.Brown;
                        //productPanel_txtsearch.FillColor2 = Color.Brown;
                        productPanel_txtsearch.Dock = DockStyle.Left;
                        productPanel_txtsearch.Update();

                        //TextBox_Search nằm trong
                        Guna2TextBox txt_searchspham = new Guna2TextBox();
                        txt_searchspham.Width = 300;
                        txt_searchspham.BackColor = Color.Black;
                        txt_searchspham.PlaceholderText = "Search...";
                        txt_searchspham.Size = new System.Drawing.Size(255, 44);
                        txt_searchspham.Font = new Font("SVN CINTRA", 10, FontStyle.Regular);
                        int verticalPosition = (productPanel_txtsearch.Height - txt_searchspham.Height) / 2;
                        txt_searchspham.Location = new Point(0, verticalPosition);
                        txt_searchspham.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material; // Hoặc Guna.UI2.WinForms.Enums.TextBoxStyle.Flat
                        txt_searchspham.Visible = false;
                        txt_searchspham.Update();

                        //Tạo khối chứa khối button
                        Guna2GradientPanel productPanel_btnthem = new Guna2GradientPanel();
                        productPanel_btnthem.Width = 180;
                        //productPanel_btnthem.FillColor = Color.Brown;
                        //productPanel_btnthem.FillColor2 = Color.Brown;
                        productPanel_btnthem.Dock = DockStyle.Right;
                        productPanel_btnthem.Update();


                        //Tạo button
                        Guna2Button btn_search = new Guna2Button();
                        btn_search.Text = "THÊM";
                        btn_search.Font = new Font("SVN-Cintra", 18, FontStyle.Regular);
                        btn_search.FillColor = Color.FromArgb(64, 64, 64);
                        btn_search.BorderRadius = 5;
                        btn_search.Width = 180;
                        btn_search.Height = 50;
                        btn_search.Anchor = AnchorStyles.None;
                        int verticalPosition_1 = (productPanel_btnthem.Height - btn_search.Height) / 2;
                        btn_search.Location = new Point(0, verticalPosition_1);
                        btn_search.Click += MyButtonClickHandler;
                        btn_search.Update();


                        //Tạo vòng lặp và lấy dữ liệu từ csdl
                        //string query = "SELECT Ma_SP N'Mã_Sản_Phẩm', LoaiHang.Ma_loai_hang N'Mã_Loại_Hàng', Ten_SP N'Tên_Sản_phẩm', Ten_loai_hang N'Tên_Loại_Hàng', So_luong N'Số_Lượng', Gia_nhap N'Giá_Nhập', Gia_ban N'Giá_Bán', FileNames N'File Names'\r\nFROM dbo.LoaiHang\r\nJOIN dbo.SanPham ON SanPham.Ma_loai_hang = LoaiHang.Ma_loai_hang";
                        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SanPham", conn);
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {

                            bool check = false;
                            while (reader.Read())
                            {
                                string searchText = txt_Searchtest.Text;
                                // Lấy giá trị từ cột "Ma_loai_hang"
                                string MaSanPham = reader["Ma_SP"].ToString();
                                string TenSanPham = reader["Ten_SP"].ToString();
                                string SoLuong = reader["So_luong"].ToString();
                                string HinhAnh = reader["FileNames"].ToString();
                                string GiaNhap = reader["Gia_nhap"].ToString();
                                string GiaBan = reader["Gia_ban"].ToString();
                                //Tìm kiếm gần đúng
                                string maSanPhamAsString = MaSanPham.ToString();
                                string tenSanPhamAsString = TenSanPham.ToString();

                                if (searchText.IndexOf(maSanPhamAsString, StringComparison.OrdinalIgnoreCase) >= 0 || searchText.IndexOf(tenSanPhamAsString, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    productPanel.Controls.Clear();
                                    flowLayoutPanel.Controls.Clear();
                                    check = true;
                                    //Thêm pannel = div
                                    Guna2GradientPanel panel_SP = new Guna2GradientPanel();
                                    panel_SP.Size = new System.Drawing.Size(270, 270);
                                    panel_SP.FillColor = Color.FromArgb(224, 224, 224); ;
                                    panel_SP.FillColor2 = Color.FromArgb(224, 224, 224); ;
                                    panel_SP.BorderRadius = 10;
                                    panel_SP.Update();

                                    Guna2GradientPanel pannel_T1 = new Guna2GradientPanel();
                                    pannel_T1.Height = 40;
                                    //pannel_T1.FillColor = Color.Blue;
                                    //pannel_T1.FillColor2 = Color.Blue;
                                    pannel_T1.BorderRadius = 10;
                                    pannel_T1.Dock = DockStyle.Top;
                                    pannel_T1.Update();


                                    Label lbl_MaSanPham = new Label();
                                    lbl_MaSanPham.Width = 100;
                                    lbl_MaSanPham.Text = MaSanPham;
                                    //lbl_MaSanPham.BackColor = Color.Brown;
                                    lbl_MaSanPham.Dock = DockStyle.Left;
                                    lbl_MaSanPham.TextAlign = ContentAlignment.MiddleLeft;
                                    lbl_MaSanPham.Padding = new Padding(10, 0, 10, 0);
                                    lbl_MaSanPham.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    lbl_MaSanPham.Update();

                                    Label lbl_SoLuong = new Label();
                                    lbl_SoLuong.Width = 100;
                                    lbl_SoLuong.Text = "Số lượng " + SoLuong;
                                    //lbl_SoLuong.BackColor = Color.Brown;
                                    lbl_SoLuong.Dock = DockStyle.Right;
                                    lbl_SoLuong.TextAlign = ContentAlignment.MiddleCenter;
                                    lbl_SoLuong.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                                    lbl_SoLuong.Update();

                                    //Tầng 2
                                    Guna2GradientPanel pannel_T2 = new Guna2GradientPanel();
                                    pannel_T2.Height = 160;
                                    //pannel_T2.FillColor = Color.Red;
                                    //pannel_T2.FillColor2 = Color.Red;
                                    pannel_T2.Dock = DockStyle.Top;
                                    pannel_T2.Padding = new Padding(10, 0, 10, 0);
                                    pannel_T2.Update();

                                    Guna2PictureBox pic_sanpham = new Guna2PictureBox();
                                    pic_sanpham.Height = 10;
                                    pic_sanpham.Image = new Bitmap($@"{HinhAnh}");
                                    pic_sanpham.SizeMode = PictureBoxSizeMode.Zoom;
                                    pic_sanpham.Dock = DockStyle.Fill;
                                    pic_sanpham.BorderRadius = 5;
                                    pic_sanpham.Update();

                                    //Tầng 3
                                    Guna2GradientPanel pannel_T3 = new Guna2GradientPanel();
                                    pannel_T3.Height = 35;
                                    //pannel_T3.FillColor = Color.Black;
                                    //pannel_T3.FillColor2 = Color.Black;
                                    pannel_T3.Dock = DockStyle.Fill;
                                    pannel_T3.Update();

                                    //Tạo button
                                    Guna2Button btn_dathang = new Guna2Button();
                                    btn_dathang.AutoSize = true;
                                    btn_dathang.Text = "ĐẶT HÀNG";
                                    btn_dathang.Font = new Font("SVN-Cintra", 13, FontStyle.Regular);
                                    btn_dathang.FillColor = Color.FromArgb(64, 64, 64);
                                    btn_dathang.BorderRadius = 5;
                                    btn_dathang.Location = new Point(80, 0);
                                    btn_dathang.Click += (s, f) => DatHangClick(s, f, MaSanPham, TenSanPham, HinhAnh, GiaNhap, GiaBan, SoLuong);
                                    btn_dathang.Update();

                                    //Tầng 4
                                    Guna2GradientPanel pannel_T4 = new Guna2GradientPanel();
                                    pannel_T4.Height = 25;
                                    //pannel_T4.FillColor = Color.Yellow;
                                    //pannel_T4.FillColor2 = Color.Yellow;
                                    pannel_T4.Dock = DockStyle.Top;
                                    pannel_T4.Padding = new Padding(8, 0, 8, 0);
                                    pannel_T4.Update();

                                    Label lbl_TenSanPham = new Label();
                                    //lbl_TenSanPham.Width = 100;
                                    lbl_TenSanPham.Text = TenSanPham;
                                    //lbl_MaSanPham.BackColor = Color.Brown;
                                    lbl_TenSanPham.Dock = DockStyle.Fill;
                                    lbl_TenSanPham.TextAlign = ContentAlignment.MiddleCenter;
                                    lbl_TenSanPham.Font = new Font("Times New Roman", 13, FontStyle.Bold);
                                    lbl_TenSanPham.Update();

                                    //Thêm sản phẩm vào layout
                                    flowLayoutPanel.Controls.Add(panel_SP);

                                    //Trong từng khối chứa sản phẩm sẽ hiển thị thông tin sản phẩm
                                    panel_SP.Controls.Add(pannel_T3);
                                    panel_SP.Controls.Add(pannel_T4);
                                    panel_SP.Controls.Add(pannel_T2);
                                    panel_SP.Controls.Add(pannel_T1);
                                    //T1 add label vào
                                    pannel_T1.Controls.Add(lbl_MaSanPham);
                                    pannel_T1.Controls.Add(lbl_SoLuong);
                                    pannel_T2.Controls.Add(pic_sanpham);
                                    pannel_T3.Controls.Add(btn_dathang);
                                    pannel_T4.Controls.Add(lbl_TenSanPham);

                                    //MessageBox.Show(productPanel_1.Width.ToString());

                                    //thẻ cha sẽ add thẻ con nằm bên trong
                                    productPanel.Controls.Add(productPanel_1);

                                    //2 thẻ con được thẻ cha add vào
                                    productPanel_1.Controls.Add(flowLayoutPanel);
                                    productPanel_1.Controls.Add(productPanel_2);

                                    //Thẻ cha của thẻ search
                                    productPanel_txtsearch.Controls.Add(txt_searchspham);
                                    productPanel_btnthem.Controls.Add(btn_search);

                                    //Thẻ pannel chứa textbox cha của textbox search
                                    productPanel_2.Controls.Add(productPanel_txtsearch);
                                    productPanel_2.Controls.Add(productPanel_btnthem);

                                    //Winfrom sẽ thêm cái phần tử cha vào
                                    this.Controls.Add(productPanel);
                                    break;
                                }
                            }
                            if (check == false)
                            {
                                productPanel.Controls.Clear();
                                flowLayoutPanel.Controls.Clear();
                                LoadDataIntoPanel();
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

                // Thực thi các câu lệnh của bạn ở đây
                e.Handled = true; // Ngăn chặn ký tự Enter được hiển thị trong TextBox
            }
        }

        string masp = "";
        string tensp = "";
        string hinhanh = "";
        string gianhap = "";
        string giaban = "";
        string soluong = "";


        private void DatHangClick(object sender, EventArgs e, string maSanPham, string tenSanPham, string hinhanhSanPham, string gianhapSanPham, string giabanSanPham, string soluongSanPham)
        {

            masp = maSanPham;
            tensp = tenSanPham;
            hinhanh = hinhanhSanPham;
            gianhap = gianhapSanPham;
            giaban = giabanSanPham;
            soluong = soluongSanPham;

            FormQuanLyDanhGia form = new FormQuanLyDanhGia(masp, tensp, hinhanh, gianhap, giaban, soluong);
            form.ShowDialog();
        }



        private static void MyButtonClickHandler(object sender, EventArgs e)
        {
            
            formCRUDsanpham form = new formCRUDsanpham();
            form.ShowDialog();
        }

        private void SomeMethod()
        {
            this.Refresh();
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            productPanel.Controls.Clear();
            flowLayoutPanel.Controls.Clear();
            LoadDataIntoPanel();
        }
        
    }
}
