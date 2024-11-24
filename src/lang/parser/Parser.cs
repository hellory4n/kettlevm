using System;
using System.Collections.Generic;
namespace kettlevm;

static partial class Parser
{
    public static (Class, int) Parse(List<Token> tokens)
    {
        // haha
        tokens.Add(new Token { Type = TokenType.End });
        tokens.Add(new Token { Type = TokenType.End });
        tokens.Add(new Token { Type = TokenType.End });
        tokens.Add(new Token { Type = TokenType.End });
        tokens.Add(new Token { Type = TokenType.End });
        tokens.Add(new Token { Type = TokenType.End });
        
        Class classy = new();
        int i = 0;
        int errors = 0;

        // expect a class :D
        Token t = tokens[i];
        int e = t.Idx; // there's a lot of error messages
        if (t.Type == TokenType.Class) {
            i++;
            t = tokens[i];
            e = t.Idx;
        }
        else {
            Console.WriteLine($"{e}: File must have a class declaration");
            return (classy, errors);
        }
        
        if (t.Type == TokenType.Identifier) {
            #pragma warning disable CS8601 // shut up
            #pragma warning disable CS8600 // shut up
            classy.Name = (string)tokens[i + 1].Literal;
            #pragma warning restore CS8600 // shut up
            #pragma warning restore CS8601 // shut up
            i++;
            t = tokens[i];
            e = t.Idx;
        }
        else {
            Console.WriteLine($"{e}: Expected class name");
            return (classy, errors);
        }

        for (; i < tokens.Count; i++) {
            t = tokens[i];
            e = t.Idx;
            if (t.Type == TokenType.Msg || t.Type == TokenType.Fun) {
                var (fun, err, ii) = ParseFunction(tokens, i);
                errors += err;
                i = ii;
                classy.Methods.Enqueue(fun);
            }
        }

        return (classy, errors);
    }
}