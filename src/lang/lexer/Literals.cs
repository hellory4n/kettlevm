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
        int start = c.lex_i;
        while (c.nextc() != '\0') {
            if (c.thisc() != '\\' && c.nextc() == '"') break;
            if (c.nextc() == '\n') c.thisline++;
            c.lex_i++;
        }

        if (c.thisc() == '\0') {
            c.complain("String doesn't end");
            return c;
        }

        // escape sequences lamo
        // the japanese character isn't used anywhere, https://en.wikipedia.org/wiki/Ghost_characters
        Console.WriteLine($"p {start} {c.lex_i}");
        // no idea why i have to subtract by 13, i just do
        string str = c.file.Substring(start + 1, c.lex_i - 13);
        Console.WriteLine($"gtgjgjgjgj {str}");
        str = str.Replace("\\\\", "垈");
        Console.WriteLine($"gtgjgjgjgj {str}");
        str = str.Replace("\\n", "\n");
        Console.WriteLine($"gtgjgjgjgj {str}");
        str = str.Replace("\\\"", "\"");
        Console.WriteLine($"gtgjgjgjgj {str}");
        // TODO make a function with 5 billion trillion escape sequences
        str = str.Replace("垈", "\\");
        Console.WriteLine($"gtgjgjgjgj {str}");

        c.tokens.Enqueue(new(TokenType.strlit) { strval = str });
        return c;
    }

    /// <summary>
    /// this is just read_string but it checks the length
    /// </summary>
    static CompilerState read_char(ref CompilerState c)
    {
        int start = c.lex_i;
        while (c.nextc() != '\0') {
            if (c.thisc() != '\\' && c.nextc() == '\'') break;
            if (c.nextc() == '\n') c.thisline++;
            c.lex_i++;
        }

        if (c.thisc() == '\0') {
            c.complain("Char doesn't end");
            return c;
        }

        // escape sequences lamo
        // the japanese character isn't used anywhere, https://en.wikipedia.org/wiki/Ghost_characters
        Console.WriteLine($"p {start} {c.lex_i}");
        // no idea why i have to subtract by 13, i just do
        string str = c.file.Substring(start + 1, c.lex_i - 13);
        Console.WriteLine($"gtgjgjgjgj {str}");
        str = str.Replace("\\\\", "垈");
        Console.WriteLine($"gtgjgjgjgj {str}");
        str = str.Replace("\\n", "\n");
        Console.WriteLine($"gtgjgjgjgj {str}");
        str = str.Replace("\\\"", "\"");
        Console.WriteLine($"gtgjgjgjgj {str}");
        // TODO make a function with 5 billion trillion escape sequences
        str = str.Replace("垈", "\\");
        Console.WriteLine($"gtgjgjgjgj {str}");

        if (str.Length == 1) c.tokens.Enqueue(new(TokenType.charlit) { charval = str[0] });
        else c.complain("Character must be 1 character, for text use strings (double quotes)");
        return c;
    }

    static CompilerState read_number(ref CompilerState c)
    {
        int start = c.lex_i;
        bool isfloat = false;
        while (c.nextc() != '\0') {
            if (!is_digit(c.thisc()) && c.thisc() != '.' && c.thisc() != '_') break;
            if (c.thisc() == '.') isfloat = true;
            c.lex_i++;
        }

        // no idea why i have to subtract by 13, i just do
        string numstr = c.file.Substring(start + 1, c.lex_i - 13);
        Console.WriteLine(numstr);
        numstr = numstr.Replace("_", "");

        // man
        try {
            if (isfloat) c.tokens.Enqueue(new(TokenType.floatlit)
                { floatval = double.Parse(numstr, CultureInfo.InvariantCulture) });
            else c.tokens.Enqueue(new(TokenType.intlit)
                { intval = ulong.Parse(numstr, CultureInfo.InvariantCulture) });
        }
        catch(Exception) {
            c.complain("Couldn't parse number");
        }
        return c;
    }
}