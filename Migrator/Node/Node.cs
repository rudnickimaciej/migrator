using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("Migrator")]
[assembly: InternalsVisibleToAttribute("Migrator.Tests")]

namespace Migrator
{
    internal interface INode
    {
        bool HasReference();
    }
    internal class Node : INode
    {
        public Node(Type type)
        {
            Type = type;
        }

        public Type Type { get; set; }

        public bool HasReference()
        {
            foreach(var prop in this.Type.GetProperties())            
                if (!prop.PropertyType.IsSimple())
                    return true;                      
            return false;
        }
    }

}
