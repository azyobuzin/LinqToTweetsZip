using System;

namespace LinqToTweetsZip
{
    internal class PayloadDetails
    {
        public uint tweets { get; set; }
        public string created_at { get; set; }
        public string lang { get; set; }

        private DateTimeOffset? createdAt;
        internal DateTimeOffset CreatedAt
        {
            get
            {
                if (!this.createdAt.HasValue)
                    this.createdAt = DateTimeOffset.Parse(this.created_at);
                return this.createdAt.Value;
            }
        }
    }
}
