using System;
using System.Linq;
namespace kettlevm;

public class Lexer(string input, string filename) {
    public int Errors { get; private set; } = 0;
    // TODO fix your dumbass lexer
    internal string input = (input + "\0\0\0\0").Replace("\\\"", "鵈");
    internal string[] lines = (input + "\0\0\0\0").Split('\n');
    internal string filename = filename;
    internal int pos = 0;
    internal int line = 1;

    public Token GetNextToken()
    {
        // lmao
        if (pos >= input.Length) return Token(TokenTag.Eof);
        else if (input[pos] == '\0') return Token(TokenTag.Eof);

        // ignore whitespace
        // we're not python
        else if (char.IsWhiteSpace(input[pos]) && input[pos] != '\0') {
            if (input[pos] == '\n') line++;
            pos++;
            // just continue lol
            return GetNextToken();
        }

        // handle comments before all of the funny slash operators
        else if (Match("//")) {
            ReadWhile(c => c != '\n');
            pos++;
            line++;
            // just continue lol
            return GetNextToken();
        }

        // giant pile of operators
        // the bigger operators need to be handled first
        else if (Match("..=")) return Token(TokenTag.DotDotEq);
        else if (Match("+=")) return Token(TokenTag.PlusEq);
        else if (Match("-=")) return Token(TokenTag.MinusEq);
        else if (Match("*=")) return Token(TokenTag.StarEq);
        else if (Match("/=")) return Token(TokenTag.SlashEq);
        else if (Match("%=")) return Token(TokenTag.PercentEq);
        else if (Match("==")) return Token(TokenTag.EqualEq);
        else if (Match("!=")) return Token(TokenTag.NotEq);
        else if (Match("&&")) return Token(TokenTag.And);
        else if (Match("||")) return Token(TokenTag.Or);
        else if (Match(">=")) return Token(TokenTag.GreaterEq);
        else if (Match("<=")) return Token(TokenTag.LessEq);
        else if (Match("..")) return Token(TokenTag.DotDot);
        else if (Match("+")) return Token(TokenTag.Plus);
        else if (Match("-")) return Token(TokenTag.Minus);
        else if (Match("/")) return Token(TokenTag.Slash);
        else if (Match("*")) return Token(TokenTag.Star);
        else if (Match("%")) return Token(TokenTag.Percent);
        else if (Match("&")) return Token(TokenTag.Ampersand);
        else if (Match("<")) return Token(TokenTag.Less);
        else if (Match(">")) return Token(TokenTag.Greater);
        else if (Match("=")) return Token(TokenTag.Equal);
        else if (Match("!")) return Token(TokenTag.Not);
        else if (Match(":")) return Token(TokenTag.Colon);
        else if (Match(";")) return Token(TokenTag.Semicolon);
        else if (Match(",")) return Token(TokenTag.Comma);
        else if (Match(".")) return Token(TokenTag.Dot);
        else if (Match("{")) return Token(TokenTag.Lbrace);
        else if (Match("}")) return Token(TokenTag.Rbrace);
        else if (Match("[")) return Token(TokenTag.Lbracket);
        else if (Match("]")) return Token(TokenTag.Rbracket);
        else if (Match("(")) return Token(TokenTag.Lparen);
        else if (Match(")")) return Token(TokenTag.Rparen);

        // giant pile of keywords
        else if (Match("true")) return Token(TokenTag.True);
        else if (Match("false")) return Token(TokenTag.False);
        else if (Match("class")) return Token(TokenTag.Class);
        else if (Match("fun")) return Token(TokenTag.Fun);
        else if (Match("msg")) return Token(TokenTag.Msg);
        else if (Match("static")) return Token(TokenTag.Static);
        else if (Match("parf")) return Token(TokenTag.Parf);
        else if (Match("if")) return Token(TokenTag.If);
        else if (Match("else")) return Token(TokenTag.Else);
        else if (Match("for")) return Token(TokenTag.For);
        else if (Match("while")) return Token(TokenTag.While);
        else if (Match("string")) return Token(TokenTag.String);
        else if (Match("int")) return Token(TokenTag.Int);
        else if (Match("uint")) return Token(TokenTag.Uint);
        else if (Match("float")) return Token(TokenTag.Float);
        else if (Match("bool")) return Token(TokenTag.Bool);
        else if (Match("char")) return Token(TokenTag.Char);
        else if (Match("void")) return Token(TokenTag.Void);
        else if (Match("return")) return Token(TokenTag.Return);
        else if (Match("and")) return Token(TokenTag.And);
        else if (Match("or")) return Token(TokenTag.Or);
        else if (Match("not")) return Token(TokenTag.Not);
        else if (Match("continue")) return Token(TokenTag.Continue);
        else if (Match("break")) return Token(TokenTag.Break);
        else if (Match("switch")) return Token(TokenTag.Switch);
        else if (Match("sync")) return Token(TokenTag.Sync);
        else if (Match("entity")) return Token(TokenTag.Entity);
        else if (Match("vec2")) return Token(TokenTag.Vec2);
        else if (Match("vec3")) return Token(TokenTag.Vec3);
        else if (Match("col")) return Token(TokenTag.Col);
        else if (Match("any")) return Token(TokenTag.Any);

        // infamous strings
        else if (Match("\"")) {
            string str = ReadWhile(c => c != '"' && c != '\n');
            // man
            if (str.EndsWith('\n')) {
                Error("Multi-line strings aren't supported; use \\n instead");
                pos++;
                return Token(TokenTag.Unexpected);
            }

            if (pos >= input.Length) {
                Error("String literal doesn't end");
                return Token(TokenTag.Unexpected);
            }

            // so it doesn't start a new string then the world falls under alien invasion and
            // everybody gets frozen on a spaceship ready to be eaten on alpha centauri by
            // some green and jelly humanoid.
            pos++;
            str = Unescape(str);
            var t = Token(TokenTag.StringLit);
            t.StringLit = str;
            return t;
        }

        // infamous characters
        else if (Match("'")) {
            string str = ReadWhile(c => c != '\'' && c != '\n');
            // man
            if (str.EndsWith('\n')) {
                Error("Multi-line strings aren't supported; use \\n instead");
                pos++;
                return Token(TokenTag.Unexpected);
            }

            if (pos >= input.Length) {
                Error("Char literal doesn't end");
                return Token(TokenTag.Unexpected);
            }

            // so it doesn't start a new CHAR then the world falls under alien invasion and
            // everybody gets frozen on a spaceship ready to be eaten on alpha centauri by
            // some green and jelly humanoid.
            pos++;

            str = Unescape(str);
            if (str.Length == 1) {
                var t = Token(TokenTag.CharLit);
                t.CharLit = str[0];
                return t;
            }
            else {
                Error("Characters can only be 1 character (for strings use \"double quotes\")");
                return Token(TokenTag.Unexpected);
            }
        }
        
        // famous arabic numerals
        else if (IsDigit(input[pos])) {
            // you can use _ for big numbers e.g. 1_000_000
            string num = ReadWhile(c => IsDigit(c) || c == '_' || c == '.');
            int dots = num.Count(c => c == '.');
            if (dots > 1) {
                Error("Floating-point numbers can only have 1 dot, that's how math works.");
                return Token(TokenTag.Unexpected);
            }

            num = num.Replace("_", "");
            if (dots == 1) {
                var t = Token(TokenTag.FloatLit);
                t.FloatLit = double.Parse(num);
                return t;
            }
            else {
                var t = Token(TokenTag.IntLit);
                t.IntLit = ulong.Parse(num);
                return t;
            }
        }

        // famous identifiers
        // we don't need to check for keywords because those already matched
        else if (IsAlpha(input[pos]) || input[pos] == '_') {
            string id = ReadWhile(c => IsAlpha(c) || IsDigit(c) || c == '_');
            var t = Token(TokenTag.Identifier);
            t.StringLit = id;
            return t;
        }

        else if (!Match("\0")) return Token(TokenTag.Eof);

        // nothing matched, complain
        else {
            Error($"Unexpected character {input[pos]}");
            return Token(TokenTag.Unexpected);
        }
    }

