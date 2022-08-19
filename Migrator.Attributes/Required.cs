using System;

namespace Migrator.Attributes
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
