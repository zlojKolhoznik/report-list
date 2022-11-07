﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string FileData { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        [JsonProperty("teacherId")]
        public int TeacherId { get; set; }
    }
}
