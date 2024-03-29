﻿using Migrator.Attributes;
using Migrator.Commons.Extensions;
using Migrator.Commons.TypeMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Migrator.Commons
{
    public interface ITypeModelConverter
    {
        TModel ConvertTypeToXMLModel(Type t);
        XmlDocument ConverXmlModelToXML(TModel model);
        TModel ConverXmlToXMLModel(XmlDocument xml);
        TModel ConverXmlToXMLModel(string xmlPath);
        XmlDocument ConvertTypeToXML(Type t);

    }
    public static class TModelConverter 
    {
        public static TModel ConvertTypeToTModel(Type t)
        {
            TModel model = new TModel();
            model.EntityName = t.Name;
            model.ID = Guid.NewGuid();

            foreach (var f in t.GetProperties())
            {

                if (f.PropertyType.IsSimple() || f.PropertyType.IsEnum)
                {
                    model.Fields.Add(new TFieldModel()
                    {
                        ID = Guid.NewGuid(),
                        EntityName =t.Name,
                        Name = f.Name,
                        Type = FieldType.SIMPLE,
                        Namespace = f.PropertyType.GetNamespace(),
                        NetType = f.PropertyType.Name,
                        SqlType = TypeMapper.ConvertToSQLType(f.PropertyType),
                        FieldLength = getFieldLength(f)
                    });
                    continue;
                }

                if (f.PropertyType.IsSingleRefenceType())
                {
                    model.Fields.Add(new TFieldModel()
                    {
                        ID = Guid.NewGuid(),
                        EntityName = t.Name,
                        Name = f.Name,
                        Type = FieldType.REFERENCE,
                        Namespace = f.PropertyType.GetNamespace(),
                        NetType = f.PropertyType.Name,
                        SqlType = SQLType.INT,
                        FieldLength = getFieldLength(f)
                    });
                    continue;
                }
                if (f.PropertyType.IsSimpleList())
                {
                    model.Fields.Add(new TFieldModel()
                    {
                        ID = Guid.NewGuid(),
                        EntityName = t.Name,
                        Name = f.Name,
                        Type = FieldType.SIMPLE_LIST,
                        Namespace = f.PropertyType.GetGenericArguments()[0].GetNamespace(),
                        NetType = f.PropertyType.GetGenericArguments()[0].Name,
                        SqlType = TypeMapper.ConvertToSQLType(f.PropertyType.GetGenericArguments()[0]),
                        FieldLength = getFieldLength(f)
                    });
                    continue;
                }
                if (f.PropertyType.IsReferenceList())
                {
                    model.Fields.Add(new TFieldModel()
                    {
                        ID = Guid.NewGuid(),
                        EntityName = t.Name,
                        Name = f.Name,
                        Type = FieldType.REFERENCE_LIST,
                        Namespace = f.PropertyType.GetGenericArguments()[0].GetNamespace(),
                        NetType = f.PropertyType.GetGenericArguments()[0].Name,
                        SqlType = SQLType.INT,
                        FieldLength = getFieldLength(f)
                    });
                    continue;
                }
            }
            return model;
        }
        public static XmlDocument ConverTypeModelToXML(TModel model)
        {
            XmlDocument doc = new XmlDocument();
           // XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            //doc.InsertBefore(xmlDeclaration, root);

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
                fieldName.AppendChild(doc.CreateTextNode(f.Name));

                XmlElement fieldType = doc.CreateElement(string.Empty, "type", string.Empty);
                fieldType.AppendChild(doc.CreateTextNode(((int)f.Type).ToString()));

                XmlElement fieldNamespace = doc.CreateElement(string.Empty, "namespace", string.Empty);
                fieldNamespace.AppendChild(doc.CreateTextNode(f.Namespace));

                XmlElement netType = doc.CreateElement(string.Empty, "NetType", string.Empty);
                netType.AppendChild(doc.CreateTextNode(f.NetType));

                XmlElement sqlType = doc.CreateElement(string.Empty, "SqlType", string.Empty);
                sqlType.AppendChild(doc.CreateTextNode(((int)f.SqlType).ToString()));


                XmlElement fieldLength = doc.CreateElement(string.Empty, "FieldLength", string.Empty);
                fieldLength.AppendChild(doc.CreateTextNode(f.FieldLength.ToString()));

                field.AppendChild(fieldName);
                field.AppendChild(fieldNamespace);
                field.AppendChild(fieldType);
                field.AppendChild(netType);
                field.AppendChild(sqlType);
                field.AppendChild(fieldLength);

                fields.AppendChild(field);
            }

            return doc;
        }

        public static TModel ConverXmlToTModel(XmlDocument xml)
        {
            XmlNode node = xml.SelectSingleNode("/entity");
            TModel model = new TModel();
            model.EntityName = node["name"].InnerText;
            model.ID = Guid.NewGuid();
            XmlNodeList fieldNodes = node["fields"].SelectNodes("field");
            foreach (XmlNode f in fieldNodes)
            {

                model.Fields.Add(new TFieldModel()
                {
                    ID = Guid.NewGuid(),
                    EntityName = model.EntityName,
                    Name = f["name"].InnerText,
                    Type = (FieldType)Int32.Parse(f["type"].InnerText),
                    NetType = f["NetType"].InnerText,
                    SqlType = (SQLType)Int32.Parse(f["SqlType"].InnerText),
                    Namespace = f["namespace"].InnerText,
                    FieldLength = int.Parse(f["FieldLength"].InnerText)
                });
            }
            return model;

        }

        public static TModel ConverXmlToTypeModel(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            return ConverXmlToTModel(xmlDoc);
        }

        public static XmlDocument ConvertTypeToXML(Type t)
        {
          return  ConverTypeModelToXML(ConvertTypeToTModel(t));
        }

        private static void validateDefaultValue(PropertyInfo field)
        {
            //if (!field.PropertyType.IsSimple())
            //{
            //    throw new DefaultSetOnNonSimpleFieldException();
            //}

            //Attribute requiredAttribute = field.GetCustomAttributes(typeof(Required)).FirstOrDefault();
            //Type t = field.GetType();
            //t.
            //(requiredAttribute as Required).Default

            //return requiredAttribute != null;
        }
        private static int getFieldLength(PropertyInfo field)
        {

            if(field.PropertyType.IsList())
            {
                var i = 0;
            }
            Type t = field.PropertyType.IsList() ? field.PropertyType.GetGenericArguments()[0] : field.PropertyType;
            int defaultLength = TypeMapper.GetTypeDefaultLength(field.PropertyType);
            Attribute lengthAttribute = field.GetCustomAttributes(typeof(Length)).FirstOrDefault();

            if (defaultLength == -1 || (lengthAttribute as Length)?.Len == null)
                return defaultLength;

            return (lengthAttribute as Length).Len; 
        }
    }
}
