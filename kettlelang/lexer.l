%{
    #include <stdio.h>
    #include "token.h"
    #include "common.h"
%}

%option noyywrap

%%
[ \r\n\t]+                  {}

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
[0-9]+                      {kettle_pstr = kettle_new_string(yytext, yyleng); return INT;}

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
    while (kettle_token_t t = (kettle_token_t)yylex()) {
        kettle_print_token(t);
    }
}