namespace MathExpr.Analyzers;

public class Lexer
{
    public TokenCollection Lex(string input)
    {
        var result = new List<Token>();
        var textWindows = GenerateTextWindows(input).ToArray();

        foreach (var textWindow in textWindows)
        {
            foreach (var token in ScanTextWindow(textWindow))
            {
                result.Add(token);
            }
        }

        result.Add(new Token(0, 0, TokenType.EndOfCode));
        return new TokenCollection(result);
    }

    private IEnumerable<Token> ScanTextWindow(TextWindow window)
    {
        while (window.NextChar())
        {
            switch (window.PeekChar())
            {
                case '(':
                    yield return Token.FromWindow(window, TokenType.OpenRoundBracket);
                    break;

                case ')':
                    yield return Token.FromWindow(window, TokenType.CloseRoundBracket);
                    break;

                case '{':
                    yield return Token.FromWindow(window, TokenType.OpenCurlyBracket);
                    break;

                case '}':
                    yield return Token.FromWindow(window, TokenType.CloseCurlyBracket);
                    break;

                case ';':
                    yield return Token.FromWindow(window, TokenType.Semicolon);
                    break;

                case '\n':
                    yield return Token.FromWindow(window, TokenType.NewLine);
                    break;
                
                case ',':
                    yield return Token.FromWindow(window, TokenType.Comma);
                    break;
                
                case '.':
                    yield return Token.FromWindow(window, TokenType.Dot);
                    break;

                case '>':
                {
                    if (window.PeekNext(out var nextChar) && nextChar is '=')
                    {
                        yield return Token.FromWindow(window, TokenType.GreaterThanEquals);
                        window.NextChar();
                        continue;
                    }
                    
                    yield return Token.FromWindow(window, TokenType.GreaterThan);
                    
                    break;
                }
                
                case '<':
                {
                    if (window.PeekNext(out var nextChar) && nextChar is '=')
                    {
                        yield return Token.FromWindow(window, TokenType.LessThanEquals);
                        window.NextChar();
                        continue;
                    }
                    
                    yield return Token.FromWindow(window, TokenType.LessThan);
                    
                    break;
                }

                case '=':
                {
                    if (window.PeekNext(out var nextChar) && nextChar is '=')
                    {
                        yield return Token.FromWindow(window, TokenType.EqualsEquals);
                        window.NextChar();
                        continue;
                    }
                    
                    yield return Token.FromWindow(window, TokenType.Equals);
                    
                    break;
                }
                
                case '+':
                {
                    if (window.PeekNext(out var nextChar) && nextChar is '=')
                    {
                        yield return Token.FromWindow(window, TokenType.PlusEquals);
                        window.NextChar();
                        continue;
                    }
                    
                    yield return Token.FromWindow(window, TokenType.Plus);
                    
                    break;
                }
                
                case '-':
                {
                    if (window.PeekNext(out var nextChar) && nextChar is '=')
                    {
                        yield return Token.FromWindow(window, TokenType.MinusEquals);
                        window.NextChar();
                        continue;
                    }
                    
                    yield return Token.FromWindow(window, TokenType.Minus);
                    
                    break;
                }
                
                case '*':
                {
                    if (window.PeekNext(out var nextChar) && nextChar is '=')
                    {
                        yield return Token.FromWindow(window, TokenType.AsteriskEquals);
                        window.NextChar();
                        continue;
                    }
                    
                    yield return Token.FromWindow(window, TokenType.Asterisk);
                    
                    break;
                }
                
                case '/':
                {
                    if (window.PeekNext(out var nextChar) && nextChar is '=')
                    {
                        yield return Token.FromWindow(window, TokenType.SlashEquals);
                        window.NextChar();
                        continue;
                    }
                    
                    yield return Token.FromWindow(window, TokenType.Slash);
                    
                    break;
                }

                default:
                {
                    if (char.IsLetter(window.PeekChar()))
                    {
                        yield return ScanIdentifier(ref window);
                    }
                    else if (char.IsDigit(window.PeekChar()))
                    {
                        yield return ScanNumber(ref window);
                    }

                    break;
                }
            }
        }
    }

    private Token ScanNumber(ref TextWindow window)
    {
        var startPosition = window.Position;

        do
        {
            if (!window.PeekNext(out var nextCharacter) || !char.IsDigit(nextCharacter))
            {
                return new Token(window.Line, startPosition, TokenType.Number, window.ToString(startPosition));
            }
        } while (window.NextChar());

        throw new Exception();
    }

    private Token ScanIdentifier(ref TextWindow window)
    {
        var startPosition = window.Position;

        do
        {
            if (!window.PeekNext(out var nextCharacter) || !char.IsLetter(nextCharacter))
            {
                return new Token(window.Line, startPosition, TokenType.Identifier, window.ToString(startPosition));
            }
        } while (window.NextChar());

        throw new Exception();
    }

    private IEnumerable<TextWindow> GenerateTextWindows(string input)
    {
        var currentLine = 0;
        var lastWindowEndIndex = 0;

        for (var currentIndex = 0; currentIndex < input.Length; currentIndex++)
        {
            var currentChar = input[currentIndex];

            if (currentChar is ';' or '\n' || currentIndex + 1 == input.Length)
            {
                yield return new TextWindow(input, lastWindowEndIndex..(currentIndex + 1), currentLine);
                lastWindowEndIndex = currentIndex + 1;

                if (currentChar is '\n')
                {
                    currentLine++;
                }
            }
        }
    }
}