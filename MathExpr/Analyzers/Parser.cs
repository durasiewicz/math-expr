using MathExpr.Syntax;

namespace MathExpr.Analyzers;

public class Parser
{
    private TokenCollection _tokenCollection;

    public Expression? Parse(TokenCollection tokenCollection)
    {
        _tokenCollection = tokenCollection;
        return Expression();
    }

    private Expression? Expression()
    {
        var result = Term();

        if (result != null && _tokenCollection.Current.Type is 
                TokenType.Plus or 
                TokenType.Minus or
                TokenType.Equals)
        {
            var tokenType = _tokenCollection.Current.Type;
            _tokenCollection.MoveNext();
            var rightNode = Expression();
            result = new BinaryExpression(tokenType switch
            {
                TokenType.Plus => ExpressionType.Add,
                TokenType.Minus => ExpressionType.Subtract,
                TokenType.Equals => ExpressionType.Assign,
                _ => throw new ArgumentOutOfRangeException()
            }, result, rightNode);
        }

        return result;
    }

    private Expression? Term()
    {
        var result = Factor();

        if (result != null && _tokenCollection.Current.Type is TokenType.Asterisk or TokenType.Slash)
        {
            var tokenType = _tokenCollection.Current.Type;
            _tokenCollection.MoveNext();
            var rightNode = Term();
            result = new BinaryExpression(tokenType switch
            {
                TokenType.Asterisk => ExpressionType.Multiply,
                TokenType.Slash => ExpressionType.Divide,
                _ => throw new ArgumentOutOfRangeException()
            }, result, rightNode);
        }

        return result;
    }

    private Expression? Factor()
    {
        Expression result = null;

        if (_tokenCollection.Current.Type is TokenType.Minus &&
            _tokenCollection.Previous is not null &&
            (_tokenCollection.Previous.Type is TokenType.Equals or TokenType.Plus or TokenType.Minus or TokenType.Asterisk or TokenType.Slash))
        {
            _tokenCollection.MoveNext();
            result = Expression();
            return new NegateExpression(result);
        }
        else if (_tokenCollection.Current.Type is TokenType.Number or TokenType.Identifier)
        {
            // Function call
            if (_tokenCollection.Next?.Type == TokenType.OpenRoundBracket)
            {
                var functionName = _tokenCollection.Current.Value;
                _tokenCollection.MoveNext();
                var parameters = new List<Expression>();
                do
                {
                    _tokenCollection.MoveNext();
                    parameters.Add(Expression());
                } while (_tokenCollection.Current.Type == TokenType.Comma);

                if (_tokenCollection.Current.Type != TokenType.CloseRoundBracket)
                {
                    throw new Exception("Expected )");
                }

                result = new FunctionCallExpression(functionName, parameters.ToArray());
            }
            else
            {
                result = new ConstantExpression(_tokenCollection.Current.Value);
            }
        }
        else if (_tokenCollection.Current.Type == TokenType.OpenRoundBracket)
        {
            _tokenCollection.MoveNext();
            result = Expression();
            if (_tokenCollection.Current.Type != TokenType.CloseRoundBracket)
            {
                throw new Exception("Expected )");
            }
        }
        else if (_tokenCollection.Current.Type == TokenType.EndOfCode)
        {
            return result;
        }
        
        _tokenCollection.MoveNext();

        return result;
    }
}