using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain;

namespace App
{
    internal class DAL
    {
        IEnumerable<Client> GetClients()
        {

        }
        IEnumerable<Worker> GetWorkers()
        {

        }
        IEnumerable<Item> GetItems()
        {

        }
        IEnumerable<Item> GetOrders()
        {

        }
    }

    private IEnumerable<object> Query(string query)
    {
        using (var cn = new SqlConnection())
        {
            cn.ConnectionString = "Data Source=KARENS-PC;" +
                                  "Initial Catalog=NorthWind;" +
                                  "Integrated Security=True";


            using (var cmd = new SqlCommand() { Connection = cn, CommandText = query })
            {

                cn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    string firstName = reader.GetString(0);
                    string lastName = reader.GetString(1);

                }

            }
        }
    }
}
