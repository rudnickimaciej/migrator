using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Migrator;


namespace APKA_TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<string> list = new List<string>();

            for (int i = 0; i < 6; i++)
            {
                list.Add((i == 2 || i == 4 || i == 6) ? i.ToString() : null);
            }
        }
    }
}
