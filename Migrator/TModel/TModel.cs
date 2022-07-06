using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("Migrator.Tests")]
namespace Migrator
{
    internal class TModel
    {
        public int ID { get; set; }
        public string EntityName { get; set; }
        public List<TFieldModel> Fields = new List<TFieldModel>();
    }


}
