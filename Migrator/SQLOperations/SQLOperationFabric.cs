using System.Collections.Generic;

namespace Migrator
{
    internal static class SQLOperationFabric //TODO\; ZMIENIC NAZWER KLASY
    {
        internal static IEnumerable<ISQLAction> Create(TModelPair pair)
        {
            if (pair.SchemaPair.Item1 == null)
                return new List<ISQLAction>() { new AddTableAction(pair.SchemaPair.Item2) };

            if (pair.SchemaPair.Item2 == null)
                return new List<ISQLAction>() { new DeleteTableAction(pair.SchemaPair.Item1) };

           return ProcessModel(pair); //TOODO ZMIENIC NAZWER METODY
        }

        internal static IEnumerable<ISQLAction> ProcessModel(TModelPair pair)
        {

            List<TFieldModelPair> fieldsPairs = TModelHelper.PairFields(pair);

        
            foreach (TFieldModelPair fieldPair in fieldsPairs)
            {
                TFieldModel oldField = fieldPair.FieldPair.Item1;
                TFieldModel newField = fieldPair.FieldPair.Item2;

                if (oldField == null)               
                    yield return new AddFieldAction(newField);
                
                if (newField == null)              
                    yield return new DeleteFieldAction(oldField);

                if(oldField != null && newField != null && (oldField.Type != newField.Type || oldField.NetType != newField.NetType))
                    yield return new ModifyFieldTypeAction(newField);
            }
        }
    }

}