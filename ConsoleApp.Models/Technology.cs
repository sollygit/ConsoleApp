using Newtonsoft.Json;

namespace ConsoleApp.Models
{
    public class Technology
    {
        [JsonProperty(PropertyName = "technologyId")]
        public int TechnologyId = 0;
        [JsonProperty(PropertyName = "technologyName")] 
        public string TechnologyName;

        public override string ToString()
        {
            return $"Id:{TechnologyId}, Name:{TechnologyName}";
        }
    }
}
