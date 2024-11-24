using System;
using System.Collections.Generic;
namespace kettlevm;

static partial class Parser
{
    public static (TypeThing, int, int) ParseType(List<Token> tokens, int i)
    {
        TypeThing type = new();
        int errors = 0;
        Token t = tokens[i];
        int e = t.Idx; // there's a lot of error messages

        switch (t.Type) {
            case TokenType.Identifier: type.Name = (string)(t.Literal ?? ""); break;
            case TokenType.Void: type.TypeType = PrimitiveType.Void; break;
            case TokenType.Any: type.TypeType = PrimitiveType.Any; break;
            case TokenType.Int: type.TypeType = PrimitiveType.Int; break;
            case TokenType.Uint: type.TypeType = PrimitiveType.Uint; break;
            case TokenType.Float: type.TypeType = PrimitiveType.Float; break;
            case TokenType.Char: type.TypeType = PrimitiveType.Char; break;
            case TokenType.String: type.TypeType = PrimitiveType.String; break;
            case TokenType.Bool: type.TypeType = PrimitiveType.Bool; break;

            default:
                Console.WriteLine($"{e}: Expected type name");
                return (type, errors, i);
        }
        i++;
        t = tokens[i];
        e = t.Idx;

        // now check if there's arrays
        while (tokens[i].Type == TokenType.LBracket && tokens[i + 1].Type == TokenType.RBracket) {
            i += 2;
            type.Arrays++;
        }

        // and pointers
        while (tokens[i].Type == TokenType.Star) {
            i++;
            type.Pointers++;
        }
        
        return (type, errors, i);
    }
}