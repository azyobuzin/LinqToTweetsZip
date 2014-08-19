using System;
using System.IO;

namespace LinqToTweetsZip
{
    public class TweetsZipDirectorySource : ITweetsZipSource
    {
        public TweetsZipDirectorySource(DirectoryInfo directory)
        {
            this.directory = directory;
        }

        public TweetsZipDirectorySource(string path)
            : this(new DirectoryInfo(path))
        { }

        private readonly DirectoryInfo directory;

        public Stream GetEntryStream(params string[] entryName)
        {
            var fileNameArray = new string[entryName.Length + 1];
            fileNameArray[0] = this.directory.FullName;
            Array.Copy(entryName, 0, fileNameArray, 1, entryName.Length);
            return File.OpenRead(Path.Combine(fileNameArray));
        }
    }
}
