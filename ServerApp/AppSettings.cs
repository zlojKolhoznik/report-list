using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    internal class AppSettings
    {
        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; } = null!;

        public static AppSettings ReadFromJsonFile(string path)
        {
            string json = File.ReadAllText(path);
            var settings = JsonConvert.DeserializeObject<AppSettings>(json);
            if (settings == null)
            {
                throw new InvalidDataException("File was not in correct format");
            }
            return settings;
        }
    }
}
