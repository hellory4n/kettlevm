#pragma once
#include <stdlib.h>

typedef struct kettle_string {
    const char* data;
    size_t len;
} kettle_string;

/*current string literal*/
kettle_string* kettle_pstr = NULL;

kettle_string* kettle_new_string(const char* data, size_t len) {
    kettle_string* s = (kettle_string*)malloc(sizeof(kettle_string));
    s->data = data;
    s->len = len;
    return s;
}

void kettle_free_string(kettle_string* s) {
    free(s);
}