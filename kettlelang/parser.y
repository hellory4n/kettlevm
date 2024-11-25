%{
    #include <stdio.h>
%}

%start program

%%
program: %empty
%%

int main() {
    yyparse();
}