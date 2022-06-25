using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Migrator.Tests.XMLModelHelper")]

namespace Migrator
{
    internal static class XMLModelHelper
    {
        internal static List<XMLModelPair> PairSchemas(List<XMLModel> first, List<XMLModel> second)
        {
            var pairs = new List<Tuple<XMLModel, XMLModel>>();

            pairs.AddRange(first.Intersect(second)
                .Select(match => Tuple.Create(match, match)));

            pairs.AddRange(first.Except(second)
                .Select(inFirst => Tuple.Create(inFirst, (XMLModel)null)));

            pairs.AddRange(second.Except(first)
                .Select(inSecond => Tuple.Create((XMLModel)null, inSecond)));

            return pairs.Select(p => new XMLModelPair(p.Item1, p.Item2)).ToList();
        }

        internal static List<XMLModelFieldPair> PairFields(XMLModelPair pair)
        {
            var pairs = new List<Tuple<XMLModelField, XMLModelField>>();
            var first = pair.SchemaPair.Item1.Fields;
            var second = pair.SchemaPair.Item2.Fields;

            pairs.AddRange(first.Intersect(second)
                .Select(match => Tuple.Create(match, match)));

            pairs.AddRange(first.Except(second)
                .Select(inFirst => Tuple.Create(inFirst, (XMLModelField)null)));

            pairs.AddRange(second.Except(first)
                .Select(inSecond => Tuple.Create((XMLModelField)null, inSecond)));

            return pairs.Select(p => new XMLModelFieldPair(p.Item1, p.Item2)).ToList();
        }
    }
}