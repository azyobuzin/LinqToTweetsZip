using System;
using System.IO;
using System.IO.Compression;

namespace LinqToTweetsZip
{
    public class TweetsZipFileSource : ITweetsZipSource, IDisposable
    {
        public TweetsZipFileSource(ZipArchive archive)
        {
            this.archive = archive;
        }

        public TweetsZipFileSource(Stream stream)
            : this(new ZipArchive(stream, ZipArchiveMode.Read))
        { }

        public TweetsZipFileSource(string fileName)
            : this(ZipFile.OpenRead(fileName))
        { }

        private ZipArchive archive;

        public Stream GetEntryStream(params string[] entryName)
        {
            return this.archive.GetEntry(string.Join("/", entryName)).Open();
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if (this.archive != null)
                {
                    this.archive.Dispose();
                    this.archive = null;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
