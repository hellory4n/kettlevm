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
    /// lexer index
    /// </summary>
    public int lex_i { get; set; } = 0;
    /// <summary>
    /// list of tokens
    /// </summary>
    public Queue<Token> tokens { get; set; } = [];
    /// <summary>
    /// the line the lexer is currently scanning
    /// </summary>
    public int thisline { get; set; } = 1;

    /// <summary>
    /// current character
    /// </summary>
    public char thisc() => file[lex_i];
    /// <summary>
    /// next character
    /// </summary>
    public char nextc() => file[lex_i + 1];
    /// <summary>
    /// next next character
    /// </summary>
    public char nexterc() => file[lex_i + 2];
    /// <summary>
    /// error message with line
    /// </summary>
    public void complain(string complaint) => Console.WriteLine($"line {thisline}: {complaint}");
}