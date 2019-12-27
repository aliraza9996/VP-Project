using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace KSLR_R_FaceRecognitionsSystem
{
    class Connection
    {
        Connection connObj;
        public SqlConnection conn()
        {
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=ALI; Initial Catalog=Recognize; Integrated Security=True;";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            Console.WriteLine("Connection Established");
            return cnn;
        }
    }
}
