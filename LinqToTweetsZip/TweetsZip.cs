using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqToTweetsZip
{
    public class TweetsZip : IReadOnlyList<MonthlyTweets>
    {
        public TweetsZip(ITweetsZipSource source)
        {
            this.payloadDetails = Lazy.Create(() => source.Read<PayloadDetails>("data", "js", "payload_details.js"));
            this.months = Lazy.Create(() =>
                source.Read<List<TweetIndexItem>>("data", "js", "tweet_index.js")
                    .Select(item => new MonthlyTweets(source, item))
                    .ToList()
            );
            this.userDetails = Lazy.Create(() => source.Read<UserDetails>("data", "js", "user_details.js"));
        }

        private readonly Lazy<PayloadDetails> payloadDetails;
        private readonly Lazy<List<MonthlyTweets>> months;
        private readonly Lazy<UserDetails> userDetails;

        public uint Tweets
        {
            get
            {
                return this.payloadDetails.Value.tweets;
            }
        }

        public DateTimeOffset CreatedAt
        {
            get
            {
                return this.payloadDetails.Value.CreatedAt;
            }
        }

        public UserDetails UserDetails
        {
            get
            {
                return this.userDetails.Value;
            }
        }

        public int Count
        {
            get
            {
                return this.months.Value.Count;
            }
        }

        public IEnumerator<MonthlyTweets> GetEnumerator()
        {
            foreach (var m in this.months.Value)
                yield return m;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public MonthlyTweets this[int index]
        {
            get
            {
                return this.months.Value[index];
            }
        }
    }
}
