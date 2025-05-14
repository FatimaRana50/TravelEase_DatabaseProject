using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace DB_module2
{
    internal class Global
    {
        public static int OperatorID { get; set; }
        public static int AdminID { get; set; }

        public static int ProviderID { get; set; }

        public static int TravelerID { get; set; }
        public static Home HomeInstance { get; set; }
    }
}
