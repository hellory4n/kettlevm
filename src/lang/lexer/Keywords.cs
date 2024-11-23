using System.Collections.Generic;
namespace kettlevm;

static class Keywords
{
    public static Dictionary<string, TokenType> KeyOfWords = new() {
        {"and", TokenType.And},
        {"or", TokenType.Or},
        {"not", TokenType.Bang},
        {"true", TokenType.True},
        {"false", TokenType.False}
    };
}