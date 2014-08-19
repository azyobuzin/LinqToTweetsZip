using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class UserMentionEntity : Entity
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; private set; }

        [JsonProperty("id")]
        public ulong Id { get; private set; }
    }
}
