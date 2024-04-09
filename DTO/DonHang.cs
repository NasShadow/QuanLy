using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO
{
    [Table(Name = "DonHang")]
    public class DonHang
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string Ma_Don_Hang { get; set; }
        [Column]
        public string Thanh_Tien { get; set; }
        [Column]
        public string Ngay_Xuat_Don { get; set; }
        [Column]
        public string Trang_Thai_Don { get; set; }
        [Column]
        public string Ma_Khach_Hang { get; set; }
        [Column]
        public string Ma_Nhan_Vien { get; set; }
    }
}
