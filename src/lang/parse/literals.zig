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

pub fn read_number(c: *compstate.CompilerState) void {
    var is_float = false;
    var numstr = "";
    var nextc = c.file[c.lex_i + 1];

    while (std.ascii.isDigit(nextc)) {
        numstr = util.strconcat(numstr, c.file[c.lex_i]);
        if (nextc == '.') {
            c.lex_i += 1;
            is_float = true;
        }
        c.lex_i += 1;
        nextc = c.file[c.lex_i + 1];
    }

    // we just parsed a string, strings are in fact not numbers
    // conveniently parseInt/parseFloat supports _s like 1_000_000
    if (is_float) {
        return try std.fmt.parseFloat(f64, numstr);
    }
    else {
        return try std.fmt.parseInt(i64, numstr, 10);
    }
}