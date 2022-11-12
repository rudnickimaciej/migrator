using Migrator.Attributes;
using System;
using System.Collections.Generic;

namespace Przykładowa.Domena
{
    [Entity]
    class Klient
    {
        [Length(150)]
        public string Imie { get; set; }
        [Length(200)]
        public string Nazwisko { get; set; }
        public DateTime Urodziny { get; set; }
        public List<Zamowienie> Zamowienia { get; set; }
    }
}
