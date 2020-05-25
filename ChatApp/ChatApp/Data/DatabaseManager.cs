using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public class DatabaseManager
    {
        public static string ConnectionString { get; set; }

        public static string LoggedInUser { get; set; }

        public static string Recipient { get; set; }
    }
}
