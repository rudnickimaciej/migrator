using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Przykładowa.Domena
{
    [Entity]
    class Pracownik
    {
        [Length(300)]
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public List<Klient> Klienci { get; set; }
        public Pracownik Przelozony { get; set; }

    }
}
