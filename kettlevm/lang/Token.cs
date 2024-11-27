namespace kettlevm;

public struct Token() {
    public TokenTag Type { get; set; } = TokenTag.Unexpected;
    public int Line { get; set; } = -1;
    public string SourceLine { get; set; } = "";
    public ulong IntLit { get; set; } = 0;
    public double FloatLit { get; set; } = 0;
    public string StringLit { get; set; } = "";
    public char CharLit { get; set; } = '\0';
}

public enum TokenTag {
    Unexpected, Eof,

    True, False, Class, Fun, Msg, Static, Parf, If, Else, For, While,
    String, Uint, Int, Float, Bool, Char, Void, Any, Return, And, Or,
    Not, Continue, Break, Switch, Sync, Entity, Vec2, Vec3, Col,
    
    Identifier, FloatLit, IntLit, StringLit, CharLit,
    
    Lparen, Rparen, Lbracket, Rbracket, Lbrace, Rbrace,
    Comma, Colon, Semicolon, Dot, MinusArrow, EqArrow,
    
    PlusEq, MinusEq, StarEq, SlashEq, PercentEq, DotDotEq,
    GreaterEq, LessEq, EqualEq, NotEq,
    Plus, Minus, Star, Slash, Percent, DotDot,

    Greater, Less, Equal, Ampersand,
}