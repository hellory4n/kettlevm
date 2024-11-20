const std = @import("std");
const token = @import("token.zig");
const compstate = @import("../compiler_state.zig");
const util = @import("../../util.zig");
const keywords = @import("keywords.zig");

pub fn lex(c: *compstate.CompilerState, allocator: std.mem.Allocator, buffer: []u8) void
{
    c.lex_i = 0;
    c.tokens = std.ArrayList(token.Token).init(allocator);
    c.file = buffer;

    while (c.lex_i < buffer.len) : (c.lex_i += 1) {
        read_token(&c);
    }
}

fn read_token(c: *compstate.CompilerState) void
{
    // identifiers/keywords
    const thisc = c.file[c.lex + 1];
    if (std.ascii.isAlphanumeric(thisc) || thisc == '_') {
        read_identifier(&c);
    }
}

fn read_identifier(c: *compstate.CompilerState) void {
    var id = "";
    var nextc = c.file[c.lex + 1];

    while (std.ascii.isAlphanumeric(nextc) || nextc == '_') {
        id = util.strconcat(id, nextc);
        c.lex_i += 1;
        nextc = c.file[c.lex + 1];
    }

    const nowthatlookslikeaj = keywords.read_keyword(id);
    switch (nowthatlookslikeaj) {
        .identifier => c.tokens.append(token.Token { .identifier = id }),
        else => c.tokens.append(nowthatlookslikeaj)
    }
}