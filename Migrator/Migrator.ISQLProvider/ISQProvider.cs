using Migrator.Commons;
using System.Collections.Generic;

namespace Migrator.ISQLProviderNamespace
{
    public interface ISQLProvider
    {
        IEnumerable<ISQLAction> CreateActions(TModelPair modelPair);

        List<TModel> GetCurrentSchemas(string connectionString);
        void ExecuteScript(string sql, string connectionString, IEnumerable<TModel> newSchemas);
    }
}