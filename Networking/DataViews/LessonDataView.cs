using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
