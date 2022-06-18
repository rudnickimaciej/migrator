using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Core
{
    public class Field : Attribute
    {
        public Field()
        {
        }

        public Field(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return this.Name; }
            set { this.Name = value; }
        }
    }
}
