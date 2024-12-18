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
        Frontend = 0x01,
        Cloud = 0x02,
        Hybrid = 0x04,
        Dotnet = 0x08,
        Javascript = 0x10,
        NodeJS = Frontend | Cloud // bitwise OR
    }
}
