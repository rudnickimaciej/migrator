using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("Migrator.ISQLProvider")]

namespace Migrator.Commons
{
    public class TModelPair
    {
        public TModelPair(TModel oldSchema, TModel newSchema)
        {
            SchemaPair = new Tuple<TModel, TModel>(oldSchema,newSchema);
        }
        public Tuple<TModel,TModel> SchemaPair{ get; set; }

         public override string ToString()
        {
            return string.Format("({0}, {1})", SchemaPair?.Item1?.EntityName, SchemaPair?.Item2?.EntityName);
        }
    }
}
