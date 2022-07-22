using Migrator.Commons;
using System.Collections.Generic;

namespace Migrator.ISQLProviderNamespace
{
    public interface ISQLProvider
    {
        IEnumerable<ISQLAction> CreateActions(TModelPair modelPair);

        void CreateConfigurationTables(string connectionString);

        List<XmlDoc> GetSchemasFromDb(string connectionString);
        void ExecuteScript(string sql);
    }
}