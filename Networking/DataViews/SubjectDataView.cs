using Newtonsoft.Json;

namespace Networking.DataViews
{
    public class SubjectDataView
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
