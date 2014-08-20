using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToTweetsZip
{
    internal class TweetsZipQueryProvider : IQueryProvider
    {
        internal TweetsZipQueryProvider(TweetsZip context)
        {
            this.context = context;
        }

        private TweetsZip context;

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TweetsZipQueryable<TElement>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var type = expression.Type.GetInterfaces()
                .First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .GetGenericArguments()[0];
            return (IQueryable)Activator.CreateInstance(
                typeof(TweetsZipQueryable<>).MakeGenericType(type),
                this, expression
            );
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)this.Execute(expression);
        }

        public object Execute(Expression expression)
        {
            var t = expression.Type;
            var isQueryable = t == typeof(IQueryable) ||
                (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IQueryable<>));

            var queryable = this.context.Months.SelectMany(_ => _).AsQueryable();
            expression = new OptimizeVisitor(queryable).Visit(expression);
            return isQueryable
                ? queryable.Provider.CreateQuery(expression)
                : queryable.Provider.Execute(expression);
        }
    }
}
