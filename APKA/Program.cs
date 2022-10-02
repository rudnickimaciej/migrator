using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APKA.Domain;


namespace APKA_TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            List<string> list = new List<string>();

            for (int i = 0; i < 6; i++)
            {
                list.Add((i == 2 || i == 4 || i == 6) ? i.ToString() : null);
            }

            SqlCommand command = new SqlCommand("SELECT * FROM Client");
            SqlDataReader dataReader;
            using( SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=migrator5;Integrated Security=True"))
            {
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                   
                }
            }

        }
    }
}
