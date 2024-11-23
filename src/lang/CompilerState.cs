using System;
using System.Collections.Generic;

namespace kettlevm;

/// <summary>
/// the entire current of the compiler
/// </summary>
public class CompilerState() {
    /// <summary>
    /// content of the file currently being compiled
    /// </summary>
    public string file { get; set; } = "";
    /// <summary>
    /// current character
    /// </summary>
    public int lex_this { get; set; } = 0;
    /// <summary>
    /// start of the token being parsed
    /// </summary>
    public int lex_start { get; set; } = 0;
    /// <summary>
    /// list of tokens
    /// </summary>
    public Queue<Token> tokens { get; set; } = [];
    /// <summary>
    /// the line the lexer is currently scanning
    /// </summary>
    public int lex_line { get; set; } = 1;

    /// <summary>
    /// error message with line
    /// </summary>
    public void complain(string complaint) => Console.WriteLine($"[{lex_line}:{lex_start}-{lex_this}]: {complaint}");
    /// <summary>
    /// for when lexer no worky
    /// </summary>
    public void noworky() =>
        Console.WriteLine($"{lex_line}:{lex_start}-{lex_this} = {file.Substring(lex_start, lex_this)}");
}