using System;
using System.Linq.Expressions;

namespace MathsCore.SymbolicManipulation
{
    public class DifferentiationVisitor : ExpressionVisitor
    {
        protected override Expression VisitConstant(ConstantExpression node)
        {
            // c
            return Expression.Constant(0D);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            // x
            return Expression.Constant(1D);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method == typeof(Math).GetMethod("Pow"))
            {
                // x^c
                if (node.Arguments[0].NodeType == ExpressionType.Parameter && node.Arguments[1].NodeType == ExpressionType.Constant)
                {
                    return Expression.Multiply(
                        node.Arguments[1], Expression.Call(null,
                            typeof(Math).GetMethod("Pow"), node.Arguments[0], Expression.Subtract(node.Arguments[1], Expression.Constant(1D))
                        )
                    );
                }
            }
            else if (node.Method == typeof(Math).GetMethod("Sin"))
            {
                // sin(x)
                //     if (node.Arguments[0].NodeType == ExpressionType.Parameter)
                return Expression.Call(null, typeof(Math).GetMethod("Cos"), node.Arguments[0]);
            }

            return base.VisitMethodCall(node);
        }
    }
}
