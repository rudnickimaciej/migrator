using Migrator.Attributes;

namespace Przykladowa.Domain
{
    [Entity]
     class Telefon
    {
        [Length(12)]
        public string Numer { get; set; }
    }
}
