using Newtonsoft.Json;

namespace Networking.DataViews
{
    public class HomeworkDataView
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dueDate")]
        public long DueDate { get; set; }

        [JsonProperty("fileExt")]
        public string FileExtension { get; set; }

        [JsonProperty("fileData")]
        public byte[] FileData { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        [JsonProperty("teacherId")]
        public int TeacherId { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("subjectId")]
        public int SubjectId { get; set; }
    }
}
