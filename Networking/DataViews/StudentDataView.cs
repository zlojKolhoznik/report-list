using Newtonsoft.Json;

namespace Networking.DataViews
{
    public class StudentDataView
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("dateOfBirth")]
        public long DateOfBirth { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}
