using System;
namespace kettlevm;

public static partial class Lexer {
    public static CompilerState lex(ref CompilerState c, string file)
    {
        c.file = file + "\0\0\0\0\0\0";
        for (c.lex_this = 0; c.lex_this < c.file.Length; c.lex_this++) {
            c = read_token(ref c);
        }
        return c;
    }

    static CompilerState read_token(ref CompilerState c)
    {
        char ch = advance(c);
        switch (ch) {
            // these are simple
            case '(': c.tokens.Enqueue(new(c, TokenType.lparen)); break;
            case ')': c.tokens.Enqueue(new(c, TokenType.rparen)); break;
            case '[': c.tokens.Enqueue(new(c, TokenType.lbracket)); break;
            case ']': c.tokens.Enqueue(new(c, TokenType.lbracket)); break;
            case '{': c.tokens.Enqueue(new(c, TokenType.lbrace)); break;
            case '}': c.tokens.Enqueue(new(c, TokenType.lbrace)); break;
            case ',': c.tokens.Enqueue(new(c, TokenType.comma)); break;
            case ':': c.tokens.Enqueue(new(c, TokenType.colon)); break;
            case ';': c.tokens.Enqueue(new(c, TokenType.semicolon)); break;

            // every operator with = (at least 2)
            case '+': c.tokens.Enqueue(match(c, '=') ? new(c, TokenType.pluseq) : new(c, TokenType.plus)); break;
            case '-': c.tokens.Enqueue(match(c, '=') ? new(c, TokenType.minuseq) : new(c, TokenType.minus)); break;
            case '*': c.tokens.Enqueue(match(c, '=') ? new(c, TokenType.stareq) : new(c, TokenType.star)); break;
            case '%': c.tokens.Enqueue(match(c, '=') ? new(c, TokenType.percenteq) : new(c, TokenType.percent)); break;
            case '=': c.tokens.Enqueue(match(c, '=') ? new(c, TokenType.equaleq) : new(c, TokenType.equal)); break;
            case '!': c.tokens.Enqueue(match(c, '=') ? new(c, TokenType.bangeq) : new(c, TokenType.bang)); break;
            case '>': c.tokens.Enqueue(match(c, '=') ? new(c, TokenType.greateq) : new(c, TokenType.great)); break;
            case '<': c.tokens.Enqueue(match(c, '=') ? new(c, TokenType.lesseq) : new(c, TokenType.less)); break;

            // / can also be comments
            case '/':
                if (match(c, '*')) {
                    Console.WriteLine("comment start :(");
                    // take that C
                    uint comments = 1;
                    while (true) {
                        Console.WriteLine($"lggllg {comments}");
                        if (peek(c) != '*' && peek_next(c) != '/') comments--;
                        if (peek(c) != '/' && peek_next(c) != '*') comments++;
                        if (comments == 0) break;
                        if (peek(c) == '\0') {
                            c.complain("Comment doesn't end");
                            break;
                        }
                        advance(c);
                    }
                }
                else c.tokens.Enqueue(match(c, '=') ? new(c, TokenType.slasheq) : new(c, TokenType.slash));
                break;
            
            // notorious whitespace
            case ' ': break;
            case '\t': break;
            case '\r': break;
            case '\n': c.lex_line++; break;

            default:
                //c.complain($"Unexpected character '{ch}'");
                break;
        };
        return c;
    }

    /// <summary>
    /// Pro-Level Chords
    /// </summary>
    static char advance(CompilerState c) => c.file[c.lex_this++];

    static bool match(CompilerState c, char expected)
    {
        if (c.file[c.lex_this] == '\0') return false;
        if (c.file[c.lex_this] != expected) return false;
        c.lex_this++;
        return true;
    }

    static char peek(CompilerState c) => c.file[c.lex_this];
    static char peek_next(CompilerState c) => c.file[c.lex_this + 1];

    public static void print_tokens(ref CompilerState c)
    {
        Console.Write("[");
        foreach (Token t in c.tokens) {
            Console.Write(t.type switch {
                TokenType.identifier => $"identifier = {t.strval}",
                TokenType.strlit => $"strlit = \"{t.strval}\"",
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