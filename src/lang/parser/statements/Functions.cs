using System;
using System.Collections.Generic;
namespace kettlevm;

static partial class Parser
{
    public static (Function, int, int) ParseFunction(List<Token> tokens, int i)
    {
        Function fun = new();
        int errors = 0;
        Token t = tokens[i];
        int e = t.Idx; // there's a lot of error messages

        if (t.Type == TokenType.Msg) {
            fun.IsMessage = true;
        }
        else if (t.Type == TokenType.Fun) {
            fun.IsMessage = false;
        }
        i++;
        t = tokens[i];
        e = t.Idx;

        // get type :D
        if (t.Type == TokenType.Identifier) {
            var (typ, err, ii) = ParseType(tokens, i);
            errors += err;
            i = ii;
            fun.ReturnType = typ;
        }
        else {
            Console.WriteLine($"{e}: Expected return type");
            return (fun, errors, i);
        }

        return (fun, errors, i);
    }
}