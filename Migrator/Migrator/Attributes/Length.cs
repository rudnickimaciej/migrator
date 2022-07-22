using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    public class Length : Attribute
    {
        public Length()
        {
        }

        public Length(int len)
        {
            Len = Len;
        }

        public string Len
        {
            get { return this.Len; }
            set { this.Len = value; }
        }
    }
}
