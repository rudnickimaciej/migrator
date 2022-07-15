using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.ISQLProviderNamespace
{
    public interface ISQLAction
    {
        List<SQLOperation> GenerateOperations();
    }
}
