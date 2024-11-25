#pragma once
#include <stdio.h>
#include "common.h"

typedef enum kettle_token_t {
    _EOF,

    // keywords
    TRUE, FALSE, CLASS, FUN, MSG, STATIC, PARF, IF, ELSE, FOR, WHILE, STRING, UINT, INT,
    FLOAT, BOOL, CHAR, VOID, ANY, RETURN, AND, OR, NOT, CONTINUE, BREAK, SWITCH, SYNC,
    ENTITY, VEC2, VEC3, COL,

    LPAREN, RPAREN, LBRACKET, RBRACKET, LBRACE, RBRACE,
    DOT, COMMA, COLON, SEMICOLON,

    PLUS, MINUS, STAR, SLASH, PERCENT, DOTDOT,
    PLUSEQ, MINUSEQ, STAREQ, SLASHEQ, PERCENTEQ, DOTDOTEQ,

    GREATER, LESS, GREATEREQ, LESSEQ, EQUAL, EQUALEQ, NOTEQ,

    AMPERSAND, EQARROW, MINUSARROW,

    // literals
    IDENTIFIER, INTLIT, FLOATLIT, STRINGLIT, CHARLIT,
} kettle_token_t;

/*
entity class CrapPlayer;
sync vec2 position = (x 69 y 420);

msg void update(CrapPlayer* c, double delta) {
    OS.println("AND THEN THEY'LL FUUUUUUUUUUUUCK YOU");
}
*/
void kettle_print_token(kettle_token_t t) {
    switch (t) {
        case _EOF: printf("eof\n"); break;
        case TRUE: printf("true "); break;
        case FALSE: printf("false "); break;
        case CLASS: printf("class "); break;
        case FUN: printf("fun "); break;
        case MSG: printf("msg "); break;
        case STATIC: printf("static "); break;
        case PARF: printf("parf "); break;
        case IF: printf("if "); break;
        case ELSE: printf("else "); break;
        case FOR: printf("for "); break;
        case WHILE: printf("while "); break;
        case STRING: printf("string "); break;
        case UINT: printf("uint "); break;
        case INT: printf("int "); break;
        case FLOAT: printf("float "); break;
        case BOOL: printf("bool "); break;
        case CHAR: printf("char "); break;
        case VOID: printf("void "); break;
        case ANY: printf("any "); break;
        case RETURN: printf("return "); break;
        case AND: printf("and "); break;
        case OR: printf("or "); break;
        case NOT: printf("not "); break;
        case CONTINUE: printf("continue "); break;
        case BREAK: printf("break "); break;
        case SWITCH: printf("switch "); break;
        case SYNC: printf("sync "); break;
        case ENTITY: printf("entity "); break;
        case IDENTIFIER: printf("id(%s) ", kettle_pstr->data); kettle_free_string(kettle_pstr); break;
        case INTLIT: printf("int(%s) ", kettle_pstr->data); kettle_free_string(kettle_pstr); break;
        case FLOATLIT: printf("float(%s) ", kettle_pstr->data); kettle_free_string(kettle_pstr); break;
        case STRINGLIT: printf("string(%s) ", kettle_pstr->data); kettle_free_string(kettle_pstr); break;
        case CHARLIT: printf("char(%s) ", kettle_pstr->data); kettle_free_string(kettle_pstr); break;
        case LPAREN: printf("( "); break;
        case RPAREN: printf(") "); break;
        case LBRACKET: printf("[ "); break;
        case RBRACKET: printf("] "); break;
        case LBRACE: printf("{\n"); break;
        case RBRACE: printf("}\n"); break;
        case COMMA: printf(", "); break;
        case COLON: printf(": "); break;
        case SEMICOLON: printf(";\n"); break;
        case DOTDOTEQ: printf("..= "); break;
        case DOTDOT: printf(".. "); break;
        case DOT: printf(". "); break;
        case MINUSARROW: printf("-> "); break;
        case EQARROW: printf("=> "); break;
        case PLUSEQ: printf("+= "); break;
        case MINUSEQ: printf("-= "); break;
        case STAREQ: printf("*= "); break;
        case SLASHEQ: printf("/= "); break;
        case PERCENTEQ: printf("%%= "); break;
        case GREATEREQ: printf(">= "); break;
        case LESSEQ: printf("<= "); break;
        case EQUALEQ: printf("== "); break;
        case NOTEQ: printf("!= "); break;
        case PLUS: printf("+ "); break;
        case MINUS: printf("- "); break;
        case STAR: printf("* "); break;
        case SLASH: printf("/ "); break;
        case PERCENT: printf("%% "); break;
        case GREATER: printf("> "); break;
        case LESS: printf("< "); break;
        case EQUAL: printf("= "); break;
        case AMPERSAND: printf("& "); break;
    }
}