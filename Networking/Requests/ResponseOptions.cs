using Networking.DataViews;
using Newtonsoft.Json;

namespace Networking.Requests
{
    public class ResponseOptions
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("errorMsg")]
        public string? ErrorMessage { get; set; }

        [JsonProperty("user")]
        public UserDataView? User { get; set; }

        [JsonProperty("student")]
        public StudentDataView? Student { get; set; }

        [JsonProperty("students")]
        public List<StudentDataView>? Students { get; set; }

        [JsonProperty("teacher")]
        public TeacherDataView? Teacher { get; set; }

        [JsonProperty("groups")]
        public List<GroupDataView>? Groups { get; set; }

        [JsonProperty("homeworks")]
        public List<HomeworkDataView>? Homeworks { get; set; }

        [JsonProperty("lessons")]
        public List<LessonDataView>? Lessons { get; set; }

        [JsonProperty("marks")]
        public List<MarkDataView>? Marks { get; set; }

        [JsonProperty("subjects")]
        public List<SubjectDataView>? Subjects { get; set; }

        [JsonProperty("hwFile")]
        public List<byte>? HomeworkFile { get; set; }

        [JsonProperty("hwFileExtension")]
        public string? HomeworkFileExtension { get; set; }
    }
}
