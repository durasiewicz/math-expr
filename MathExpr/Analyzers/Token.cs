namespace MathExpr.Analyzers;

public record class Token(int Line,
    int Position,
    TokenType Type,
    string? Value = null)
{
    public static Token FromWindow(TextWindow window, TokenType type, string? value = null) =>
        new Token(window.Position, window.Line, type, value);
}
