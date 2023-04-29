namespace MathExpr.Analyzers;

public class TokenCollection
{
    private readonly Token[] _tokens;
    private int _cursor = 0;

    public TokenCollection(IEnumerable<Token> tokens)
    {
        _tokens = tokens.ToArray();
    }

    public bool MoveNext()
    {
        if (_cursor >= _tokens.Count() - 1) return false;

        _cursor++;
        return true;
    }

    public Token Current => _tokens[_cursor];

    public Token? Previous
    {
        get
        {
            var previousCursorPosition = _cursor - 1;
            return previousCursorPosition >= 0 ? _tokens[previousCursorPosition] : null;
        }
    }
    
    public Token? Next 
    {
        get
        {
            var nextCursorPosition = _cursor + 1;
            return nextCursorPosition >= _tokens.Count() - 1 ? null : _tokens[nextCursorPosition];
        }
    }
}