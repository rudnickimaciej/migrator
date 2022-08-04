using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Commons.Attributes
{
    internal class LengthAttributeTooBig : Exception
    {
        public LengthAttributeTooBig(int length) : base($"{length} is too big value for Length Attribute")
        {

        }
    }
}
