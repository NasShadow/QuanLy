using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO
{
    [Table(Name = "KhachHang")]
    public class KhachHang
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string Ma_Khach_Hang { get; set; }
        [Column]
        public string Ten_TK { get; set; }
        [Column]
        public string Ten_Khach_Hang { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string MatKhau { get; set; }
        [Column]
        public string XacNhanMatKhau { get; set; }
        [Column]
        public string Gioi_Tinh { get; set; }
        [Column]
        public string DienThoai { get; set; }
        [Column]
        public string TrangThai { get; set; }
    }
}
