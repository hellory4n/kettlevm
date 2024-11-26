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

typedef struct kettle_linked_node {
    void* val;
    kettle_linked_node* next;
    kettle_linked_node* prev;
} kettle_linked_node;

typedef struct kettle_linked_list {
    kettle_linked_node* start;
    kettle_linked_node* end;
} kettle_linked_list;

kettle_linked_list* kettle_new_linked_list() {
    kettle_linked_list* g = (kettle_linked_list*)malloc(sizeof(kettle_linked_list));
    g->start = NULL;
    g->end = NULL;
    return g;
}

void kettle_linked_list_push(kettle_linked_list* l, void* val) {
    if (l->end == NULL) {
        kettle_linked_node* new = (kettle_linked_node*)malloc(sizeof(kettle_linked_node));
        new->val = val;
        new->next = NULL;
        new->prev = NULL;
        l->start = new;
        l->end = new;
    }
    else {
        kettle_linked_node* new = (kettle_linked_node*)malloc(sizeof(kettle_linked_node));
        new->val = val;
        new->prev = l->end;
        new->next = NULL;
        l->end->next = new;
        l->end = val;
    }
}

void kettle_free_linked_list(kettle_linked_list* l) {
    for (kettle_linked_node* tmp = l->start; tmp != NULL; tmp = tmp->next) {
        if (tmp->prev != NULL) {
            free(tmp->prev);
        }
    }
    free(l->start);
    free(l);
}