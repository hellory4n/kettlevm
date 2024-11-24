using System.Collections.Generic;

namespace kettlevm;

interface AstNode {}
interface Expr: AstNode {}
interface Stmt: AstNode {}

class Class: Stmt {
    public string Name { get; set; } = "";
    public Queue<VariableDecl> Properties { get; set; } = [];
    public Queue<Function> Methods { get; set; } = [];
}

class VariableDecl: Stmt {
    public TypeThing Type { get; set; } = new();
    public string Name { get; set; } = "";
    public Expr? Initializer { get; set; }
}

class TypeThing {
    public string Name { get; set; } = "";
    public PrimitiveType TypeType { get; set; }
}

enum PrimitiveType {
    Void,
    Any,
    CustomType,
    Int,
    Uint,
    Float,
    Char,
    String,
    Bool,
}

class Function {
    public bool IsMessage { get; set; }
    public TypeThing ReturnType { get; set; } = new();
    public Queue<VariableDecl> Arguments { get; set; } = [];
    public Queue<Stmt> Statements { get; set; } = [];
}

class IntLit: Expr {
    public long Value { get; set; } = 0;
}

class FloatLit: Expr {
    public double Value { get; set; } = 0;
}

class BoolLit: Expr {
    public bool Value { get; set; } = false;
}

class CharLit: Expr {
    public char Value { get; set; } = '\0';
}

class StringLit: Expr {
    public string Value { get; set; } = "";
}