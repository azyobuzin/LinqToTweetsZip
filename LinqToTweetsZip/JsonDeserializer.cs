using System.IO;
using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    internal static class JsonDeserializer
    {
        internal static T Deserialize<T>(Stream stream)
        {
            var sr = new StreamReader(stream);
            var c = sr.Read();
            while (c != '=')
            {
                if (c == -1) throw new InvalidDataException();
                c = sr.Read();
            }

            return JsonSerializer.CreateDefault().Deserialize<T>(new JsonTextReader(sr));
        }
    }
}
