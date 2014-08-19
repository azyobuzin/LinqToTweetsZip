using System;

namespace LinqToTweetsZip
{
    internal static class Lazy
    {
        internal static Lazy<T> Create<T>(Func<T> factory)
        {
            return new Lazy<T>(factory, true);
        }
    }
}
