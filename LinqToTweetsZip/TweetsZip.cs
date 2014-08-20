using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToTweetsZip
{
    public class TweetsZip
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
            this.Tweets = new TweetsZipQueryable<Tweet>(this);
        }

        private readonly Lazy<PayloadDetails> payloadDetails;
        private readonly Lazy<List<MonthlyTweets>> months;
        private readonly Lazy<UserDetails> userDetails;

        public uint TweetsCount
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

        public IReadOnlyList<MonthlyTweets> Months
        {
            get
            {
                return this.months.Value;
            }
        }

        public IQueryable<Tweet> Tweets { get; private set; }
    }
}
