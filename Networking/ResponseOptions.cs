using Networking.ViewModels;
using Newtonsoft.Json;

namespace Networking
{
    public class ResponseOptions
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("errorMsg")]
        public string? ErrorMessage { get; set; }

        [JsonProperty("user")]
        public UserViewModel? User { get; set; }

        [JsonProperty("student")]
        public StudentViewModel? Student { get; set; }

        [JsonProperty("students")]
        public List<StudentViewModel>? Students { get; set; }

        [JsonProperty("teacher")]
        public TeacherViewModel? Teacher { get; set; }

        [JsonProperty("groups")]
        public List<GroupViewModel>? Groups { get; set; }

        [JsonProperty("homeworks")]
        public List<HomeworkViewModel>? Homeworks { get; set; }

        [JsonProperty("lessons")]
        public List<LessonViewModel>? Lessons { get; set; }

        [JsonProperty("marks")]
        public List<MarkViewModel>? Marks { get; set; }

        [JsonProperty("subjects")]
        public List<SubjectViewModel>? Subjects { get; set; }
    }
}
