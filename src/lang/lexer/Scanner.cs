using System.Collections.Generic;
using System.Globalization;
namespace kettlevm;

static class Scanner
{
    public static List<Token> Scan(string text)
    {
        // so we don't have to deal with this every single time we want to look at the next character
        text += '\0';
        List<Token> tokens = [];

        // we use a normal for loop since it allows changing the index and looking at the next character
        for (int i = 0; i < text.Length; i++) {
            char c = text[i];

            switch (c) {
                // these are just 1 character
                case '(': tokens.Add(new Token { Type = TokenType.LParen }); break;
                case ')': tokens.Add(new Token { Type = TokenType.RParen }); break;
                case '[': tokens.Add(new Token { Type = TokenType.LBracket }); break;
                case ']': tokens.Add(new Token { Type = TokenType.RBracket }); break;
                case '{': tokens.Add(new Token { Type = TokenType.LBrace }); break;
                case '}': tokens.Add(new Token { Type = TokenType.RBrace }); break;
                case ',': tokens.Add(new Token { Type = TokenType.Comma }); break;
                case ':': tokens.Add(new Token { Type = TokenType.Colon }); break;
                case ';': tokens.Add(new Token { Type = TokenType.Semicolon }); break;
                
                // dumb operators that can have = at the end
                case '+':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.PlusEqual });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Plus });
                    }
                    break;
                
                case '-':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.MinusEqual });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Minus });
                    }
                    break;
                
                case '*':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.StarEqual });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Star });
                    }
                    break;
                
                case '%':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.PercentEqual });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Percent });
                    }
                    break;
                
                case '=':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.EqualEqual });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Equal });
                    }
                    break;

                case '!':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.BangEqual });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Bang });
                    }
                    break;
                
                case '<':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.LessEqual });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Less });
                    }
                    break;
                
                case '>':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.GreaterEqual });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Greater });
                    }
                    break;
                
                // more dumb
                case '&':
                    if (text[i + 1] == '&') {
                        tokens.Add(new Token { Type = TokenType.And });
                        i++;
                    }
                    break;
                
                case '|':
                    if (text[i + 1] == '|') {
                        tokens.Add(new Token { Type = TokenType.Or });
                        i++;
                    }
                    break;
                
                // . is a bit fucky since .. is for string concatenation
                case '.':
                    if (text[i + 1] == '.') {
                        if (text[i + 2] == '=') {
                            tokens.Add(new Token { Type = TokenType.DotDotEqual });
                            i += 2;
                        }
                        else {
                            tokens.Add(new Token { Type = TokenType.DotDot });
                            i++;
                        }
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Dot });
                    }
                    break;

                // numbers and identifiers and stuff are a bit trickier
                default:
                    if (ScannerUtils.IsDigit(c)) {
                        bool isFloat = false;
                        string thing = c.ToString();

                        while (ScannerUtils.IsDigit(text[i + 1])) {
                            i++;
                            thing += text[i];
                        }

                        // now check if it's actually a float
                        if (text[i + 1] == '.') {
                            isFloat = true;
                            i++;
                            thing += '.';

                            // and do that shit again
                            while (ScannerUtils.IsDigit(text[i + 1])) {
                                i++;
                                thing += text[i];
                            }
                        }

                        // then we actually add the token
                        if (isFloat) {
                            tokens.Add(new Token {
                                Type = TokenType.Float,
                                Literal = double.Parse(thing, CultureInfo.InvariantCulture)
                            });
                        }
                        else {
                            tokens.Add(new Token {
                                Type = TokenType.Integer,
                                Literal = int.Parse(thing, CultureInfo.InvariantCulture)
                            });
                        }
                    }

                    // handle keywords/identifiers
                    if (ScannerUtils.IsLetter(c) || c == '_') {
                        string id = "";

                        while (ScannerUtils.IsValidId(text[i + 1])) {
                            id += text[i];
                            i++;
                        }
                        id += text[i];

                        if (Keywords.KeyOfWords.TryGetValue(id, out TokenType epicAmazingFantasticMajesticToken)) {
                            tokens.Add(new Token {
                                Type = epicAmazingFantasticMajesticToken
                            });
                        }
                        else {
                            tokens.Add(new Token {
                                Type = TokenType.Identifier,
                                Literal = id
                            });
                        }
                    }
                    break;
            }
        }

        return tokens;
    }
}