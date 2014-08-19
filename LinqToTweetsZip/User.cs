using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; private set; }

        [JsonProperty("protected")]
        public bool Protected { get; private set; }

        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrlHttps { get; private set; }

        [JsonProperty("verified")]
        public bool Verified { get; private set; }
    }
}
