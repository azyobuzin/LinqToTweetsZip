using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class UrlEntity : Entity
    {
        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("expanded_url")]
        public string ExpandedUrl { get; private set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; private set; }
    }
}
