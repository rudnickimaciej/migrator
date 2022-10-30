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


        public float CenaFloat { get; set; }
        [Length(20)]
        public float CenaFloatWithLength { get; set; }


        public double CenaDouble { get; set; }
        [Length(30)]
        public double CenaDoubleWithLength { get; set; }

        public decimal CenaDecimal { get; set; }
        [Length(30)]
        public decimal CenaDecimalWithLength { get; set; }
    }
}