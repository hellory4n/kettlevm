%{
    #include <stdio.h>
%}

%option noyywrap

%%
[ \r\n\t]+                  {}

"if"                        {printf("THATS DEFINITELY THE IF KEYWORD\n");}

[a-zA-Z_][a-zA-Z0-9_]*      {printf("NICE NAME CAN I HAVE IT\n");}
[0-9]+                      {printf("NICE NUMBER CAN I HAVE IT\n");}

<<EOF>>                     {printf("bye bye (bye)\n"); return 0;}

%%

int main() {
    yylex();
}