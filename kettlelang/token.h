#pragma once
#include <stdio.h>
#include "common.h"

typedef enum kettle_token_t {
    _EOF,

    // keywords
    TRUE, FALSE, CLASS, FUN, MSG, STATIC, PARF, IF, ELSE, FOR, WHILE, STRING, UINT, INT,
    FLOAT, BOOL, CHAR, VOID, ANY, RETURN, AND, OR, NOT, CONTINUE, BREAK, SWITCH, SYNC,
    ENTITY,

    // literals
    IDENTIFIER, NUMBER,
} kettle_token_t;

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
        case IDENTIFIER: printf("identifier(%s) ", kettle_pstr->data); kettle_free_string(kettle_pstr); break;
        case NUMBER: printf("number(%s) ", kettle_pstr->data); kettle_free_string(kettle_pstr); break;
    }
}