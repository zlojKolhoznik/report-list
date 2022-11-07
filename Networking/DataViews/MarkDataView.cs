using Newtonsoft.Json;

namespace Networking.DataViews
{
    public class MarkDataView
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("studId")]
        public int StudentId { get; set; }

        [JsonProperty("hwId")]
        public int? HomeworkId { get; set; }

        [JsonProperty("lessonId")]
        public int? LessonId { get; set; }
    }
}
