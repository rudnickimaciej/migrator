using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal class SQLOperationCollection : IEnumerable<SQLOperation>
    {
        public SQLOperationCollection(params SQLOperation[] operation)
        {
        }

        public IEnumerator<SQLOperation> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
