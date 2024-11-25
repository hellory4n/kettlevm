namespace kettlevm;

enum TokenType
{
    // brackets and shit
    // (    )       [         ]         {       }
    LParen, RParen, LBracket, RBracket, LBrace, RBrace,
    // . ,      :      ,
    Dot, Comma, Colon, Semicolon,

    // literals & shit
    Integer, Floating, Identifier, StringLit, CharLit,

    // operators
    // +  -      *     /      %        ..
    Plus, Minus, Star, Slash, Percent, DotDot,
    // +=      -=          *=         /=          %=            ..=
    PlusEqual, MinusEqual, StarEqual, SlashEqual, PercentEqual, DotDotEqual,

    // !  ||  &&
    Bang, Or, And,

    //    >  <     >=            <=         =      ==          !=
    Greater, Less, GreaterEqual, LessEqual, Equal, EqualEqual, BangEqual,

    // keywords (and or not are also keywords btw)
    True, False, Class, Fun, Msg, Static, Parf, If, Else, For, While, String, Int, Uint, Float, Bool, Char, Void,
    Return, Any,

    End
}