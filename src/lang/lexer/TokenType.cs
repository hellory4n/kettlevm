namespace kettlevm;

enum TokenType
{
    // brackets and shit
    // (    )       [         ]         {       }
    LParen, RParen, LBracket, RBracket, LBrace, RBrace,
    // . ,      :      ,
    Dot, Comma, Colon, Semicolon,

    // literals & shit
    Integer, Float, Identifier, StringLit, CharLit,

    // operators
    // +  -      *     /      %        ..
    Plus, Minus, Star, Slash, Percent, DotDot,
    // +=      -=          *=         /=          %=            ..=
    PlusEqual, MinusEqual, StarEqual, SlashEqual, PercentEqual, DotDotEqual,

    // !  ||  &&
    Bang, Or, And,

    //    >  <     >=            <=         =      ==          !=
    Greater, Less, GreaterEqual, LessEqual, Equal, EqualEqual, BangEqual,

    // exclusively used by the parser
    Negate,

    // keywords
    True, False,

    End
}