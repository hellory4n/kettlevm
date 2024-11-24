using System.Collections.Generic;
namespace kettlevm;

static class Keywords
{
    public static Dictionary<string, TokenType> KeyOfWords = new() {
        {"and", TokenType.And},
        {"or", TokenType.Or},
        {"not", TokenType.Bang},
        {"true", TokenType.True},
        {"false", TokenType.False},
        {"class", TokenType.Class},
        {"fun", TokenType.Fun},
        {"msg", TokenType.Msg},
        {"static", TokenType.Static},
        {"parf", TokenType.Parf}, // best keyword ever bestest of all time 10th best programming language when it comes to usability awards 3000 BC winner best programming language in the world awards 1921 winner
        {"if", TokenType.If},
        {"else", TokenType.Else},
        {"for", TokenType.For},
        {"while", TokenType.While},
        {"string", TokenType.String},
        {"int", TokenType.Int},
        {"uint", TokenType.Uint},
        {"float", TokenType.Float},
        {"bool", TokenType.Bool},
        {"char", TokenType.Char},
        {"void", TokenType.Void},
        {"return", TokenType.Return},
        {"any", TokenType.Any},
    };
}