using System.Collections.Generic;
using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class Entities
    {
        [JsonProperty("user_mentions")]
        public IReadOnlyList<UserMentionEntity> UserMentions { get; private set; }

        [JsonProperty("media")]
        public IReadOnlyList<MediaEntity> Media { get; private set; }

        [JsonProperty("hashtags")]
        public IReadOnlyList<HashtagEntity> Hashtags { get; private set; }

        [JsonProperty("urls")]
        public IReadOnlyList<UrlEntity> Urls { get; private set; }
    }
}
