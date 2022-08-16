namespace Enterpreter;

public class Token
{
    public readonly Type Type;
    public object? Value;
    
    public Token(Type type, object? value)
    {
        Type = type;
        Value = value;
    }

    public override string ToString()
    {
        return $"Token({Type}, {Value})";
    }
}