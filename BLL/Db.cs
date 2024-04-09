using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Db
    {
        public string strConnection = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
    }
}
