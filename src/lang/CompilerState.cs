using System.Collections.Generic;

namespace kettlevm;

/// <summary>
/// the entire current of the compiler
/// </summary>
public struct CompilerState() {
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
}