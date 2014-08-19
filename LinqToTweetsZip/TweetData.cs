using System;
using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    internal class TweetData
    {
        [JsonProperty("source")]
        public string Source { get; private set; }

        [JsonProperty("entities")]
        public Entities Entities { get; private set; }

        [JsonProperty("geo")]
        public Geo Geo { get; private set; }

        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("text")]
        public string Text { get; private set; }

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

        [JsonProperty("user")]
        public User User { get; private set; }
    }
}
