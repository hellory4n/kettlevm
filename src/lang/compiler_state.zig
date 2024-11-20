const std = @import("std");
const token = @import("parse/token.zig");

/// current state of the compiler
pub const CompilerState = struct {
    /// lexer index
    lex_i: u64,
    tokens: std.ArrayList(token.Token),
    /// content of the file currently being compiled
    file: []u8
};