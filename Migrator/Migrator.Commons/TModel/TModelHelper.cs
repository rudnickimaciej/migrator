using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Migrator.Tests.TModelHelper")]

namespace Migrator.Commons
{
    public static class TModelHelper
    {
        public static List<TModelPair> PairSchemas(List<TModel> first, List<TModel> second)
        {
            var pairs = new List<Tuple<TModel, TModel>>();

            pairs.AddRange(first.Intersect(second, new XmlModelNameEqualityComparer())
                .Select(match => Tuple.Create(match, match)));

            pairs.AddRange(first.Except(second, new XmlModelNameEqualityComparer())
                .Select(inFirst => Tuple.Create(inFirst, (TModel)null)));

            pairs.AddRange(second.Except(first, new XmlModelNameEqualityComparer())
                .Select(inSecond => Tuple.Create((TModel)null, inSecond)));

            return pairs.Select(p => new TModelPair(p.Item1, p.Item2)).ToList();
        }

        public static List<TFieldModelPair> PairFields(TModelPair pair)
        {
            var pairs = new List<Tuple<TFieldModel, TFieldModel>>();
            var first = pair.SchemaPair.Item1.Fields;
            var second = pair.SchemaPair.Item2.Fields;

            pairs.AddRange(first.Intersect(second, new XmlModelFieldNameEqualityComparer())
                .Select(match => Tuple.Create(match, second.Where(s=>s.Name.Equals(match.Name)).FirstOrDefault())));

            pairs.AddRange(first.Except(second, new XmlModelFieldNameEqualityComparer())
                .Select(inFirst => Tuple.Create(inFirst, (TFieldModel)null)));

            pairs.AddRange(second.Except(first, new XmlModelFieldNameEqualityComparer())
                .Select(inSecond => Tuple.Create((TFieldModel)null, inSecond)));

            return pairs.Select(p => new TFieldModelPair(p.Item1, p.Item2)).ToList();
        }
    }


    public class XmlModelNameEqualityComparer : IEqualityComparer<TModel>
    {
        public bool Equals(TModel x, TModel y) => x.EntityName == y.EntityName;

        public int GetHashCode(TModel obj)
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

    public class XmlModelFieldNameEqualityComparer : IEqualityComparer<TFieldModel>
    {
        public bool Equals(TFieldModel x, TFieldModel y) => x.Name == y.Name;

        public int GetHashCode(TFieldModel obj)
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