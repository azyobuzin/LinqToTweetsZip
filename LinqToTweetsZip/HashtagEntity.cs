using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class HashtagEntity : Entity
    {
        [JsonProperty("text")]
        public string Text { get; private set; }
    }
}
