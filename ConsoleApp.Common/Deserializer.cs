using ChoETL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace ConsoleApp.Common
{
    public static class Deserializer
    {
        public static IEnumerable<T> FromJson<T>(string json) where T : class
        {
            var items = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            var jsonObj = items.ToJson();

            // Ensure JSON is valid and output to debug console
            Debug.WriteLine(JToken.Parse(jsonObj).ToString(Formatting.Indented));

            return items;
        }

        public static IEnumerable<T> FromCsv<T>(string csvPath, string[] cols) where T : class
        {
            var items = new List<T>();
            var config = new ChoCSVRecordConfiguration();

            for (int i = 0; i < cols.Length; i++)
            {
                config.CSVRecordFieldConfigurations.Add(new ChoCSVRecordFieldConfiguration(cols[i], i + 1));
            }

            foreach (var item in new ChoCSVReader<T>(csvPath, config).WithFirstLineHeader())
            {
                items.Add(item);
            }

            return items;
        }

        public static Dictionary<int, T> FromJsonDictionary<T>(string json) where T : class
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };
            var items = System.Text.Json.JsonSerializer.Deserialize<Dictionary<int, T>>(json, jsonOptions);

            return items;
        }
    }
}
