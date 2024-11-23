namespace kettlevm;

public static partial class Lexer {
    /// <summary>
    /// returns an ID token if it's not a keyword
    /// </summary>
    public static Token read_keyword(CompilerState c, string id)
    {
        return id switch {
            "true" => new(c, TokenType.ktrue),
            "false" => new(c, TokenType.kfalse),
            "class" => new(c, TokenType.kclass),
            "fun" => new(c, TokenType.kfun),
            "msg" => new(c, TokenType.kmsg),
            "static" => new(c, TokenType.kstatic),
            "parf" => new(c, TokenType.kparf),
            "if" => new(c, TokenType.kif),
            "else" => new(c, TokenType.kelse),
            "for" => new(c, TokenType.kfor),
            "string" => new(c, TokenType.kstring),
            "int" => new(c, TokenType.kint),
            "uint" => new(c, TokenType.kuint),
            "float" => new(c, TokenType.kfloat),
            "bool" => new(c, TokenType.kbool),
            "char" => new(c, TokenType.kchar),
            "void" => new(c, TokenType.kvoid),
            "return" => new(c, TokenType.kreturn),
            _ => new() {
                strval = id,
            }
        };
    }
}