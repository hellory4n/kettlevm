using System;
using System.Collections.Generic;
namespace kettlevm;

static class Parser
{
    public static Class Parse(List<Token> tokens)
    {
        Class classy = new();
        int i = 0;

        // expect a class :D
        Token t = tokens[i];
        int e = t.Idx; // there's a lot of error messages
        if (t.Type == TokenType.Class) {
            i++;
        }
        else {
            Console.WriteLine($"{e}: File must have a class declaration");
            return classy;
        }
        
        if (t.Type == TokenType.Identifier) {
            #pragma warning disable CS8601 // shut up
            #pragma warning disable CS8600 // shut up
            classy.Name = (string)tokens[i + 1].Literal;
            #pragma warning restore CS8600 // shut up
            #pragma warning restore CS8601 // shut up
            i++;
        }
        else {
            Console.WriteLine($"{e}: Expected class name");
        }

        return classy;
    }
}