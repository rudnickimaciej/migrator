using Migrator.Attributes;
using System;

namespace APKA.Domain
{
    [Entity]
    public class Car
    {
        public int Pole1 { get; set; }
        public int Pole2 { get; set; }
        [Length(200000)]
        public string Pole3 { get; set; }

    }


    [Entity]

    public class Car2
    {
        public int Pole1 { get; set; }
        public int Pole2 { get; set; }
    }
}
