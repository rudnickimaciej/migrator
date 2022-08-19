using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Name : Attribute
    {
        public Name()
        {
        }

        public Name(string name)
        {
            FieldName = name;
        }

        public string FieldName { get; private set; }
    }
}
