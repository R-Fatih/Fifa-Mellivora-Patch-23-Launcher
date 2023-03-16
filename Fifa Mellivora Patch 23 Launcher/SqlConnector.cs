using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifa_Mellivora_Patch_23_Launcher
{
    internal class SqlConnector
    {
        SqlConnection connection = new SqlConnection(@"Data Source=SQL5103.site4now.net;Initial Catalog=db_a9023d_fifamelpatch23;User Id=db_a9023d_fifamelpatch23_admin;Password=Quaresma1378;MultipleActiveResultSets=True");
    public SqlConnection Connection()
        {
            return connection;  
        }
    }
}
