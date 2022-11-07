using Newtonsoft.Json;

namespace Networking.DataViews
{
    public class UserDataView
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("pword")]
        public string Password { get; set; }
    }
}
