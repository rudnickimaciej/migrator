﻿using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class ModifyFieldTypeAction : ISQLAction
    {
        public ModifyFieldTypeAction(XMLModelField field)
        {
        }

        public List<SQLOperation> Execute()
        {
            throw new NotImplementedException();
        }
    }
}