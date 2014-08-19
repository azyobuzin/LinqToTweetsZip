using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToTweetsZip
{
    internal class TweetsZipQueryable<T> : IOrderedQueryable<T>
    {
        internal TweetsZipQueryable(TweetsZipQueryProvider provider, Expression expression)
        {
            this.provider = provider;
            this.expression = expression;
        }

        internal TweetsZipQueryable(TweetsZipQueryProvider provider)
        {
            this.provider = provider;
            this.expression = Expression.Constant(this);
        }

        private readonly Expression expression;
        private readonly TweetsZipQueryProvider provider;

        public IEnumerator<T> GetEnumerator()
        {
            return this.provider.Execute<IEnumerable<T>>(this.expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.provider.Execute<IEnumerable>(this.expression).GetEnumerator();
        }

        public Type ElementType
        {
            get
            {
                return typeof(T);
            }
        }

        public Expression Expression
        {
            get
            {
                return this.expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return this.provider;
            }
        }
    }
}
