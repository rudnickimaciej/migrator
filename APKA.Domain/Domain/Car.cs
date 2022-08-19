using Migrator.Attributes;


namespace APKA.Domain
{
    [Entity]
    public class Car
    {
        public int Pole1 { get; set; }
        public int Pole2 { get; set; }
    }


    [Entity("Car")]
    public class Car2
    {
        public int Pole1 { get; set; }
        public int Pole2 { get; set; }
    }
}
