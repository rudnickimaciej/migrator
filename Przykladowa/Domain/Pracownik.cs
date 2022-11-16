using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Przykładowa.Domena
{
    [Entity]
    class Pracownik
    {
        [Length(25)]
        public string Imie { get; set; }
        [Length(30)]
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public List<Klient> Klienci { get; set; }
        public List<string> Telefony { get; set; }
        public Pracownik Przelozony { get; set; }
    }
}





