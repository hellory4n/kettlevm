using System;
namespace kettlevm;

public static partial class Lexer {
    public static CompilerState lex(ref CompilerState c, string file)
    {
        c.file = file + "\0\0\0\0\0\0";
        for (; c.lex_i < c.file.Length; c.lex_i++) {
            c = read_token(ref c);
        }
        return c;
    }

    static CompilerState read_token(ref CompilerState c)
    {
        switch (c.thisc()) {
            //case '"': read_string(ref c); break;
            case '\'': read_char(ref c); break;

            // ignore whitespace
            case ' ': break;
            case '\t': break;
            case '\r': break;
            case '\n': c.thisline++; break;
            case '\0': return c;

            // + +=
            case '+':
                if (c.nextc() == '=') {
                    c.tokens.Enqueue(new(TokenType.pluseq));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.plus));
                break;
            
            // - -=
            case '-':
                if (c.nextc() == '=') {
                    c.tokens.Enqueue(new(TokenType.minuseq));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.minus));
                break;
            
            // * *=
            case '*':
                if (c.nextc() == '=') {
                    c.tokens.Enqueue(new(TokenType.asteriskeq));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.asterisk));
                break;
            
            // / /= /* */
            case '/':
                if (c.nextc() == '=') {
                    c.tokens.Enqueue(new(TokenType.slasheq));
                    c.lex_i++;
                }
                else if (c.nextc() == '*') {
                    // TODO big mistake!
                    while (true) {
                        if (c.nextc() == '*' && c.nexterc() == '/') break;
                        // the lexer inserts those at the end
                        if (c.nextc() == '\0') {
                            c.complain("Multi-line comment doesn't end");
                            break;
                        }
                        if (c.nextc() == '\n') c.thisline++;
                        c.lex_i++;
                    }
                }
                else c.tokens.Enqueue(new(TokenType.slash));
                break;
            
            // % %=
            case '%':
                if (c.nextc() == '=') {
                    c.tokens.Enqueue(new(TokenType.percenteq));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.percent));
                break;
            
            // = ==
            case '=':
                if (c.nextc() == '=') {
                    c.tokens.Enqueue(new(TokenType.equaleq));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.equal));
                break;
            
            // ! !=
            case '!':
                if (c.nextc() == '=') {
                    c.tokens.Enqueue(new(TokenType.bangeq));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.bang));
                break;
            
            // > >=
            case '>':
                if (c.nextc() == '=') {
                    c.tokens.Enqueue(new(TokenType.greateq));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.great));
                break;
            
            // < <=
            case '<':
                if (c.nextc() == '=') {
                    c.tokens.Enqueue(new(TokenType.lesseq));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.less));
                break;
            
            // & &&
            case '&':
                if (c.nextc() == '&') {
                    c.tokens.Enqueue(new(TokenType.ampersand2));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.ampersand));
                break;
            
            // | ||
            case '|':
                if (c.nextc() == '|') {
                    c.tokens.Enqueue(new(TokenType.pipe2));
                    c.lex_i++;
                }
                else c.tokens.Enqueue(new(TokenType.pipe));
                break;
            
            // . ..
            case '.':
                if (c.nextc() == '.') {
                    c.tokens.Enqueue(new(TokenType.dotdot));
                    c.lex_i++;
                }
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
                /*// numbers
                if (is_digit(c.thisc())) {
                    c = read_number(ref c);
                }
                // literals keywords
                else if (is_alphabetic(c.thisc()) || is_digit(c.thisc()) || c.thisc() == '_') {
                    c = read_identifier(ref c);
                }
                else {
                    c.complain($"Unexpected character '{c.thisc()}'");
                }*/

                break;
        }
        return c;
    }

    public static void print_tokens(ref CompilerState c)
    {
        Console.Write("[");
        foreach (Token t in c.tokens) {
            Console.Write(t.type switch {
                TokenType.identifier => $"identifier = {t.strval}",
                TokenType.strlit => $"strlit = {t.strval}",
                TokenType.intlit => $"intlit = {t.intval}",
                TokenType.floatlit => $"floatlit = {t.floatval}",
                TokenType.charlit => $"charlit = {t.charval}",
                _ => $"{t.type}",
            });
            Console.Write(", ");
        }
        Console.Write("]\n");
    }
}