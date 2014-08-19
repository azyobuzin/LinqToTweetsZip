using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class MediaSize
    {
        [JsonProperty("h")]
        public int H { get; private set; }

        [JsonProperty("w")]
        public int W { get; private set; }

        [JsonProperty("resize")]
        public string Resize { get; private set; }
    }
}
