﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    public class RequestOptions
    {
        [JsonProperty("rType")]
        public RequestType RequestType { get; set; }

        [JsonProperty("uId")]
        public int? UserId { get; set; }

        [JsonProperty("sId")]
        public int? SubjectId { get; set; }

        [JsonProperty("tId")]
        public int? TeacherId { get; set; }

        [JsonProperty("gId")]
        public int? GroupId { get; set; }

        [JsonProperty("hwId")]
        public int? HomeworkId { get; set; }

        [JsonProperty("lId")]
        public int? LessonId { get; set; }

        [JsonProperty("mId")]
        public int? MarkId { get; set; }

        [JsonProperty("sеId")]
        public int? StudId { get; set; }

        [JsonProperty("gName")]
        public string? groupName { get; set; }

        [JsonProperty("hwDueDate")]
        public int? HomeworkDueDate { get; set; }

        [JsonProperty("hwFileExtension")]
        public string? HomeworkFileExtension { get; set; }

        [JsonProperty("login")]
        public string? Login { get; set; }

        [JsonProperty("pword")]
        public string? Password { get; set; }

        [JsonProperty("stName")]
        public string? StudentName { get; set; }

        [JsonProperty("stSurname")]
        public string? StudentSurname { get; set; }

        [JsonProperty("stDateOfBirth")]
        public int? StudentDateOfBirth { get; set; }

        [JsonProperty("isAdmin")]
        public bool? IsAdmin { get; set; }

        [JsonProperty("lTopic")]
        public string? LessonTopic { get; set; }

        [JsonProperty("lDate")]
        public int? LessonDate { get; set; }

        [JsonProperty("lGroupsIds")]
        public List<int>? LessonGroupsIds { get; set; }

        [JsonProperty("mVal")]
        public int? MarkValue { get; set; }

        [JsonProperty("tName")]
        public string? TeacherName { get; set; }

        [JsonProperty("tSurname")]
        public string? TeacherSurname { get; set; }

        [JsonProperty("tSubjectsIds")]
        public List<int>? TeacherSubjectsIds { get; set; }

        [JsonProperty("sName")]
        public string? SubjectName { get; set; }
    }
}
