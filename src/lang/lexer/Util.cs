namespace kettlevm;

public static partial class Lexer {
    // see an ascii table
    public static bool is_digit(char c) => c >= '0' && c <= '9';
    public static bool is_alphabetic(char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
}