using System;
using System.Collections;
using System.Collections.Generic;

namespace LinqToTweetsZip
{
    public class MonthlyTweets : IReadOnlyList<Tweet>
    {
        internal MonthlyTweets(ITweetsZipSource source, TweetIndexItem info)
        {
            this.info = info;
            this.data = Lazy.Create(() =>
                source.Read<List<TweetData>>("data", "js", "tweets", string.Format("{0:D4}_{1:D2}.js", info.year, info.month)));
            var count = info.tweet_count;
            this.tweets = new Tweet[count];
            for (var i = 0; i < count; i++)
                this.tweets[i] = new Tweet(this, i);
        }

        private readonly TweetIndexItem info;
        internal readonly Lazy<List<TweetData>> data;
        private readonly Tweet[] tweets;

        public int Year
        {
            get
            {
                return this.info.year;
            }
        }

        public int Month
        {
            get
            {
                return this.info.month;
            }
        }

        public Tweet this[int index]
        {
            get
            {
                return this.tweets[index];
            }
        }

        public int Count
        {
            get
            {
                return this.info.tweet_count;
            }
        }

        public IEnumerator<Tweet> GetEnumerator()
        {
            return (IEnumerator<Tweet>)this.tweets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.tweets.GetEnumerator();
        }
    }
}
