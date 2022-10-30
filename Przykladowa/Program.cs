using Migrator.Attributes;
using System;
using System.Collections.Generic;

namespace PrzykładowaAplikacjaKonsolowa
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}


                        [Entity]
                        class Klasa
                        {
                            public int Pole1 { get; set; }
                            public List<Klasa2> ListaKlas2 { get; set; }
                        }



                        [Entity]
                        class Klasa2
                        {
                            public int Pole1 { get; set; }
                            public string Pole2 { get; set; }
                            public DateTime Pole3 { get; set; }
                        }


