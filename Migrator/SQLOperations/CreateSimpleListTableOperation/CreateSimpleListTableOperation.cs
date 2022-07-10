﻿using System.Collections.Generic;

namespace Migrator
{
    internal class CreateSimpleListTableOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CREATE_TABLE;

        public CreateSimpleListTableOperation(TModel entity)
        {
            _sql = $"CREATE TABLE {entity.EntityName} (ID INT PRIMARY KEY)"; //TODO: USE SCHEMA FROM FILE
        }

    }
}