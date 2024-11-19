#include "array.h"
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

Array* Array_new(size_t size)
{
    Array* a = (Array*)malloc(sizeof(Array));
    a->arr = malloc(size * sizeof(void*));
    a->len = size;
}

size_t Array_len(Array* a)
{
    return a->len;
}

void* Array_at(Array* a, size_t idx)
{
    if (idx >= a->len) {
        perror("Can't access index");
        return NULL;
    }
    return (void*)((char*)a->arr + idx * sizeof(void*));
}

void Array_set(Array* a, size_t idx, void* val)
{
    if (idx >= a->len) {
        perror("Can't access index");
        return;
    }
    memcpy((char*)a->arr + idx * sizeof(void*), val, sizeof(void*));
}

void Array_delete(Array* a)
{
    free(a->arr);
    free(a);
}
