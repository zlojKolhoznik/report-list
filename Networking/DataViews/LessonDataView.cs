using Newtonsoft.Json;

namespace Networking.DataViews
{
    public class LessonDataView
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("subjId")]
        public int SubjectId { get; set; }

        [JsonProperty("teachId")]
        public int TeacherId { get; set; }

        [JsonProperty("groupsIds")]
        public List<int> GroupsIds { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
    }
}
