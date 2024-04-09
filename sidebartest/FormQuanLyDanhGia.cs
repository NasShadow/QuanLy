using BLL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sidebartest
{
    public partial class FormQuanLyDanhGia : Form
    {
        public string Masp { get; set; }
        public string Tensp { get; set; }
        public string Hinhanh { get; set; }
        public string Gianhap { get; set; }
        public string Giaban { get; set; }
        public string Soluong { get; set; }

        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();


        public FormQuanLyDanhGia(string masp, string tensp, string hinhanh, string gianhap, string giaban, string soluong)
        {
            InitializeComponent();

            // Gán giá trị vào các thuộc tính của form 2
            Masp = masp;
            Tensp = tensp;
            Hinhanh = hinhanh;
            Gianhap = gianhap;
            Giaban = giaban;
            Soluong = soluong;

            lbl_tensanpham.Text = tensp;
            lbl_soluongsp.Text = "Số Lượng: " + soluong;

            double gianhapValue = double.Parse(gianhap) + 1000000;
            lbl_gianhap.Text = "Giá gốc: " + gianhapValue.ToString("C0");

            double giabanValue = double.Parse(giaban) + 0;
            lbl_giaban.Text = "Giá KM: " + giabanValue.ToString("C0");
            pic_spham.Image = new Bitmap($@"{Hinhanh}");
        }

        private void FormQuanLyDanhGia_Load(object sender, EventArgs e)
        {
            //Load giao diện comment
            Load_DanhGia();
        }


        //Hàm load giao diện comment
        private void Load_DanhGia()
        {
            try
            {
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT dbo.KhachHang.Ten_Khach_Hang, dbo.DanhGia.Like_Dislike, dbo.KhachHang.FileNames, dbo.DanhGia.Noi_Dung, dbo.DanhGia.Ma_Khach_Hang, dbo.DanhGia.Ma_SP\r\nFROM dbo.SanPham \r\nJOIN dbo.DanhGia ON DanhGia.Ma_SP = SanPham.Ma_SP\r\nJOIN dbo.KhachHang ON KhachHang.Ma_Khach_Hang = DanhGia.Ma_Khach_Hang\r\nWHERE DanhGia.Ma_SP = N'{this.Masp}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string LikeDislike = reader["Like_Dislike"].ToString();
                    string TenKhachHang = reader["Ten_Khach_Hang"].ToString();
                    string NoiDung = reader["Noi_Dung"].ToString();
                    string MaKhachHang = reader["Ma_Khach_Hang"].ToString();
                    string MaSanPham = reader["Ma_SP"].ToString();
                    string HinhAnh = reader["FileNames"].ToString();

                    if (LikeDislike == "True")
                    {
                        LikeDislike = "Hài Lòng";
                    }
                    else if (LikeDislike == "True")
                    {
                        LikeDislike = "Không Hài Lòng";
                    }

                    //Thêm pannel = div
                    Guna2GradientPanel panel_CMT = new Guna2GradientPanel();
                    panel_CMT.Size = new System.Drawing.Size(670, 120);
                    panel_CMT.FillColor = Color.FromArgb(224, 224, 224);
                    panel_CMT.FillColor2 = Color.FromArgb(224, 224, 224);
                    panel_CMT.BorderRadius = 10;
                    panel_CMT.Update();

                    //Cha chứa ảnh
                    Guna2GradientPanel panel_IMG = new Guna2GradientPanel();
                    //panel_IMG.FillColor = Color.Red;
                    //panel_IMG.FillColor2 = Color.Red;
                    panel_IMG.Dock = DockStyle.Left;
                    panel_IMG.BorderRadius = 10;
                    panel_IMG.Size = new System.Drawing.Size(120, 120);
                    panel_IMG.Padding = new Padding(10);
                    panel_IMG.Update();

                    //Thẻ img
                    Guna2CirclePictureBox pic_cmt = new Guna2CirclePictureBox();
                    pic_cmt.Image = new Bitmap($@"{HinhAnh}");
                    pic_cmt.Dock = DockStyle.Fill;
                    //pic_cmt.FillColor = Color.AliceBlue;
                    pic_cmt.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic_cmt.Update();

                    //Thẻ chữ chứa tên độc giả và đánh giá
                    Guna2GradientPanel panel_Name = new Guna2GradientPanel();
                    //panel_Name.FillColor = Color.Blue;
                    //panel_Name.FillColor2 = Color.Blue;
                    panel_Name.Dock = DockStyle.Fill;
                    panel_Name.BorderRadius = 10;
                    panel_Name.Update();

                    Guna2GradientPanel panel_childname = new Guna2GradientPanel();
                    //panel_childname.FillColor = Color.Yellow;
                    //panel_childname.FillColor2 = Color.Yellow;
                    panel_childname.Dock = DockStyle.Top;
                    panel_childname.BorderRadius = 10;
                    panel_childname.Size = new System.Drawing.Size(60, 60);
                    panel_childname.Update();

                    Guna2HtmlLabel lbl_Hoten_DanhGia = new Guna2HtmlLabel();
                    lbl_Hoten_DanhGia.Text = TenKhachHang + " | " + LikeDislike;
                    //lbl_Hoten_DanhGia.BackColor = Color.Brown;
                    lbl_Hoten_DanhGia.Font = new Font("SVN-Cintra", 15, FontStyle.Regular);
                    //lbl_Hoten_DanhGia.TextAlignment = ContentAlignment.MiddleLeft;
                    // Đặt vị trí của label để căn giữa dọc
                    int verticalLocation = (panel_childname.Height - lbl_Hoten_DanhGia.Height) / 2;
                    lbl_Hoten_DanhGia.Location = new Point(0, verticalLocation);
                    lbl_Hoten_DanhGia.Update();


                    Guna2GradientPanel panel_noidung = new Guna2GradientPanel();
                    //panel_noidung.FillColor = Color.Yellow;
                    //panel_noidung.FillColor2 = Color.Yellow;
                    panel_noidung.Dock = DockStyle.Top;
                    panel_noidung.BorderRadius = 10;
                    panel_noidung.Size = new System.Drawing.Size(60, 60);
                    //panel_noidung.AutoSize = true;
                    panel_noidung.Update();

                    Guna2HtmlLabel lbl_comment = new Guna2HtmlLabel();
                    lbl_comment.Text = NoiDung;
                    //lbl_comment.BackColor = Color.Brown;
                    lbl_comment.Font = new Font("JetBrains Mono NL", 12, FontStyle.Regular);
                    lbl_comment.AutoSize = true;
                    // Đặt vị trí của label để căn giữa dọc
                    int verticalLocation1 = (panel_noidung.Height - lbl_comment.Height) / 2;
                    lbl_comment.Location = new Point(0, verticalLocation);
                    lbl_comment.Update();


                    panel_noidung.Controls.Add(lbl_comment);

                    panel_childname.Controls.Add(lbl_Hoten_DanhGia);

                    //
                    panel_Name.Controls.Add(panel_noidung);

                    panel_Name.Controls.Add(panel_childname);

                    //Pannel img add img
                    panel_IMG.Controls.Add(pic_cmt);

                    //Pannel cha add con vào
                    panel_CMT.Controls.Add(panel_Name);
                    panel_CMT.Controls.Add(panel_IMG);

                    flowLayoutPanel2.Controls.Add(panel_CMT);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }




        }



        //Đóng Form
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
