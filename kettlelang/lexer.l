%{
    #include <stdio.h>
    #include "token.h"
%}

%option noyywrap

%%
[ \r\n\t]+                  {}

"if"                        {return IF;}

[a-zA-Z_][a-zA-Z0-9_]*      {return IDENTIFIER;}
[0-9]+                      {return NUMBER;}

.                           {printf("UNEXPECTED HAPPENING THAT HAPPENED UNEXPECTEDLY\n");}
<<EOF>>                     {return _EOF; return 0;}

%%

int main() {
    // i hate c
    while (kettle_token_t t = (kettle_token_t)yylex()) {
        kettle_print_token(t);
    }
}