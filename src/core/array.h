#pragma once
#include <stdlib.h>

/* safe array :D */
typedef struct {
    void* arr;
    size_t len;
} Array;

/* makes a new array */
Array* Array_new(size_t size);
/* gets the length of the array */
size_t Array_len(Array* a);
/* gets the item at an index */
void* Array_at(Array* a, size_t idx);
/* sets the item at an index */
void Array_set(Array* a, size_t idx, void* newval);
/* deletes the array */
void Array_delete(Array* a);