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
    /// *
    /// </summary>
    Asterisk,
    
    /// <summary>
    /// +
    /// </summary>
    Plus,
    
    /// <summary>
    /// -
    /// </summary>
    Minus,
    
    /// <summary>
    /// /
    /// </summary>
    Slash,
    
    /// <summary>
    /// End of code (or file)
    /// </summary>
    EndOfCode,
    
    /// <summary>
    /// ,
    /// </summary>
    Comma
}