using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MockNet.Tests")]
namespace MockNet.Http
{
    internal class IsExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.Right is MethodCallExpression call)
            {
                if (methods.ContainsKey(call.Method.Name))
                {
                    return methods[call.Method.Name](call, node);
                }
            }

            return node;
        }

        private static Dictionary<string, Func<MethodCallExpression, BinaryExpression, Expression>> methods = new Dictionary<string, Func<MethodCallExpression, BinaryExpression, Expression>>()
        {
            [nameof(Is.Any)] = (c, n) => Make(c.Method, n.Left),
            [nameof(Is.NotNull)] = (c, n) => Make(c.Method, n.Left),
            [nameof(Is.Empty)] = (c, n) => Make(c.Method, n.Left),
            [nameof(Is.SameAs)] = (c, n) => Make(c.Method, n.Left, c.Arguments),
            [nameof(Is.Equal)] = (c, n) => Make(c.Method, n.Left, c.Arguments),
            [nameof(Is.In)] = (c, n) => Make(c.Method, n.Left, c.Arguments),
        };

        private static Dictionary<string, Func<MethodInfo, Type[], Type[]>> generics = new Dictionary<string, Func<MethodInfo, Type[], Type[]>>()
        {
            ["*"] = (c, ts) => c.GetGenericArguments(),
            [nameof(Is.NotNull)] = (c, ts) => ts,
            [nameof(Is.Any)] = (c, ts) => ts,
        };

        private static Expression Make(MethodInfo call, Expression parameter)
        {
            if (parameter.NodeType == ExpressionType.Convert && parameter is UnaryExpression unary)
            {
                parameter = unary.Operand;
            }

            var method = GetMethod(call, new[] { parameter.Type });

            return Expression.Call(method, parameter);
        }

        private static Expression Make(MethodInfo call, Expression parameter, ReadOnlyCollection<Expression> expressions)
        {
            var args = expressions.ToList();

            args.Insert(0, parameter);

            var method = GetMethod(call, new[] { parameter.Type });

            return Expression.Call(method, args.ToArray());
        }

        private static MethodInfo GetMethod(MethodInfo call, Type[] typeArguments)
        {
            var callParameterType = call.GetParameters()[0].ParameterType;

            {
                var methods = typeof(IsInternal).GetMethods();
                var correctNameMethods = methods.Where(x => x.Name == call.Name && x.IsGenericMethod == call.IsGenericMethod);
                var anonymous = correctNameMethods.Select(x => new
                {
                    Method = x,
                    SecondParameter = x.GetParameters().SecondOrDefault()
                }).ToList();
                var z = anonymous.Where(x => x.SecondParameter.ParameterType.IsAssignableFrom(callParameterType)).ToList();
            }


            var method = typeof(IsInternal)
                .GetMethods()
                .Where(x => x.Name == call.Name && x.IsGenericMethod == call.IsGenericMethod)
                .Select(x => new
                {
                    Method = x,
                    Parameters = x.GetParameters().Select(x => x.ParameterType).ToList()
                })
                .Where(x => x.Parameters[1].IsAssignableFrom(callParameterType))
                .Select(x => x.Method)
                .FirstOrDefault();

            // return the generic version of the method
            if (call.IsGenericMethod)
            {
                return method.MakeGenericMethod(GetGenericTypes(call, typeArguments));
            }

            return method;
        }

        private static Type[] GetGenericTypes(MethodInfo call, Type[] typeArguments)
        {
            var key = "*";

            if (generics.ContainsKey(call.Name))
            {
                key = call.Name;
            }

            return generics[key](call, typeArguments);
        }
    }
}