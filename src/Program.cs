using System;
using System.IO;
using Raylib_cs;
namespace kettlevm;

class Program {
    public static void Main()
    {
        string file = File.ReadAllText(Path.GetFullPath("test/class_decl.ktl"));
        var (aShit, crapErrors) = Scanner.Scan(file);
        if (crapErrors == 0) {
            Parser.Parse(aShit);
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