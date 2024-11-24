namespace kettlevm;

class Token
{
    public TokenType Type { get; set; }
    public object? Literal { get; set; }
    /// <summary>
    /// the index of the token in the file
    /// </summary>
    public int Idx { get; set; } = 0;

    public override string ToString()
    {
        return Literal == null ? $"[{Type}]" : $"[{Type} = {Literal}]";
    }
}