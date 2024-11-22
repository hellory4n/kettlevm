namespace kettlevm;

public static partial class Lexer {
    /// <summary>
    /// returns an ID token if it's not a keyword
    /// </summary>
    public static Token read_keyword(string id)
    {
        return id switch {
            "true" => new(TokenType.ktrue),
            "false" => new(TokenType.kfalse),
            "class" => new(TokenType.kclass),
            "fun" => new(TokenType.kfun),
            "msg" => new(TokenType.kmsg),
            "static" => new(TokenType.kstatic),
            "parf" => new(TokenType.kparf),
            "if" => new(TokenType.kif),
            "else" => new(TokenType.kelse),
            "for" => new(TokenType.kfor),
            "string" => new(TokenType.kstring),
            "int" => new(TokenType.kint),
            "uint" => new(TokenType.kuint),
            "float" => new(TokenType.kfloat),
            "bool" => new(TokenType.kbool),
            "char" => new(TokenType.kchar),
            "void" => new(TokenType.kvoid),
            "return" => new(TokenType.kreturn),
            _ => new() {
                strval = id,
            }
        };
    }
}