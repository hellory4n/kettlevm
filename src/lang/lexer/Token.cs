namespace kettlevm;

/// <summary>
/// it's a token :D
/// </summary>
public struct Token(CompilerState c, TokenType type)
{
    public int start { get; set; } = c.lex_start;
    public int end { get; set; } = c.lex_this;
    public int line { get; set; } = c.lex_line;
    public string strval { get; set; } = "";
    public char charval { get; set; } = '\0';
    public ulong intval { get; set; } = 0;
    public double floatval { get; set; } = 0;
    public TokenType type { get; set; } = type;
}

public enum TokenType {
    none,

    identifier,
    strlit,
    charlit,
    intlit,
    floatlit,

    plus,       // +
    pluseq,     // +=
    minus,      // -
    minuseq,    // -=
    star,       // *
    stareq,     // *=
    slash,      // /
    slasheq,    // /=
    percent,    // %
    percenteq,  // %=
    equal,      // =
    equaleq,    // ==
    bang,       // !
    bangeq,     // !=
    ampersand,  // &
    pipe,       // |
    ampersand2, // &&
    pipe2,      // ||
    great,      // >
    greateq,    // >=
    less,       // <
    lesseq,     // <=
    semicolon,  // ;
    colon,      // :
    dot,        // .
    comma,      // ,
    lparen,     // (
    rparen,     // )
    lbracket,   // [
    rbracket,   // ]
    lbrace,     // {
    rbrace,     // }
    dotdot,     // ..

    ktrue,
    kfalse,
    kclass,
    kfun,
    kmsg,
    kstatic,
    kparf,
    kif,
    kelse,
    kfor,
    kwhile,
    kstring,
    kint,
    kuint,
    kfloat,
    kbool,
    kchar,
    kvoid,
    kreturn,
}