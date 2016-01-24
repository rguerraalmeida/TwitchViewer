using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TwitchClientViewer
{
    public static class Reflect
    {
        public static MemberInfo GetMember(Expression<Action> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(
                    GetMember(() => expression).Name);
            }

            return GetMemberInfo(expression as LambdaExpression);
        }

        public static MemberInfo GetMember<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(
                    GetMember(() => expression).Name);
            }

            return GetMemberInfo(expression as LambdaExpression);
        }

        public static MethodInfo GetMethod(Expression<Action> expression)
        {
            MethodInfo method = GetMember(expression) as MethodInfo;
            if (method == null)
            {
                throw new ArgumentException(
                    "Not a method call expression", GetMember(() => expression).Name);
            }

            return method;
        }

        public static PropertyInfo GetProperty<T>(Expression<Func<T>> expression)
        {
            PropertyInfo property = GetMember(expression) as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException(
                    "Not a property expression", GetMember(() => expression).Name);
            }

            return property;
        }

        public static FieldInfo GetField<T>(Expression<Func<T>> expression)
        {
            FieldInfo field = GetMember(expression) as FieldInfo;
            if (field == null)
            {
                throw new ArgumentException(
                    "Not a field expression", GetMember(() => expression).Name);
            }

            return field;
        }

        internal static MemberInfo GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
            {
                throw new ArgumentNullException(
                    GetMember(() => lambda).Name);
            }

            MemberExpression memberExpression = null;
            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpression = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = lambda.Body as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.Call)
            {
                return ((MethodCallExpression)lambda.Body).Method;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException(
                    "Not a member access", GetMember(() => lambda).Name);
            }

            return memberExpression.Member;
        }
    }


    public static class ReflectOn<T>
    {
        public static MemberInfo GetMember(Expression<Action<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(Reflect.GetMember(() => expression).Name);
            }

            return Reflect.GetMemberInfo(expression as LambdaExpression);
        }

        public static MemberInfo GetMember<TResult>(Expression<Func<T, TResult>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(Reflect.GetMember(() => expression).Name);
            }

            return Reflect.GetMemberInfo(expression as LambdaExpression);
        }

        public static MethodInfo GetMethod(Expression<Action<T>> expression)
        {
            MethodInfo method = GetMember(expression) as MethodInfo;
            if (method == null)
            {
                throw new ArgumentException(
                    "Not a method call expression",
                    Reflect.GetMember(() => expression).Name);
            }

            return method;
        }

        public static PropertyInfo GetProperty<TResult>(Expression<Func<T, TResult>> expression)
        {
            PropertyInfo property = GetMember(expression) as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException(
                    "Not a property expression", Reflect.GetMember(() => expression).Name);
            }

            return property;
        }

        public static FieldInfo GetField<TResult>(Expression<Func<T, TResult>> expression)
        {
            FieldInfo field = GetMember(expression) as FieldInfo;
            if (field == null)
            {
                throw new ArgumentException(
                    "Not a field expression", Reflect.GetMember(() => expression).Name);
            }

            return field;
        }
    }


    public static class PushNotificationOn
    {
        public static void CreateExpression()
        {
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression five = Expression.Constant(5, typeof(int));
            BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
            Expression<Func<int, bool>> lambda1 =
                Expression.Lambda<Func<int, bool>>(
                    numLessThanFive,
                    new ParameterExpression[] { numParam });
        }

        public static PropertyInfo GetProperty(Expression<Func<string>> expr)
        {
            var member = expr.Body as MemberExpression;
            if (member == null)
                throw new InvalidOperationException("Expression is not a member access expression.");
            var property = member.Member as PropertyInfo;
            if (property == null)
                throw new InvalidOperationException("Member in expression is not a property.");
            return property;
        }
    }
}
