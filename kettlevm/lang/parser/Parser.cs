using System;
using System.Collections.Generic;
namespace kettlevm;

public partial class Parser(List<Token> tokens, Lexer lexer)
{
    internal List<Token> tokens = tokens;
    public int Errors { get; private set; } = 0;
    internal int pos = 0;
    internal string filename = lexer.filename;
    internal string[] lines = lexer.lines;
    internal Token thist => tokens[pos];

    bool Match(TokenTag tag)
    {
        // that funny .. thing is just making a substring
        if (thist.Type == tag) {
            pos++;
            return true;
        }
        return false;
    }

    List<Token> ReadWhile(Func<Token, bool> condition, Action<Token> loopAction)
    {
        int start = pos;
        while (pos < tokens.Count && condition(thist)) {
            pos++;
            loopAction(thist);
        }
        // that funny .. thing is just making a substring
        List<Token> g = tokens[start..pos];
        return g;
    }

    void Error(string error) {
        // example:
        // kettleproj/main.ktl:69: Unexpected happening that happened unexpectedly.
        //     suffer_and_die();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{filename}:{tokens[pos].Line}: {error}");
        Console.ResetColor();
        Console.WriteLine($"    {tokens[pos].SourceLine}");
        Console.WriteLine();
        Errors++;
    }
}
