using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Migrator.Tests.SQLOperationFabric
{
    internal class SQLOperationFabricTests
    {

        [Test]
        public void CreateActions_FieldTypeChanged_ReturnModifyFieldTypeAction()
        {
            XMLModel oldSchema = new XMLModel()
            {
                EntityName = "entity1",
                Fields = new List<XMLModelField>()
                {
                    new XMLModelField()
                    {
                        ID = 11,
                        EntityName = "entity1",
                        fieldName = "field1",
                        fieldType = FieldType.SIMPLE,
                        netType = "INT",
                        sqlType = SQLType.INT,
                        typeNamespace = "SYSTEM"
                    },
                    new XMLModelField()
                    {
                        ID = 12,
                        EntityName = "entity1",
                        fieldName = "field2",
                        fieldType = FieldType.REFERENCE,
                        netType = "INT",
                        sqlType = SQLType.INT,
                        typeNamespace = "SYSTEM"
                    }
                }
            };

            XMLModel newSchema = new XMLModel()
            {
                EntityName = "entity1",
                Fields = new List<XMLModelField>()
                    {
                        new XMLModelField()
                        {
                            ID = 21,
                            EntityName = "entity1",
                            fieldName = "field1",
                            fieldType = FieldType.SIMPLE,
                            netType = "INT",
                            sqlType = SQLType.INT,
                            typeNamespace = "SYSTEM"
                        },
                        new XMLModelField()
                        {
                            ID = 22,
                            EntityName = "entity1",
                            fieldName = "field2",
                            fieldType = FieldType.SIMPLE,
                            netType = "STRING",
                            sqlType = SQLType.INT,
                            typeNamespace = "SYSTEM"
                        }
                    }
            };

            List<SQLOperation> pairs = Migrator.SQLOperationFabric.Create(new XMLModelPair(oldSchema, newSchema));

            //Assert.AreEqual(4, pairs.Count);
            //Assert.IsNull(pairs[0].SchemaPair.Item1);
            //Assert.IsNull(pairs[1].SchemaPair.Item1);
            //Assert.IsNull(pairs[2].SchemaPair.Item1);
            //Assert.IsNull(pairs[3].SchemaPair.Item1);

            //Assert.IsNotNull(pairs[0].SchemaPair.Item2);
            //Assert.IsNotNull(pairs[1].SchemaPair.Item2);
            //Assert.IsNotNull(pairs[2].SchemaPair.Item2);
            //Assert.IsNotNull(pairs[3].SchemaPair.Item2);
        }
    }
}
