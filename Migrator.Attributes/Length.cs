using System;

namespace Migrator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Length : Attribute
    {
        public Length(System.Reflection.FieldInfo field)
        {
        }

        public Length(int length)
        {
            Len = length;
        }

        public int Len { get; private set; }
    }
}
