using System.Runtime.CompilerServices;
using System.Xml;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Migrator.ISQLProviderNamespace
{
    public class XmlDoc
    {
        public XmlDocument xml { get; set; }

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
