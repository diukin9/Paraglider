using System.Linq.Expressions;

namespace Paraglider.Infrastructure.Common;

public class Visitor<T> : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    public Visitor(ParameterExpression parameter)
    {
        _parameter = parameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return _parameter;
    }

    protected override Expression VisitMember(MemberExpression node)
    {
        if (node.Member.MemberType != System.Reflection.MemberTypes.Property)
        {
            throw new NotImplementedException();
        }

        var memberName = node.Member.Name;
        var otherMember = typeof(T).GetProperty(memberName);
        var inner = Visit(node.Expression);
        return Expression.Property(inner, otherMember!);
    }
}
