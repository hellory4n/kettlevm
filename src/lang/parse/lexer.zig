const std = @import("std");
const token = @import("token.zig");
const compstate = @import("../compiler_state.zig");

pub fn lex(c: *compstate.CompilerState, allocator: std.mem.Allocator, buffer: []u8) std.ArrayList(token.Token) {
    c.lex_i = 0;
    var tokens = std.ArrayList(token.Token).init(allocator);

    while (c.lex_i < buffer.len) : (c.lex_i += 1) {
        
    }

    return tokens;
}