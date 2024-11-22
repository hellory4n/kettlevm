namespace kettlevm;

/// <summary>
/// it's a token :D
/// </summary>
public struct Token(TokenType type) {
    public string strval { get; set; } = "";
    public char charval { get; set; } = '\0';
    public uint intval { get; set; } = 0;
    public double floatval { get; set; } = 0;
    public TokenType type { get; set; } = type;
}

public enum TokenType {
    identifier,
    strlit,
    charlit,
    intlit,
    floatlit,

    plus,       // +
    pluseq,     // +=
    minus,      // -
    minuseq,    // -=
    asterisk,   // *
    asteriskeq, // *=
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