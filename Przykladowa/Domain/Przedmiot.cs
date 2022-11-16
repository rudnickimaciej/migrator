using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Przykładowa.Domena
{
    [Entity]
    class Przedmiot
    {
        [Length(100)]
        public string Nazwa { get; set; }
        public float Cena{ get; set; }
    }
}


