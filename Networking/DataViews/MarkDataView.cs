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

        [JsonProperty("lesson")]
        public LessonDataView? Lesson { get; set; }

        [JsonProperty("homework")]
        public HomeworkDataView? Homework { get; set; }
    }
}
