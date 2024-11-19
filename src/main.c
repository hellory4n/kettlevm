//#include "raylib.h"
#include "core/array.h"
#include <stdlib.h>
#include <stdint.h>
#include <stdio.h>

int main() {
    int64_t j1 = 11, j2 = 22, j3 = 33, j4 = 44;
    printf("9 ");
    Array* a = Array_new(4);
    printf("11 ");
    Array_set(a, 0, &j1);
    printf("13 ");
    Array_set(a, 1, &j2);
    printf("15 ");
    Array_set(a, 2, &j3);
    printf("17 ");
    Array_set(a, 3, &j4);

    printf("20 ");
    for (size_t i = 0; i < Array_len(a); i++) {
        printf("22 ");
        printf("array item %i", Array_at(a, i));
    }

    printf("26 ");
    Array_delete(a);
    printf("28 ");
    return 0;
}

// raylib setup for later
/*const int scrw = 800;
const int scrh = 450;

InitWindow(scrw, scrh, "raylib [core] example - basic window");

while (!WindowShouldClose()) {
    BeginDrawing();
        ClearBackground(RAYWHITE);
        DrawText("Congrats! You created your first window!", 190, 200, 20, LIGHTGRAY);
    EndDrawing();
}

CloseWindow();*/