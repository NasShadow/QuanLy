using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO
{
    [Table(Name = "SanPham")]
    public class SanPham
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string Ma_SP { get; set; }
        [Column]
        public string Ten_SP { get; set; }
        [Column]
        public string So_luong { get; set; }
        [Column]
        public string Gia_nhap { get; set; }
        [Column]
        public string Gia_ban { get; set; }
        [Column]
        public string FileNames { get; set; }
        [Column]
        public string Ma_loai_hang { get; set; }
    }
}
