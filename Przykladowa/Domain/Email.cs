using Migrator.Attributes;
using System;

namespace Przykladowa.Domain
{
    [Entity]
    internal class Email
    {
        [Length(50)]
        public string Address { get; set; }
    }
}