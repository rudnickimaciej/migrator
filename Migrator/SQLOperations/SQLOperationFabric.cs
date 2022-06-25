using System.Collections.Generic;

namespace Migrator
{
    internal static class SQLOperationFabric
    {
        internal static IEnumerable<ISQLAction> Create(XMLModelPair pair)
        {
            if (pair.SchemaPair.Item1 == null)

                return new List<ISQLAction>() { new CreateTableAction(pair.SchemaPair.Item2) };

            if (pair.SchemaPair.Item2 == null)
                return new List<ISQLAction>() { new DeleteTableAction(pair.SchemaPair.Item1) };

           return ProcessModel(pair);
        }

        internal static IEnumerable<ISQLAction> ProcessModel(XMLModelPair pair)
        {

            List<XMLModelFieldPair> fieldsPairs = XMLModelHelper.PairFields(pair);

        
            foreach (XMLModelFieldPair fieldPair in fieldsPairs)
            {
                XMLModelField oldField = fieldPair.FieldPair.Item1;
                XMLModelField newField = fieldPair.FieldPair.Item2;

                if (oldField == null)               
                    yield return new AddFieldAction(newField);
                
                if (newField == null)              
                    yield return new DeleteFieldAction(oldField);

                if(oldField.fieldType != newField.fieldType || oldField.netType != newField.netType)
                    yield return new ModifyFieldTypeAction(newField);
            }
        }
    }

}