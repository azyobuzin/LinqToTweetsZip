using System.IO;
using Newtonsoft.Json;

namespace LinqToTweetsZip
{
    internal static class JavaScriptConvert
    {
        internal static T Read<T>(this ITweetsZipSource source, params string[] entryName)
        {
            using (var sr = new StreamReader(source.GetEntryStream(entryName)))
            {
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
}
