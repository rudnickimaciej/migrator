using System.Collections.Generic;

namespace Migrator
{
    internal interface ISQLAction
    {
        SQLOperationCollection GenerateOperations();
    }

    
}