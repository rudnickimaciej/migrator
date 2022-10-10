using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Przykładowa.Domain
{
    [Entity]
    class Przedmiot
    {
        public string Nazwa { get; set; }
        public float Cena{ get; set; }
    }
}
