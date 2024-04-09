using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace DTO
{
    [Table(Name = "LoaiHang")]
    public class LoaiHang
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string Ma_loai_hang { get; set; }
        [Column]
        public string Ten_loai_hang { get; set; }
        [Column]
        public string Mo_ta { get; set; }
    }
}
