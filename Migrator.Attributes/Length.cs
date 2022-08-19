using Migrator.Attributes.Exceptions;
using System;

namespace Migrator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Length : Attribute
    {
        public Length()
        {
        }

        public Length(int length)
        {
            if(length < 0)
            {
                throw new LengthAttributeTooSmall(length);
            }
            if(length > 8000)
            {
                throw new LengthAttributeTooBig(length);
            }
            Len = length;
        }

        public int Len { get; private set; }
    }
}
