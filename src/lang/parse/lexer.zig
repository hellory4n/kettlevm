const std = @import("std");
const token = @import("token.zig");
const compstate = @import("../compiler_state.zig");
const literals = @import("literals.zig");

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
    const thisc = c.file[c.lex];
    // TODO add a \0 at the end so it doesn't crash and die at the end
    const nextc = c.file[c.lex + 1];

    switch (thisc) {
        // TODO i forgor to make char literals
        '"' => literals.read_string(&c),
        '+' => {
            if (nextc == '=') c.tokens.append(token.Token { .pluseq })
            else c.tokens.append(token.Token { .plus });
        },

        else => {
            // numbers
            if (std.ascii.isDigit(thisc)) {
                literals.read_number(&c);
            }

            // literals/keywords
            if (std.ascii.isAlphanumeric(thisc) || thisc == '_') {
                literals.read_identifier(&c);
            }
        }
    }
}