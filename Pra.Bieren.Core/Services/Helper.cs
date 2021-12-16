using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Bieren.Core.Services
{
    class Helper
    {
        public static string GetConnectionString()
        {
            return @"Data Source=(local)\SQLEXPRESS;Initial Catalog=praBieren; Integrated security=true;";
        }
    }
}
