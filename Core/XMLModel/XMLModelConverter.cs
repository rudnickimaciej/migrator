using Common.Extensions;
using Core.TypeMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.XMLModel
{
    internal interface IXMLModelConverter
    {
        XMLModel ConvertTypeToXMLModel(Type t);
        XmlDocument ConverXmlModelToXML(XMLModel model);
        XMLModel ConverXmlToXMLModel(XmlDocument xml);
        XMLModel ConverXmlToXMLModel(string xmlPath);

    }
    internal class XMLModelConverter 
    {
        public static XMLModel ConvertTypeToXMLModel(Type t)
        {
            XMLModel model = new XMLModel();
            model.EntityName = t.Name;

            foreach (var f in t.GetProperties())
            {

                if (f.PropertyType.IsSimple())
                {
                    model.Fields.Add(new XMLModelField()
                    {
                        fieldName = f.Name,
                        fieldType = FieldType.SIMPLE,
                        typeNamespace = f.PropertyType.GetNamespace(),
                        netType = f.PropertyType.Name,
                        sqlType = TypeMapper.ConvertToSQLType(f.PropertyType)
                    });
                    continue;
                }

                if (f.PropertyType.IsSingleRefenceType())
                {
                    model.Fields.Add(new XMLModelField()
                    {
                        fieldName = f.Name,
                        fieldType = FieldType.REFERENCE,
                        typeNamespace = f.PropertyType.GetNamespace(),
                        netType = f.PropertyType.Name,
                        sqlType = SQLType.INT
                    });
                    continue;
                }
                if (f.PropertyType.IsSimpleList())
                {
                    model.Fields.Add(new XMLModelField()
                    {
                        fieldName = f.Name,
                        fieldType = FieldType.SIMPLE_LIST,
                        typeNamespace = f.PropertyType.GetGenericArguments()[0].GetNamespace(),
                        netType = f.PropertyType.GetGenericArguments()[0].ToString(),
                        sqlType = TypeMapper.ConvertToSQLType(f.PropertyType.GetGenericArguments()[0])
                    });
                    continue;
                }
                if (f.PropertyType.IsReferenceList())
                {
                    model.Fields.Add(new XMLModelField()
                    {
                        fieldName = f.Name,
                        fieldType = FieldType.REFERENCE_LIST,
                        typeNamespace = f.PropertyType.GetGenericArguments()[0].GetNamespace(),
                        netType = f.PropertyType.GetGenericArguments()[0].ToString(),
                        sqlType = SQLType.INT
                    });
                    continue;
                }
            }
            return model;
        }
        public static XmlDocument ConverXmlModelToXML(XMLModel model)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement entity = doc.CreateElement(string.Empty, "entity", string.Empty);
            XmlElement name = doc.CreateElement(string.Empty, "name", string.Empty);
            name.AppendChild(doc.CreateTextNode(model.EntityName));
            XmlElement fields = doc.CreateElement(string.Empty, "fields", string.Empty);
            doc.AppendChild(entity);
            entity.AppendChild(name);
            entity.AppendChild(fields);


            foreach (var f in model.Fields)
            {
                XmlElement field = doc.CreateElement(string.Empty, "field", string.Empty);
                XmlElement fieldName = doc.CreateElement(string.Empty, "name", string.Empty);
                fieldName.AppendChild(doc.CreateTextNode(f.fieldName));

                XmlElement fieldType = doc.CreateElement(string.Empty, "type", string.Empty);
                fieldType.AppendChild(doc.CreateTextNode(((int)f.fieldType).ToString()));

                XmlElement fieldNamespace = doc.CreateElement(string.Empty, "namespace", string.Empty);
                fieldNamespace.AppendChild(doc.CreateTextNode(f.typeNamespace));

                XmlElement netType = doc.CreateElement(string.Empty, "netType", string.Empty);
                netType.AppendChild(doc.CreateTextNode(f.netType));

                XmlElement sqlType = doc.CreateElement(string.Empty, "sqlType", string.Empty);
                sqlType.AppendChild(doc.CreateTextNode(((int)f.sqlType).ToString()));

                field.AppendChild(fieldName);
                field.AppendChild(fieldNamespace);
                field.AppendChild(fieldType);
                field.AppendChild(netType);
                field.AppendChild(sqlType);
                fields.AppendChild(field);
            }

            return doc;
        }

        public static XMLModel ConverXmlToXMLModel(XmlDocument xml)
        {
            XmlNode node = xml.SelectSingleNode("/entity");
            XMLModel model = new XMLModel();
            model.EntityName = node["name"].InnerText;

            XmlNodeList fieldNodes = node["fields"].SelectNodes("field");
            foreach (XmlNode f in fieldNodes)
            {

                model.Fields.Add(new XMLModelField()
                {
                    fieldName = f["name"].InnerText,
                    fieldType = (FieldType)Int32.Parse(f["type"].InnerText),
                    netType = f["netType"].InnerText,
                    sqlType = (SQLType)Int32.Parse(f["sqlType"].InnerText),
                    typeNamespace = f["namespace"].InnerText
                });
            }
            return model;

        }

        public static XMLModel ConverXmlToXMLModel(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            return ConverXmlToXMLModel(xmlDoc);
        }
    }
}
