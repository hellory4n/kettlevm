using System;
namespace kettlevm;

public static partial class Lexer {
    static CompilerState read_identifier(ref CompilerState c)
    {
        string id = "";
        char nextc = c.file[c.lex_i + 1];

        while (is_alphabetic(nextc) || is_digit(nextc) || nextc == '_') {
            id += c.file[c.lex_i];
            nextc = c.file[c.lex_i + 1];
            c.lex_i++;
        }

        // read_keyword returns an id token if it's not a keyword
        Token nowthatlookslikeaj = read_keyword(id);
        c.tokens.Enqueue(nowthatlookslikeaj.type switch {
            TokenType.identifier => new(TokenType.identifier) { strval = id },
            _ => nowthatlookslikeaj
        });
        return c;
    }

    static CompilerState read_string(ref CompilerState c)
    {
        
        return c;
    }
}