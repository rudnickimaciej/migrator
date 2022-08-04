﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Commons.Attributes
{
    internal class LengthAttributeTooSmall : Exception
    {
        public LengthAttributeTooSmall(int length) : base($"{length} is too small value for Length Attribute")
        {

        }
    }
}
