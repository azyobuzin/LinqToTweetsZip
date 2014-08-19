using System;
using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class UserDetails
    {
        [JsonProperty("screen_name")]
        public string ScreenName { get; private set; }

        [JsonProperty("location")]
        public string Location { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("expanded_url")]
        public string ExpandedUrl { get; private set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; private set; }

        [JsonProperty("full_name")]
        public string FullName { get; private set; }

        [JsonProperty("bio")]
        public string Bio { get; private set; }

        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty]
        private string created_at { get; set; }

        private DateTimeOffset? createdAt;
        public DateTimeOffset CreatedAt
        {
            get
            {
                if (!this.createdAt.HasValue)
                    this.createdAt = DateTimeOffset.Parse(this.created_at);
                return this.createdAt.Value;
            }
        }
    }
}
