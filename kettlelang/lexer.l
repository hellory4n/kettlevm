%{
    #include <stdio.h>
    #include "token.h"
    #include "common.h"
    int kettle_comment_depth = 0;
%}

%option noyywrap
%x COMMENT

%%
[ \r\n\t]+                  {}

"//".*                   { /*ignore*/ }
"/*"                     { BEGIN(COMMENT); kettle_comment_depth = 1; }
<COMMENT>"/*"            { kettle_comment_depth++; }
<COMMENT>"*/"            {
                            if (--kettle_comment_depth == 0) {
                                BEGIN(INITIAL);
                            }
                          }
<COMMENT>.|\n            { /*ignore*/ }
<COMMENT><<EOF>>         { fprintf(stderr, "Unclosed comment\n"); exit(1); }

"true"                      {return TRUE;}
"false"                     {return FALSE;}
"class"                     {return CLASS;}
"fun"                       {return FUN;}
"msg"                       {return MSG;}
"static"                    {return STATIC;}
"parf"                      {return PARF;}
"if"                        {return IF;}
"else"                      {return ELSE;}
"for"                       {return FOR;}
"while"                     {return WHILE;}
"string"                    {return STRING;}
"uint"                      {return UINT;}
"int"                       {return INT;}
"float"                     {return FLOAT;}
"bool"                      {return BOOL;}
"char"                      {return CHAR;}
"void"                      {return VOID;}
"any"                       {return ANY;}
"return"                    {return RETURN;}
"and"                       {return AND;}
"or"                        {return OR;}
"not"                       {return NOT;}
"continue"                  {return CONTINUE;}
"break"                     {return BREAK;}
"switch"                    {return SWITCH;}
"sync"                      {return SYNC;}
"entity"                    {return ENTITY;}
"vec2"                      {return VEC2;}
"vec3"                      {return VEC3;}
"col"                       {return COL;}

[a-zA-Z_][a-zA-Z0-9_]*      {kettle_pstr = kettle_new_string(yytext, yyleng); return IDENTIFIER;}
[0-9]+\.[0-9]*              {kettle_pstr = kettle_new_string(yytext, yyleng); return FLOATLIT; }
[0-9]+                      {kettle_pstr = kettle_new_string(yytext, yyleng); return INTLIT;}
\"([^\\\"]|\\.)*\"          {
                                /*i love regex*/
                                yytext[yyleng - 1] = '\0';  /* remove trailing quote */
                                kettle_pstr = kettle_new_string(strdup(yytext + 1), yyleng); /* remove leading quote */
                                return STRINGLIT;
                            }
\'([^\\\"]|\\.)*\'          {
                                /*please note char literals are just strings, the rest of the compiler
                                is supposed to check the length or smth*/
                                yytext[yyleng - 1] = '\0';  /* remove trailing quote */
                                kettle_pstr = kettle_new_string(strdup(yytext + 1), yyleng); /* remove leading quote */
                                return CHARLIT;
                            }

"("                         {return LPAREN;}
")"                         {return RPAREN;}
"["                         {return LBRACKET;}
"]"                         {return RBRACKET;}
"{"                         {return LBRACE;}
"}"                         {return RBRACE;}
","                         {return COMMA;}
":"                         {return COLON;}
";"                         {return SEMICOLON;}

"..="                       {return DOTDOTEQ;}
".."                        {return DOTDOT;}
"."                         {return DOT;}

"->"                        {return MINUSARROW;}
"=>"                        {return EQARROW;}

"+="                        {return PLUSEQ;}
"-="                        {return MINUSEQ;}
"*="                        {return STAREQ;}
"/="                        {return SLASHEQ;}
"%="                        {return PERCENTEQ;}

">="                        {return GREATEREQ;}
"<="                        {return LESSEQ;}
"=="                        {return EQUALEQ;}
"!="                        {return NOTEQ;}

"+"                         {return PLUS;}
"-"                         {return MINUS;}
"*"                         {return STAR;}
"/"                         {return SLASH;}
"%"                         {return PERCENT;}

">"                         {return GREATER;}
"<"                         {return LESS;}
"="                         {return EQUAL;}
"!"                         {return NOT;}

"&&"                        {return AND;}
"&"                         {return AMPERSAND;}
"||"                        {return OR;}

.                           {printf("UNEXPECTED CHARACTER\n");}
<<EOF>>                     {return _EOF; return 0;}

%%

int main() {
    // i hate c
    while (kettle_token t = (kettle_token)yylex()) {
        kettle_print_token(t);
    }
}