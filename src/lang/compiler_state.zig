const std = @import("std");
const token = @import("parse/token.zig");

/// current state of the compiler
pub const CompilerState = struct {
    /// lexer index
    var lex_i: u64 = 0;
    /// list of tokens
    tokens: std.ArrayList(token.Token),
    /// content of the file currently being compiled
    var file: []const u8 = "";
};