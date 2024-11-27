using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Raylib_cs;
namespace kettlevm;

class Program {
    public static void Main()
    {
        string file = File.ReadAllText(Path.GetFullPath("test/lexer.ktl"));
        Lexer lexer = new(file, "test/lexer.ktl");
        List<Token> tokens = [];
        for (Token t = lexer.GetNextToken(); t.Type != TokenTag.Eof; t = lexer.GetNextToken()) {
            // can't be bothered :D
            Console.WriteLine($"{t.Type} {t.Line} \"{t.SourceLine}\" {t.IntLit} {t.FloatLit} {t.StringLit} {t.CharLit}");
            tokens.Add(t);
        }
        /*CompilerState c = new();
        c = Lexer.lex(ref c, file);
        Lexer.print_tokens(ref c);*/
        /*Raylib.InitWindow(800, 480, "Hello World");
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();*/
    }
}
