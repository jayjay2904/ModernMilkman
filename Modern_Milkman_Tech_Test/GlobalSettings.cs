using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modern_Milkman_Tech_Test
{
    public class GlobalSettings
    {
        public static string _conn;

        public static string GetModernMilkmanConnection(IConfiguration _config)
        {
            _conn = _config.GetConnectionString("myDataBase");
            return _conn;
        }

    }
}
