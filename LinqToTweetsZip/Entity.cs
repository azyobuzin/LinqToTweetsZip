using System.Collections.Generic;
using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    public class Entity
    {
        [JsonProperty("indices")]
        public IReadOnlyList<int> Indices { get; private set; }
    }
}
