using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal class TFieldModelPair
    {
        public TFieldModelPair(TFieldModel oldField, TFieldModel newField)
        {
            FieldPair = new Tuple<TFieldModel, TFieldModel>(oldField, newField);
        }
        public Tuple<TFieldModel, TFieldModel> FieldPair { get; set; }

         public override string ToString()
        {
            return string.Format("({0} {1}, {2} {3})", FieldPair?.Item1?.Name, FieldPair?.Item1?.ID, FieldPair?.Item2?.Name, FieldPair?.Item2?.ID);
        }
    }
}
