using System;
using System.Globalization;
namespace kettlevm;

public static partial class Lexer {
    static CompilerState read_identifier(ref CompilerState c)
    {
        string id = "";

        while (is_alphabetic(c.nextc()) || is_digit(c.nextc()) || c.nextc() == '_') {
            id += c.thisc();
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
        string str = "";

        while (c.nextc() != '"') {
            // escape stuff
            if (c.nextc() == '\\') {
                c.lex_i++;
                str += c.thisc();
            }
            else {
                str += c.thisc();
            }
            c.lex_i++;
        }

        c.tokens.Enqueue(new(TokenType.strlit) { strval = str });
        return c;
    }

    static CompilerState read_char(ref CompilerState c)
    {
        string str = "";

        while (c.nextc() != '\'') {
            // escape stuff
            if (c.nextc() == '\\') {
                c.lex_i++;
                str += c.thisc();
            }
            else {
                str += c.thisc();
            }
            c.lex_i++;
        }

        if (str.Length == 1) c.tokens.Enqueue(new(TokenType.charlit) { strval = str });
        else c.complain("Characters must be 1 character");
        return c;
    }

    static CompilerState read_number(ref CompilerState c)
    {
        bool isfloat = false;
        string numstr = "";
        
        // _ is supported so you can write really big numbers like 1_000_000
        while (is_digit(c.nextc()) || c.nextc() == '_') {
            numstr += c.thisc();
            if (c.nextc() == '.') {
                c.lex_i++;
                isfloat = true;
            }
            c.lex_i++;
        }

        // we just parsed a string, strings are in fact not numbers
        numstr = numstr.Replace("_", "");
        if (isfloat) {
            c.tokens.Enqueue(new(TokenType.floatlit) { floatval = double.Parse(numstr, CultureInfo.InvariantCulture) });
        }
        else {
            c.tokens.Enqueue(new(TokenType.intlit) { intval = ulong.Parse(numstr, CultureInfo.InvariantCulture) });
        }
        return c;
    }
}