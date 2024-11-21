const std = @import("std");
const rl = @import("raylib");
const lexer = @import("lang/parse/lexer.zig");
const compstate = @import("lang/compiler_state.zig");
const token = @import("lang/parse/token.zig");

pub fn main() anyerror!void
{
    var file = try std.fs.cwd().openFile("test/lexer.ktl", .{});
	defer file.close();

	var buf_reader = std.io.bufferedReader(file.reader());
	var in_stream = buf_reader.reader();

	const file_stat = try file.stat();

    var allocator = std.heap.page_allocator;
	const buffer = try allocator.alloc(u8, file_stat.size);
	try in_stream.readNoEof(buffer);

    // do crap
    var c: compstate.CompilerState = .{
        .tokens = std.ArrayList(token.Token).init(allocator),
    };
    c.lex_i = 0;
    c.file = buffer;
    c = try lexer.lex(c, buffer);
    lexer.print_tokens(c);

	allocator.free(buffer);
    // const screenWidth = 800;
    // const screenHeight = 450;

    // rl.initWindow(screenWidth, screenHeight, "raylib-zig [core] example - basic window");
    // defer rl.closeWindow();

    // rl.setTargetFPS(60);

    // while (!rl.windowShouldClose()) {
    //     rl.beginDrawing();
    //     defer rl.endDrawing();

    //     rl.clearBackground(rl.Color.white);

    //     rl.drawText("Congrats! You created your first window!", 190, 200, 20, rl.Color.light_gray);
    // }
}
