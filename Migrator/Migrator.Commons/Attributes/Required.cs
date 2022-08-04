using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Required : Attribute
    {
        public Required(object defaultValue)
        {
            Default = defaultValue;
        }

        public object Default { get; private set; }
    }
}
