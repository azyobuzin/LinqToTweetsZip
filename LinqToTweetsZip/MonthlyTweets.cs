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
            this.tweets = Lazy.Create(() =>
                source.Read<List<Tweet>>("data", "js", "tweets", string.Format("{0:D4}_{1:D2}.js", info.year, info.month)));
        }

        private readonly TweetIndexItem info;
        private readonly Lazy<List<Tweet>> tweets;

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
                return this.tweets.Value[index];
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
            foreach (var t in this.tweets.Value)
                yield return t;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
