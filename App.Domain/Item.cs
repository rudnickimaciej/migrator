using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain
{
    [Entity]
    class Item
    {
        public string Name { get; set; }

        public float Price { get; set; }
    }
}