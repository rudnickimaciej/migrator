using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal class XMLModelFieldPair
    {
        public XMLModelFieldPair(XMLModelField oldField, XMLModelField newField)
        {
            FieldPair = new Tuple<XMLModelField, XMLModelField>(oldField, newField);
        }
        public Tuple<XMLModelField, XMLModelField> FieldPair { get; set; }

         public override string ToString()
        {
            return string.Format("({0} {1}, {2} {3})", FieldPair?.Item1?.fieldName, FieldPair?.Item1?.ID, FieldPair?.Item2?.fieldName, FieldPair?.Item2?.ID);
        }
    }
}
