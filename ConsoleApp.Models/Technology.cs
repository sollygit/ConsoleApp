using System;

namespace ConsoleApp.Models
{
    public class Technology
    {
        public int TechnologyId { get; set; }
        public string TechnologyName { get; set; }
        public TechnologyType TechnologyType { get; set; }

        public override string ToString()
        {
            return $"Id:{TechnologyId}, Name:{TechnologyName}, Type:{TechnologyType}";
        }
    }

    [Flags]
    public enum TechnologyType
    {
        Unknown = 0x00,
        Upstream = 0x01,
        Downstream = 0x02,
        Standard = Upstream | Downstream,
        Transit = 0x04,
        Cage = 0x08,
        Mailbag = 0x10
    }
}
