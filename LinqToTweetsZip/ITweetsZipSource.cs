using System.IO;

namespace LinqToTweetsZip
{
    public interface ITweetsZipSource
    {
        Stream GetEntryStream(params string[] entryName);
    }
}
