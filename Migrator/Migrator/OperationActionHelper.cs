﻿using Migrator.ISQLProviderNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
        internal static List<SQLOperation> RemoveDuplicates(IEnumerable<SQLOperation> list)
        {
            return list.GroupBy(o => o.Sql).Select(o => o.First()).ToList();
        }

        internal static IEnumerable<SQLOperation> SortByType(IEnumerable<SQLOperation> list) 
            => list.OrderBy(c => c.Type).ToList();

        internal static string Merge(IEnumerable<SQLOperation> sortedOperations)
        {
            StringBuilder sb = new StringBuilder();
            sortedOperations.ToList().ForEach(o => sb.AppendLine(o.Sql));
            return sb.ToString();
        }
    }
}
