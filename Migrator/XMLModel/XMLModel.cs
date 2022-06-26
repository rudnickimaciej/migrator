using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("Migrator.Tests")]
namespace Migrator
{
    internal class XMLModel
    {
        public int ID { get; set; }
        public string EntityName { get; set; }
        public List<XMLModelField> Fields = new List<XMLModelField>();
    }

    internal class XMLModelField
    {
        public int ID { get; set; }
        public string EntityName { get; set; }
        public FieldType Type { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string NetType { get; set; }
        public SQLType SqlType { get; set; }
    }

    internal enum FieldType
    {
        SIMPLE,
        REFERENCE,
        SIMPLE_LIST,
        REFERENCE_LIST,
        UNDEFINED
    }

    public enum SQLType
    {
        INT,
        VARCHAR,
        DATETIME,
        BOOL
    }
}
