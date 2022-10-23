using Migrator.Attributes;
using System;
using System.Collections.Generic;

namespace Przykładowa.Domain
{
    [Entity]
    class Klient
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime Urodziny { get; set; }
        public List<Zamowienie> Zamowienia { get; set; }
    }
}
