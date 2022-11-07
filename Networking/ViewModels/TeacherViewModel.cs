using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.ViewModels
{
    public class TeacherViewModel
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