    bool Match(string text)
    {
        // that funny .. thing is just making a substring
        if (input[pos..].StartsWith(text)) {
            pos += text.Length;
            line += text.Count(c => c == '\n');
            return true;
        }
        return false;
    }

    string ReadWhile(Func<char, bool> condition)
    {
        int start = pos;
        while (pos < input.Length && condition(input[pos])) {
            pos++;
        }
        // that funny .. thing is just making a substring
        string s = input[start..pos];
        line += s.Count(c => c == '\n');
        return s;
    }

    Token Token(TokenTag type) {
        return new() {
            Type = type,
            Line = line,
            SourceLine = lines[line - 1],
        };
    }

    void Error(string error) {
        // example:
        // kettleproj/main.ktl:69: Unexpected happening that happened unexpectedly.
        //     suffer_and_die();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{filename}:{line}: {error}");
        Console.ResetColor();
        Console.WriteLine($"    {lines[line - 1]}");
        Console.WriteLine();
        Errors++;
    }

    bool IsDigit(char c) => c >= '0' && c <= '9';
    bool IsAlpha(char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');

    string Unescape(string s) {
        
        return s
            .Replace("\\\\", "垈") // 垈 isn't used anywhere unless you're talking about itself, see
                                   // https://en.wikipedia.org/wiki/Ghost_characters
                                   // TODO: don't
            // these are just C escapes except for \e and \? and the other ones too
            // TODO: make unicode escapes too
            .Replace("\\a", "\a")
            .Replace("\\b", "\b")
            .Replace("\\f", "\f")
            .Replace("\\n", "\n")
            .Replace("\\r", "\r")
            .Replace("\\t", "\t")
            .Replace("\\v", "\v")
            .Replace("\\'", "'")
            .Replace("鵈", "\"") // don't ask
            .Replace('垈', '\\');
    }
}
