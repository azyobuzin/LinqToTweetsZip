using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;

namespace LinqToTweetsZip
{
    internal class TweetsZipQueryProvider : IQueryProvider
    {
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
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
            // DateTimeOffset 同士の比較を置き換える
            // DateTimeOffset へのプロパティアクセスを置き換える
        }
    }
}
