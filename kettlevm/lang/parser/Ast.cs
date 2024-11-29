namespace kettlevm;

class Ast(Token token) {
    public Token Token { get; set; } = token;
}

class Expr(Token token) : Ast(token) {}
class Stmt(Token token) : Ast(token) {}