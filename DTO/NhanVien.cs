using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO
{
    [Table(Name = "NhanVien")]
    public class NhanVien
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string Ma_Nhan_Vien { get; set; }
        [Column]
        public string Ten_NV { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string MatKhau { get; set; }
        [Column]
        public string XacNhanMatKhau { get; set; }
        [Column]
        public string Gioi_Tinh { get; set; }
        [Column]
        public string VaiTro { get; set; }
        [Column]
        public string TinhTrang { get; set; }
        [Column]
        public string DiaChi { get; set; }

    }
}
