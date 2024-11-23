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
                // pretty easy to parse those
                case '+': tokens.Add(new Token { Type = TokenType.Plus }); break;
                case '-': tokens.Add(new Token { Type = TokenType.Minus }); break;
                case '*': tokens.Add(new Token { Type = TokenType.Star }); break;
                case '/': tokens.Add(new Token { Type = TokenType.Slash }); break;
                case '%': tokens.Add(new Token { Type = TokenType.Percent }); break;
                case '(': tokens.Add(new Token { Type = TokenType.LParen }); break;
                case ')': tokens.Add(new Token { Type = TokenType.RParen }); break;

                // dumb operators that can also be keywords
                case '&':
                    if (text[i + 1] == '&') {
                        tokens.Add(new Token { Type = TokenType.And });
                    }
                    break;
                
                case '|':
                    if (text[i + 1] == '|') {
                        tokens.Add(new Token { Type = TokenType.Or });
                    }
                    break;
                
                // =
                case '=':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.EqualEqual });
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Equal });
                    }
                    break;

                case '!':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.BangEqual });
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Bang });
                    }
                    break;
                
                case '<':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.LessEqual });
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Less });
                    }
                    break;
                
                case '>':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.GreaterEqual });
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Greater });
                    }
                    break;

                // numbers are a bit trickier
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