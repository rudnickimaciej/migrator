using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Migrator
{
    internal class XmlDoc
    {
        protected XmlDocument xml { get; set; }

        public XmlDoc(XmlDocument xml)
        {
            this.xml = xml;
        }

        public XmlDoc(string xml)
        {
            if (this.xml == null)
                this.xml = new XmlDocument();

            this.xml.LoadXml(xml);
        }
    }
}
