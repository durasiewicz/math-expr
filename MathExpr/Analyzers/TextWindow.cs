namespace MathExpr.Analyzers;

public struct TextWindow
{
    private readonly string _text;
    private readonly Range _range;
    private int _cursor;
    private readonly int _currentLine;

    public TextWindow(string text, Range range, int currentLine)
    {
        _currentLine = currentLine;
        _text = text;
        _range = range;
        _cursor = _range.Start.Value - 1;
    }

    public int Line => _currentLine;
    public int Position => _cursor;

    public override string ToString() =>
        _text.AsSpan().Slice(_range.Start.Value, _range.End.Value - _range.Start.Value).ToString();

    public string ToString(int startPosition) =>
        _text.AsSpan().Slice(startPosition, _cursor - startPosition + 1).ToString();

    public bool NextChar()
    {
        if (_cursor >= _range.End.Value - 1) return false;

        _cursor++;
        return true;
    }

    public bool PeekNext(out char nextChar)
    {
        nextChar = '\0';

        if (_cursor + 1 >= _range.End.Value)
        {
            return false;
        }

        nextChar = _text.AsSpan()[_cursor + 1];
        return true;
    }

    public char PeekChar()
    {
        return _text.AsSpan()[_cursor];
    }
}