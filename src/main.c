#include "raylib.h"

int main(void) {
    const int scrw = 800;
    const int scrh = 450;

    InitWindow(scrw, scrh, "raylib [core] example - basic window");
    
    while (!WindowShouldClose()) {
        BeginDrawing();
            ClearBackground(RAYWHITE);
            DrawText("Congrats! You created your first window!", 190, 200, 20, LIGHTGRAY);
        EndDrawing();
    }

    CloseWindow();
    return 0;
}
