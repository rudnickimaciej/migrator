using Migrator.ISQLProviderNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal class OperationActionHelper
    {
        internal static IEnumerable<ISQLAction> FlattenActions(IEnumerable<IEnumerable<ISQLAction>> actions)
        {
            List<ISQLAction> flat = new List<ISQLAction>();

            foreach (var a in actions)
                foreach (var a2 in a)
                    flat.Add(a2);
            return flat;
        }
        internal static IEnumerable<SQLOperation> FlattenOperations(IEnumerable<IEnumerable<SQLOperation>> operations)
        {
            List<SQLOperation> flat = new List<SQLOperation>();

            foreach (var a in operations)
                foreach (var a2 in a)
                    flat.Add(a2);
            return flat;
        }

        internal static IEnumerable<SQLOperation> SortByType(IEnumerable<SQLOperation> list) => list.OrderBy(c => c.Type).ToList();

        internal static string ToSQL(List<SQLScript> scripts)
        {
            StringBuilder sb = new StringBuilder();
            scripts.ForEach(s => sb.AppendLine(s.Sql + "GO;"));
            sb.Replace("\r\n", " ");
            return sb.ToString();
        }

        internal static List<SQLScript> FlattenPackages(List<SQLPackage> packages)
        {
            List<SQLScript> flat = new List<SQLScript>();

            foreach (var package in packages)
                foreach (var script in package.Scripts)
                    flat.Add(script);
            return flat;
        }
    }
}
