// See https://aka.ms/new-console-template for more information

using Enterpreter;

string? _text;

while (true)
{
    try
    {
        _text = Console.ReadLine();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }

    var interpreter = new Interpreter(_text);
    var result = interpreter.Expr();
    Console.WriteLine(result);

}