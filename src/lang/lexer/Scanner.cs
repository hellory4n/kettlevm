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
                        tokens.Add(new Token { Type = TokenType.SlashEqual });
                        i++;
                    }
                    else {
                        tokens.Add(new Token { Type = TokenType.Slash });
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
                        tokens.Add(new Token { Type = TokenType.StringLit, Literal = str });
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
                        if (cha.Length == 1) tokens.Add(new Token { Type = TokenType.CharLit, Literal = cha });
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