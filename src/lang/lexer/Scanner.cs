using System;
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
                case '(': tokens.Add(new Token { Type = TokenType.LParen, Idx = i }); break;
                case ')': tokens.Add(new Token { Type = TokenType.RParen, Idx = i }); break;
                case '[': tokens.Add(new Token { Type = TokenType.LBracket, Idx = i }); break;
                case ']': tokens.Add(new Token { Type = TokenType.RBracket, Idx = i }); break;
                case '{': tokens.Add(new Token { Type = TokenType.LBrace, Idx = i }); break;
                case '}': tokens.Add(new Token { Type = TokenType.RBrace, Idx = i }); break;
                case ',': tokens.Add(new Token { Type = TokenType.Comma, Idx = i }); break;
                case ':': tokens.Add(new Token { Type = TokenType.Colon, Idx = i }); break;
                case ';': tokens.Add(new Token { Type = TokenType.Semicolon, Idx = i }); break;
                
                // dumb operators that can have = at the end
                case '+':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.PlusEqual, Idx = i });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Plus, Idx = i });
                    }
                    break;
                
                case '-':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.MinusEqual, Idx = i });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Minus, Idx = i });
                    }
                    break;
                
                case '*':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.StarEqual, Idx = i });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Star, Idx = i });
                    }
                    break;
                
                case '%':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.PercentEqual, Idx = i });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Percent, Idx = i });
                    }
                    break;
                
                case '=':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.EqualEqual, Idx = i });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Equal, Idx = i });
                    }
                    break;

                case '!':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.BangEqual, Idx = i });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Bang, Idx = i });
                    }
                    break;
                
                case '<':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.LessEqual, Idx = i });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Less, Idx = i });
                    }
                    break;
                
                case '>':
                    if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.GreaterEqual, Idx = i });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Greater, Idx = i });
                    }
                    break;
                
                // more dumb
                case '&':
                    if (text[i + 1] == '&') {
                        tokens.Add(new Token { Type = TokenType.And, Idx = i });
                        i++;
                    }
                    break;
                
                case '|':
                    if (text[i + 1] == '|') {
                        tokens.Add(new Token { Type = TokenType.Or, Idx = i });
                        i++;
                    }
                    break;
                
                // . is a bit fucky since .. is for string concatenation
                case '.':
                    if (text[i + 1] == '.') {
                        if (text[i + 2] == '=') {
                            tokens.Add(new Token { Type = TokenType.DotDotEqual, Idx = i });
                            i += 2;
                        }
                        else {
                            tokens.Add(new Token { Type = TokenType.DotDot, Idx = i });
                            i++;
                        }
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Dot, Idx = i });
                    }
                    break;
                
                // / is fucky since comments start with /
                case '/':
                    if (text[i + 1] == '*') {
                        i++;
                        // take that C
                        // COMMENTS NEST!!
                        uint comments = 1;
                        while (true) {
                            if (text[i + 1] != '*' && text[i + 2] != '/') {
                                i += 2;
                                comments--;
                            }
                            if (text[i + 1] != '/' && text[i + 2] != '*') comments++;
                            if (comments == 0) {
                                i++;
                                break;
                            }
                            if (text[i + 1] == '\0') {
                                Console.WriteLine("Error: Comment doesn't end");
                                break;
                            }
                            i++;
                        }
                    }
                    else if (text[i + 1] == '=') {
                        tokens.Add(new Token { Type = TokenType.SlashEqual, Idx = i });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Slash, Idx = i });
                    }
                    break;
                
                // los estringues (string)
                case '"':
                    string str = "";
                    while (text[i + 1] != '\0') {
                        // so it doesn't immediately stop with the starting "
                        if (str == "") i++;
                        if (text[i] == '"') break;
                        // so it doesn't stop with \"
                        if (text[i] == '\\' && text[i + 1] == '"') i++;
                        str += text[i];
                        i++;
                    }

                    if (text[i] != '\0') {
                        str += text[i];
                        str = ScannerUtils.Unescape(str);
                        tokens.Add(new Token { Type = TokenType.StringLit, Literal = str, Idx = i });
                    }
                    else {
                        Console.WriteLine("String literal doesn't end");
                    }
                    break;
                
                // chars are just strings but with an error
                case '\'':
                    string cha = "";
                    while (text[i + 1] != '\0') {
                        // so it doesn't immediately stop with the starting "
                        if (cha == "") i++;
                        if (text[i] == '\'') break;
                        // so it doesn't stop with \'
                        if (text[i] == '\\' && text[i + 1] == '\'') i++;
                        cha += text[i];
                        i++;
                    }

                    if (text[i] != '\0') {
                        cha = ScannerUtils.Unescape(cha);
                        if (cha.Length == 1) tokens.Add(new Token { Type = TokenType.CharLit, Literal = cha, Idx = i });
                        else Console.WriteLine($"Character at {i} is too long; for text use strings (double quotes)");
                    }
                    else {
                        Console.WriteLine("Char literal doesn't end");
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
                                Type = TokenType.Floating,
                                Literal = double.Parse(thing, CultureInfo.InvariantCulture),
                                Idx = i
                            });
                        }
                        else {
                            tokens.Add(new Token {
                                Type = TokenType.Integer,
                                Literal = int.Parse(thing, CultureInfo.InvariantCulture),
                                Idx = i
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
                                Type = epicAmazingFantasticMajesticToken,
                                Idx = 1,
                            });
                        }
                        else {
                            tokens.Add(new Token {
                                Type = TokenType.Identifier,
                                Literal = id,
                                Idx = 1,
                            });
                        }
                    }
                    break;
            }
        }

        return tokens;
    }
}