namespace Enterpreter;

public class Interpreter
{
    private readonly string _text;
    private int _pos;
    private Token _currentToken;
    private char _currentChar;

    public Interpreter(string text)
    {
        _text = text;
        _pos = 0;
        _currentChar = text[_pos];
    }

    private object Term()
    {
        var token = _currentToken;
        Eat(Type.Integer);
        return token.Value;
    }

    private void Advance()
    {
        _pos += 1;
        if (_pos > _text.Length - 1)
        {
            _currentChar = '\0';
        }
        else
        {
            _currentChar = _text[_pos];
        }
    }

    private void SkipWhitespace()
    {
        while (_currentChar is not '\0' && Char.IsWhiteSpace(_currentChar))
        {
            Advance();
        }
    }

    private int Integer()
    {
        var result = "";
        while (_currentChar is not '\0' && Char.IsDigit(_currentChar))
        {
            result += _currentChar;
            Advance();
        }

        return int.Parse(result);
    }

    private Token NextToken()
    {

        while (_currentChar is not '\0')
        {
            if (Char.IsWhiteSpace(_currentChar))
            {
                SkipWhitespace();
                continue;
            }

            if (Char.IsDigit(_currentChar))
            {
                return new Token(Type.Integer, Integer());
            }

            if (_currentChar == '+')
            {
                Advance();
                return new Token(Type.Plus, '+');
            }
            
            if (_currentChar == '-')
            {
                Advance();
                return new Token(Type.Minus, '-');
            }
            
            if (_currentChar == '*')
            {
                Advance();
                return new Token(Type.Factor, '*');
            }
            
            if (_currentChar == '/')
            {
                Advance();
                return new Token(Type.Divide, '/');
            }
            
            throw new Exception($"Error while parsing token. Given value doesn't have a corresponding token.");
        }
       
        return new Token(Type.Eof, Enumerable.Empty<object>());
    }

    private void Eat(Type type)
    {
        if (_currentToken.Type == type)
        {
            _currentToken = NextToken();
        }
        else
        {
            throw new Exception("Error parsing input.");
        }
    }

    public int Expr()
    {
        _currentToken = NextToken();
        int result = (int)Term();
        while (
            _currentToken.Type == Type.Plus || 
            _currentToken.Type == Type.Minus ||
            _currentToken.Type == Type.Divide ||
            _currentToken.Type == Type.Factor
            )
        {
            var token = _currentToken;
            if (token.Type == Type.Plus)
            {
                Eat(Type.Plus);
                result += (int)Term();
            } 
            else if (token.Type == Type.Minus)
            {
                Eat(Type.Minus);
                result -= (int)Term();
            }
            else if (token.Type == Type.Divide)
            {
                Eat(Type.Divide);
                result /= (int)Term();
            }
            else if (token.Type == Type.Factor)
            {
                Eat(Type.Factor);
                result *= (int)Term();
            }
        }
        return result;
    }
}