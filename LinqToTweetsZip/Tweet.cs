using System;

namespace LinqToTweetsZip
{
    public class Tweet
    {
        internal Tweet(MonthlyTweets parent, int index)
        {
            this.data = Lazy.Create(() => parent.data.Value[index]);
            this.year = parent.Year;
            this.month = parent.Month;
        }

        private readonly Lazy<TweetData> data;

        public string Source
        {
            get
            {
                return this.data.Value.Source;
            }
        }

        public Entities Entities
        {
            get
            {
                return this.data.Value.Entities;
            }
        }

        public Geo Geo
        {
            get
            {
                return this.data.Value.Geo;
            }
        }

        public ulong Id
        {
            get
            {
                return this.data.Value.Id;
            }
        }

        public string Text
        {
            get
            {
                return this.data.Value.Text;
            }
        }

        public DateTimeOffset CreatedAt
        {
            get
            {
                return this.data.Value.CreatedAt;
            }
        }

        public User User
        {
            get
            {
                return this.data.Value.User;
            }
        }

        internal readonly int year;
        internal readonly int month;
    }
}
