using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTweetsZip
{
    internal static class Extensions
    {
        internal static TResult Using<TSource, TResult>(this TSource source, Func<TSource, TResult> selector)
            where TSource : IDisposable
        {
            using (source)
                return selector(source);
        }

        internal static T Read<T>(this ITweetsZipSource source, params string[] entryName)
        {
            return JsonDeserializer.Deserialize<T>(source.GetEntryStream(entryName));
        }
    }
}
