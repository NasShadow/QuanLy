using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Voucher
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string Ma_Voucher { get; set; }
        [Column]
        public string Gia_Tri { get; set; }
        [Column]
        public string Tinh_Trang { get; set; }
        [Column]
        public string Ngay_Het_Han { get; set; }
        [Column]
        public string Ngay_Nhan_Voucher { get; set; }
        [Column]
        public string Ma_Khach_Hang { get; set; }
    }
}
