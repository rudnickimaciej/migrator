using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal static class SQLOperationFabric
    {
        internal static List<SQLOperation> Create(XmlModelPair pair)
        {
            if (pair.SchemaPair.Item1 == null)
                return new CreateTableAction(pair.SchemaPair.Item2).Execute();
            if (pair.SchemaPair.Item2 == null)
                return new DeleteTableAction(pair.SchemaPair.Item1).Execute();


            return null;
        }
    }
}
