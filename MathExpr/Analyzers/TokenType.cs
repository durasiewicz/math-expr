// ReSharper disable InvalidXmlDocComment
namespace MathExpr.Analyzers;

public enum TokenType
{
    /// <summary>
    /// (
    /// </summary>
    OpenRoundBracket,
    
    /// <summary>
    /// )
    /// </summary>
    CloseRoundBracket,
    
    /// <summary>
    /// {
    /// </summary>
    OpenCurlyBracket,
    
    /// <summary>
    /// }
    /// </summary>
    CloseCurlyBracket,
    
    /// <summary>
    /// abc
    /// </summary>
    Identifier,
    
    /// <summary>
    /// 123
    /// </summary>
    Number,
    
    /// <summary>
    /// ;
    /// </summary>
    Semicolon,
    
    /// <summary>
    /// \n
    /// </summary>
    NewLine,
    
    /// <summary>
    /// =
    /// </summary>
    Equals,
    
    /// <summary>
    /// ==
    /// </summary>
    EqualsEquals,
    
    /// <summary>
    /// >
    /// </summary>
    GreaterThan,
    
    /// <summary>
    /// >=
    /// </summary>
    GreaterThanEquals,
    
    /// <summary>
    /// <
    /// </summary>
    LessThan,
    
    /// <summary>
    /// <=
    /// </summary>
    LessThanEquals,
    
    /// <summary>
    /// !=
    /// </summary>
    ExclamationEquals,
    
    /// <summary>
    /// *
    /// </summary>
    Asterisk,
    
    /// <summary>
    /// *=
    /// </summary>
    AsteriskEquals,
    
    /// <summary>
    /// +
    /// </summary>
    Plus,
    
    /// <summary>
    /// PlusEquals
    /// </summary>
    PlusEquals,
    
    /// <summary>
    /// -
    /// </summary>
    Minus,
    
    /// <summary>
    /// -=
    /// </summary>
    MinusEquals,
    
    /// <summary>
    /// /
    /// </summary>
    Slash,
    
    /// <summary>
    /// /=
    /// </summary>
    SlashEquals,
    
    /// <summary>
    /// End of code (or file)
    /// </summary>
    EndOfCode,
    
    /// <summary>
    /// ,
    /// </summary>
    Comma,
    
    /// <summary>
    /// .
    /// </summary>
    Dot
}