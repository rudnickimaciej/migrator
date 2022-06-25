using System.Collections.Generic;

namespace Migrator
{
    internal interface ISQLAction
    {
        List<SQLOperation> Execute();
    }
}