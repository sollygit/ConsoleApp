using Newtonsoft.Json;

namespace ConsoleApp
{
    public class Technology
    {
        [JsonProperty(PropertyName = "technologyId")]
        public int TechnologyId = 0;
        [JsonProperty(PropertyName = "technologyName")] 
        public string TechnologyName;
    }
}
