using Migrator.Attributes;
using System;
using System.Collections.Generic;

namespace Przykładowa.Domena
{
    [Entity]
    class Klient
    {
        [Length(20)]
        public string Imie { get; set; }
        [Length(25)]
        public string Nazwisko { get; set; }
        public DateTime Urodziny { get; set; }
        public List<Zamowienie> Zamowienia { get; set; }
    }
}
