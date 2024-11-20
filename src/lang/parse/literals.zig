const std = @import("std");
const compstate = @import("../compiler_state.zig");
const token = @import("token.zig");
const util = @import("../../util.zig");
const keywords = @import("keywords.zig");

pub fn read_identifier(c: *compstate.CompilerState) void {
    var id = "";
    var nextc = c.file[c.lex_i + 1];

    while (std.ascii.isAlphanumeric(nextc) || nextc == '_') {
        id = util.strconcat(id, c.file[c.lex_i]);
        c.lex_i += 1;
        nextc = c.file[c.lex_i + 1];
    }

    const nowthatlookslikeaj = keywords.read_keyword(id);
    switch (nowthatlookslikeaj) {
        .identifier => c.tokens.append(token.Token { .identifier = id }),
        else => c.tokens.append(nowthatlookslikeaj),
    }
}

pub fn read_string(c: *compstate.CompilerState) void {
    var str = "";
    var nextc = c.file[c.lex_i + 1];

    // TODO: add escapes like \n \" \u69420
    while (nextc != '"') {
        str = util.strconcat(str, c.file[c.lex_i]);
        c.lex_i += 1;
        nextc = c.file[c.lex_i + 1];
    }
}