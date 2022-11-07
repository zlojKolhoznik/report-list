using Newtonsoft.Json;

namespace Networking.DataViews
{
    public class TeacherDataView
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("subjectsIds")]
        public List<int> SubjectsIds { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}
