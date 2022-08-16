namespace Enterpreter;

public class Interpreter
{
    public string Text;
    public int Pos;
    public Token CurrentToken;
    public char? CurrentChar;

    public Interpreter(string text)
    {
        Text = text;
        Pos = 0;
        CurrentChar = text[Pos];
    }

    public void Advance()
    {
        Pos += 1;
        if (Pos > Text.Length - 1)
        {
            CurrentChar = null;
        }
        else
        {
            CurrentChar = Text[Pos];
        }
    }

    public void SkipWhitespace()
    {
        while (CurrentChar is not null && Char.IsWhiteSpace((char) CurrentChar))
        {
            Advance();
        }
    }

    public int Integer()
    {
        var result = "";
        while (CurrentChar is not null && Char.IsDigit((char) CurrentChar))
        {
            result += CurrentChar;
            Advance();
        }

        return int.Parse(result);
    }
    
    public Token NextToken()
    {

        while (CurrentChar is not null)
        {
            if (Char.IsWhiteSpace((char) CurrentChar))
            {
                SkipWhitespace();
                continue;
            }

            if (Char.IsDigit((char) CurrentChar))
            {
                return new Token(Type.INTEGER, Integer());
            }

            if (CurrentChar == '+')
            {
                Advance();
                return new Token(Type.PLUS, '+');
            }
            
            if (CurrentChar == '-')
            {
                Advance();
                return new Token(Type.MINUS, '-');
            }
            
            throw new Exception("Error parsing input.");
        }

        return new Token(Type.EOF, null);
    }

    public void Eat(Type type)
    {
        if (CurrentToken.Type == type)
        {
            CurrentToken = NextToken();
        }
        else
        {
            throw new Exception("Error parsing input.");
        }
    }

    public int Expr()
    {
        CurrentToken = NextToken();

        var left = CurrentToken;
        Eat(Type.INTEGER);

        var op = CurrentToken;
        if (op.Type == Type.PLUS)
        {
            Eat(Type.PLUS);
        }
        else
        {
            Eat(Type.MINUS);
        }

        var right = CurrentToken;
        Eat(Type.INTEGER);

        
        if (left.Value != null || right.Value != null)
        {
            if (op.Type == Type.PLUS)
            {
                var _result = (int)(left.Value) + (int)(right.Value);
                return _result;
            }
            else
            {
                var _result = (int)(left.Value) - (int)(right.Value);
                return _result;
            }
            
        }
        
        throw new Exception("Token value null.");
    }
}