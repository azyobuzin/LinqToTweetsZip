using System.Collections.Generic;
using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class Geo
    {
        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("coordinates")]
        public IReadOnlyList<double> Coordinates { get; private set; }
    }
}
