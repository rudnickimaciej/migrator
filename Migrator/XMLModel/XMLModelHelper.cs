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

            pairs.AddRange(first.Intersect(second, new XmlModelNameEqualityComparer())
                .Select(match => Tuple.Create(match, match)));

            pairs.AddRange(first.Except(second, new XmlModelNameEqualityComparer())
                .Select(inFirst => Tuple.Create(inFirst, (XMLModel)null)));

            pairs.AddRange(second.Except(first, new XmlModelNameEqualityComparer())
                .Select(inSecond => Tuple.Create((XMLModel)null, inSecond)));

            return pairs.Select(p => new XMLModelPair(p.Item1, p.Item2)).ToList();
        }

        internal static List<XMLModelFieldPair> PairFields(XMLModelPair pair)
        {
            var pairs = new List<Tuple<XMLModelField, XMLModelField>>();
            var first = pair.SchemaPair.Item1.Fields;
            var second = pair.SchemaPair.Item2.Fields;

            pairs.AddRange(first.Intersect(second, new XmlModelFieldNameEqualityComparer())
                .Select(match => Tuple.Create(match, second.Where(s=>s.Name.Equals(match.Name)).FirstOrDefault())));

            pairs.AddRange(first.Except(second, new XmlModelFieldNameEqualityComparer())
                .Select(inFirst => Tuple.Create(inFirst, (XMLModelField)null)));

            pairs.AddRange(second.Except(first, new XmlModelFieldNameEqualityComparer())
                .Select(inSecond => Tuple.Create((XMLModelField)null, inSecond)));

            return pairs.Select(p => new XMLModelFieldPair(p.Item1, p.Item2)).ToList();
        }
    }


    internal class XmlModelNameEqualityComparer : IEqualityComparer<XMLModel>
    {
        public bool Equals(XMLModel x, XMLModel y) => x.EntityName == y.EntityName;

        public int GetHashCode(XMLModel obj)
        {
            unchecked
            {
                var hash = 17;
                //same here, if you only want to get a hashcode on a, remove the line with b
                hash = hash * 23 + obj.EntityName.GetHashCode();
                hash = hash * 23 + obj.EntityName.GetHashCode();

                return hash;
            }
        }
    }

    internal class XmlModelFieldNameEqualityComparer : IEqualityComparer<XMLModelField>
    {
        public bool Equals(XMLModelField x, XMLModelField y) => x.Name == y.Name;

        public int GetHashCode(XMLModelField obj)
        {
            unchecked
            {
                var hash = 17;
                //same here, if you only want to get a hashcode on a, remove the line with b
                hash = hash * 23 + obj.Name.GetHashCode();
                hash = hash * 23 + obj.Name.GetHashCode();

                return hash;
            }
        }
    }
}