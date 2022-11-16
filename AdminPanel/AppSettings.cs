using Newtonsoft.Json;

namespace AdminPanel
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
