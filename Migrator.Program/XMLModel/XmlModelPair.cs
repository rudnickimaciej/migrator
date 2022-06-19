using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal class XmlModelPair
    {
        public XmlModelPair(XMLModel oldSchema, XMLModel newSchema)
        {
            SchemaPair = new Tuple<XMLModel, XMLModel>(oldSchema,newSchema);
        }
        public Tuple<XMLModel,XMLModel> SchemaPair{ get; set; }
    }
}
