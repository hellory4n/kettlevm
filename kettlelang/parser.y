%{
    #include <stdio.h>
%}

%%
S
    :statements _EOF
;

statements
    : %empty
    | statements statement
;

statement:
    : lvalue EQUAL expr SEMICOLON
    | IF LPAREN bool_expr RPAREN statement
    | IF LPAREN bool_expr RPAREN statement ELSE statement
    | WHILE LPAREN bool_expr RPAREN statement
;

expr
    : rel_expr
;

bool_expr
    : rel_expr
;

rel_expr
    : math_expr GREATEREQ math_expr
;

math_expr
    : INTLIT
    | FLOATLIT
    | lvalue
    | math_expr PLUS math_expr
    | math_expr STAR math_expr
;

lvalue
    | IDENTIFIER
    {
        $$ = 
    }
;
%%

int main() {
    yyparse();
}