#pragma once
#include "common.h"
#include <stdint.h>
#include <stdbool.h>

typedef struct kettle_type {
    kettle_string* name;
    kettle_type_type type;
    uint64_t ptrs;
    uint64_t arrs;
} kettle_type;

typedef enum kettle_type_type {
    VOID, ANY, CUSTOM,
    BOOL, INT, UINT, FLOAT, CHAR, STRING
} kettle_type_type;

typedef struct kettle_class {
    kettle_string* name;
    bool is_entity;
} kettle_class;

typedef struct kettle_function {
    kettle_string* name;
    void* statements;
    /*kettle_variable_decl*/
    kettle_linked_list* arguments;
    kettle_type return_type;
    bool is_message;
} kettle_function;

typedef struct kettle_variable_decl {
    kettle_string* name;
    kettle_type type;
    void* init_expr;
} kettle_variable_decl;

typedef struct kettle_variable_redef {
    kettle_string* name;
    void* expr;
} kettle_variable_redef;