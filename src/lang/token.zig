const TokenTag = enum {
    identifier,
    string,
    char,
    int,
    uint,
    float,

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
};

const Token = union(TokenTag) {
    identifier: []const u8,
    string: []const u8,
    char: u8,
    int: i64,
    uint: u64,
    float: f64,
};
