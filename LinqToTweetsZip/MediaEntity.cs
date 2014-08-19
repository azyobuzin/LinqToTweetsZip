using System.Collections.Generic;
using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class MediaEntity : UrlEntity
    {
        [JsonProperty("media_url")]
        public string MediaUrl { get; private set; }

        [JsonProperty("media_url_https")]
        public string MediaUrlHttps { get; private set; }

        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("sizes")]
        public IReadOnlyList<MediaSize> Sizes { get; private set; }
    }
}
