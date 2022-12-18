using Migrator.Attributes;
using System;

namespace Przykladowa.Domain
{



     class Certyfikat
    {
        [Length(100)]
        public string Nazwa { get; set; }
        public DateTime DataWystawienia { get; set; }
        public DateTime DataUpływuWażności { get; set; }
    }


}