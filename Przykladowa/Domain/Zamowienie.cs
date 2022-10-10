using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Przykładowa.Domain
{
    [Entity]
    class Zamowienie
    {
        public Przedmiot Przedmiot { get; set; }
        public int Ilosc { get; set; }
    }
}
