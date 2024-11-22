using System;
namespace kettlevm;

public static partial class Lexer {
    public static CompilerState lex(ref CompilerState c, string file)
    {
        c.file = file + "\0\0\0";
        for (int i = 0; i < c.file.Length; i++) {
            c = read_token(ref c);
        }
        return c;
    }

    static CompilerState read_token(ref CompilerState c)
    {
        char thisc = c.file[c.lex_i];
        char nextc = c.file[c.lex_i + 1];
        char nexterc = c.file[c.lex_i + 2];

        switch (thisc) {
            case '"': read_string(ref c); break;
            case '\'': read_char(ref c); break;

            // + +=
            case '+':
                if (nextc == '=') c.tokens.Enqueue(new(TokenType.pluseq));
                else c.tokens.Enqueue(new(TokenType.plus));
                break;
            
            // - -=
            case '-':
                if (nextc == '=') c.tokens.Enqueue(new(TokenType.minuseq));
                else c.tokens.Enqueue(new(TokenType.minus));
                break;
            
            // * *=
            case '*':
                if (nextc == '=') c.tokens.Enqueue(new(TokenType.asteriskeq));
                else c.tokens.Enqueue(new(TokenType.asterisk));
                break;
            
            // / /= /* */
            case '/':
                if (nextc == '=') c.tokens.Enqueue(new(TokenType.slasheq));
                else if (nextc == '*') {
                    // TODO big mistake!
                    while (true) {
                        if (nextc == '*' && nexterc == '/') break;
                        c.lex_i++;
                        thisc = c.file[c.lex_i];
                        nextc = c.file[c.lex_i + 1];
                        nexterc = c.file[c.lex_i + 2];
                    }
                }
                else c.tokens.Enqueue(new(TokenType.slash));
                break;
            
            // % %=
            case '%':
                if (nextc == '=') c.tokens.Enqueue(new(TokenType.percenteq));
                else c.tokens.Enqueue(new(TokenType.percent));
                break;
            
            // = ==
            case '=':
                if (nextc == '=') c.tokens.Enqueue(new(TokenType.equaleq));
                else c.tokens.Enqueue(new(TokenType.equal));
                break;
            
            // ! !=
            case '!':
                if (nextc == '=') c.tokens.Enqueue(new(TokenType.bangeq));
                else c.tokens.Enqueue(new(TokenType.bang));
                break;
            
            // > >=
            case '>':
                if (nextc == '=') c.tokens.Enqueue(new(TokenType.greateq));
                else c.tokens.Enqueue(new(TokenType.great));
                break;
            
            // < <=
            case '<':
                if (nextc == '=') c.tokens.Enqueue(new(TokenType.lesseq));
                else c.tokens.Enqueue(new(TokenType.less));
                break;
            
            // & &&
            case '&':
                if (nextc == '&') c.tokens.Enqueue(new(TokenType.ampersand2));
                else c.tokens.Enqueue(new(TokenType.ampersand));
                break;
            
            // | ||
            case '|':
                if (nextc == '|') c.tokens.Enqueue(new(TokenType.pipe2));
                else c.tokens.Enqueue(new(TokenType.pipe));
                break;
            
            // . ..
            case '.':
                if (nextc == '.') c.tokens.Enqueue(new(TokenType.dotdot));
                else c.tokens.Enqueue(new(TokenType.dot));
                break;
            
            // ,;:()[]{}
            case ',': c.tokens.Enqueue(new(TokenType.comma)); break;
            case ';': c.tokens.Enqueue(new(TokenType.semicolon)); break;
            case ':': c.tokens.Enqueue(new(TokenType.colon)); break;
            case '(': c.tokens.Enqueue(new(TokenType.lparen)); break;
            case ')': c.tokens.Enqueue(new(TokenType.rparen)); break;
            case '[': c.tokens.Enqueue(new(TokenType.lbracket)); break;
            case ']': c.tokens.Enqueue(new(TokenType.rbracket)); break;
            case '{': c.tokens.Enqueue(new(TokenType.lbrace)); break;
            case '}': c.tokens.Enqueue(new(TokenType.rbrace)); break;

            default:
                // numbers
                if (is_digit(thisc)) {
                    c = read_number(ref c);
                }
                // literals keywords
                else if (is_alphabetic(thisc) || is_digit(thisc) || thisc == '_') {
                    c = read_identifier(ref c);
                }
                else {
                    Console.WriteLine($"Unexpected character '{thisc}'");
                }

                break;
        }
        return c;
    }

    public static void print_tokens(ref CompilerState c)
    {
        Console.Write("[");
        foreach (Token t in c.tokens) {
            Console.Write(t.type switch {
                TokenType.identifier => $"(identifier = {t.strval})",
                TokenType.strlit => $"(strlit = {t.strval})",
                TokenType.intlit => $"(intlit = {t.intval})",
                TokenType.floatlit => $"(floatlit = {t.floatval})",
                TokenType.charlit => $"(charlit = {t.charval})",
                _ => $"({t.type})",
            });
        }
        Console.Write("]\n");
    }
}