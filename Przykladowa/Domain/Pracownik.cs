using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Przykładowa.Domain
{
    [Entity]
    class Pracownik
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public List<Klient> Klienci { get; set; }
        public Pracownik Przelozony { get; set; }

    }
}
