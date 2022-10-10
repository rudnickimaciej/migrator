using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Przykładowa.Domain
{
    [Entity]
    class Klient
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public bool Urodziny { get; set; }
        public List<Zamowienie> Zamowienia { get; set; }
    }
}
