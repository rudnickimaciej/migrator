using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
using System;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLActions
{
    public class ModifyFieldTypeAction : ISQLAction
    {
        public ModifyFieldTypeAction(TFieldModel field)
        {
        }

        public List<SQLOperation> GenerateOperations()
        {
            throw new NotImplementedException();
        }
    }
}