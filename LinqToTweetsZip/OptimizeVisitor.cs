using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToTweetsZip
{
    internal class OptimizeVisitor : ExpressionVisitor
    {
        public OptimizeVisitor(IQueryable target)
        {
            this.target = target;
        }

        private IQueryable target;

        private static readonly Type tweetType = typeof(Tweet);
        private static readonly PropertyInfo createdAtProperty = tweetType.GetProperty("CreatedAt");

        private Expression GetCreatedAtPropertyContainer(Expression expr)
        {
            if (expr == null || expr.NodeType != ExpressionType.MemberAccess)
                return null;

            var mexpr = expr as MemberExpression;
            return mexpr != null && mexpr.Member == createdAtProperty
                ? mexpr.Expression : null;
        }

        private static bool opEquality(Tweet left, DateTimeOffset right)
        {
            right = right.ToUniversalTime();
            return left.year == right.Year && left.month == right.Month && left.CreatedAt == right;
        }

        private static bool opInequality(Tweet left, DateTimeOffset right)
        {
            return !opEquality(left, right);
        }

        // >
        private static bool opGreaterThan(Tweet left, DateTimeOffset right)
        {
            right = right.ToUniversalTime();
            if (left.year > right.Year) return true;
            if (left.year < right.Year) return false;
            if (left.month > right.Month) return true;
            if (left.month < right.Month) return false;
            return left.CreatedAt > right;
        }

        // >=
        private static bool opGreaterThanOrEqual(Tweet left, DateTimeOffset right)
        {
            right = right.ToUniversalTime();
            if (left.year > right.Year) return true;
            if (left.year < right.Year) return false;
            if (left.month > right.Month) return true;
            if (left.month < right.Month) return false;
            return left.CreatedAt >= right;
        }

        // <
        private static bool opLessThan(Tweet left, DateTimeOffset right)
        {
            return !opGreaterThanOrEqual(left, right);
        }

        // <=
        private static bool opLessThanOrEqual(Tweet left, DateTimeOffset right)
        {
            return !opGreaterThan(left, right);
        }

        private static readonly Type thisType = typeof(OptimizeVisitor);
        private const BindingFlags privateStatic = BindingFlags.NonPublic | BindingFlags.Static;
        private static readonly MethodInfo opEqualityMethod = thisType.GetMethod("opEquality", privateStatic);
        private static readonly MethodInfo opInequalityMethod = thisType.GetMethod("opInequality", privateStatic);
        private static readonly MethodInfo opGreaterThanMethod = thisType.GetMethod("opGreaterThan", privateStatic);
        private static readonly MethodInfo opGreaterThanOrEqualMethod = thisType.GetMethod("opGreaterThanOrEqual", privateStatic);
        private static readonly MethodInfo opLessThanMethod = thisType.GetMethod("opLessThan", privateStatic);
        private static readonly MethodInfo opLessThanOrEqualMethod = thisType.GetMethod("opLessThanOrEqual", privateStatic);

        protected override Expression VisitBinary(BinaryExpression node)
        {
            MethodInfo method = null;
            var tweetExpr = GetCreatedAtPropertyContainer(node.Left);

            if (tweetExpr != null)
            {
                switch (node.NodeType)
                {
                    case ExpressionType.Equal:
                        method = opEqualityMethod;
                        break;
                    case ExpressionType.NotEqual:
                        method = opInequalityMethod;
                        break;
                    case ExpressionType.GreaterThan:
                        method = opGreaterThanMethod;
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        method = opGreaterThanOrEqualMethod;
                        break;
                    case ExpressionType.LessThan:
                        method = opLessThanMethod;
                        break;
                    case ExpressionType.LessThanOrEqual:
                        method = opLessThanOrEqualMethod;
                        break;
                }
                if (method != null)
                    return Expression.Call(method, tweetExpr, this.Visit(node.Right));
            }
            else if ((tweetExpr = GetCreatedAtPropertyContainer(node.Right)) != null)
            {
                switch (node.NodeType)
                {
                    case ExpressionType.Equal:
                        method = opEqualityMethod;
                        break;
                    case ExpressionType.NotEqual:
                        method = opInequalityMethod;
                        break;
                    case ExpressionType.GreaterThan:
                        method = opLessThanMethod;
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        method = opLessThanOrEqualMethod;
                        break;
                    case ExpressionType.LessThan:
                        method = opGreaterThanMethod;
                        break;
                    case ExpressionType.LessThanOrEqual:
                        method = opGreaterThanOrEqualMethod;
                        break;
                }
                if (method != null)
                    return Expression.Call(method, tweetExpr, this.Visit(node.Left));
            }

            return base.VisitBinary(node);
        }

        private static readonly Type dateTimeOffsetType = typeof(DateTimeOffset);
        private static readonly PropertyInfo yearProperty = dateTimeOffsetType.GetProperty("Year");
        private static readonly PropertyInfo monthProperty = dateTimeOffsetType.GetProperty("Month");
        private static readonly FieldInfo yearField = tweetType.GetField("year", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo monthField = tweetType.GetField("month", BindingFlags.NonPublic | BindingFlags.Instance);

        protected override Expression VisitMember(MemberExpression node)
        {
            var tweetExpr = GetCreatedAtPropertyContainer(node.Expression);
            if (node.NodeType == ExpressionType.MemberAccess && tweetExpr != null)
            {
                if (node.Member == yearProperty)
                {
                    return Expression.Field(tweetExpr, yearField);
                }
                else if (node.Member == monthProperty)
                {
                    return Expression.Field(tweetExpr, monthField);
                }
            }

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type.IsGenericType && node.Type.GetGenericTypeDefinition() == typeof(TweetsZipQueryable<>))
                return Expression.Constant(this.target);

            return base.VisitConstant(node);
        }
    }
}
