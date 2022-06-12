using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Core
{
    internal class XMLModel
    {
        public string EntityName { get; set; }
        public List<XMLModelField> Fields = new List<XMLModelField>();
    }

    internal class XMLModelField
    {
        public string EntityName { get; set; }
        public FieldType fieldType { get; set; }
        public string fieldName { get; set; }
        public string typeNamespace { get; set; }
        public string netType { get; set; }
        public SQLType sqlType { get; set; }
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
