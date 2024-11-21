const std = @import("std");
const token = @import("token.zig");
const compstate = @import("../compiler_state.zig");
const literals = @import("literals.zig");
const util = @import("../../util.zig");

pub fn lex(cc: compstate.CompilerState) !compstate.CompilerState
{
    var c = cc;
    c.file = try util.strconcat(c.file, "\n\n");

    while (c.lex_i < c.file.len) : (c.lex_i += 1) {
        c = read_token(c);
    }
    return c;
}

fn read_token(cc: compstate.CompilerState) compstate.CompilerState
{
    var c = cc;
    const thisc = c.file[c.lex];
    const nextc = c.file[c.lex + 1];
    const nexterc = c.file[c.lex + 2];

    switch (thisc) {
        '"' => c = literals.read_string(&c),
        '\'' => c = literals.read_char(&c),

        // +, +=
        '+' => {
            if (nextc == '=') c.tokens.append(token.Token { .pluseq })
            else c.tokens.append(token.Token { .plus });
        },

        // -, -=
        '-' => {
            if (nextc == '=') c.tokens.append(token.Token { .minuseq })
            else c.tokens.append(token.Token { .minus });
        },

        // *, *=
        '*' => {
            if (nextc == '=') c.tokens.append(token.Token { .asteriskeq })
            else c.tokens.append(token.Token { .asterisk });
        },

        // /, /=, /* */
        '/' => {
            if (nextc == '=') c.tokens.append(token.Token { .slasheq })
            else if (nextc == '*') {
                // TODO big mistake!
                while (true) {
                    if (nextc == '*' and nexterc == '/') {
                        break;
                    }
                    c.lex_i += 1;
                    nextc = c.file[c.lex_i + 1];
                    nexterc = c.file[c.lex.i + 2];
                }
            }
            else c.tokens.append(token.Token { .asterisk });
        },

        // %, %=
        '%' => {
            if (nextc == '=') c.tokens.append(token.Token { .percenteq })
            else c.tokens.append(token.Token { .percent });
        },

        // =, ==
        '=' => {
            if (nextc == '=') c.tokens.append(token.Token { .equaleq })
            else c.tokens.append(token.Token { .equal });
        },

        // !, !=
        '!' => {
            if (nextc == '=') c.tokens.append(token.Token { .bangeq })
            else c.tokens.append(token.Token { .bang });
        },

        // >, >=
        '>' => {
            if (nextc == '=') c.tokens.append(token.Token { .greateq })
            else c.tokens.append(token.Token { .great });
        },

        // <, <=
        '<' => {
            if (nextc == '=') c.tokens.append(token.Token { .lesseq })
            else c.tokens.append(token.Token { .less });
        },

        // &, &&
        '&' => {
            if (nextc == '&') c.tokens.append(token.Token { .ampersand2 })
            else c.tokens.append(token.Token { .ampersand });
        },

        // |, ||
        '|' => {
            if (nextc == '|') c.tokens.append(token.Token { .pipe2 })
            else c.tokens.append(token.Token { .pipe });
        },

        // ., ..
        '.' => {
            if (nextc == '.') c.tokens.append(token.Token { .dotdot })
            else c.tokens.append(token.Token { .dot });
        },

        // ,
        ',' => c.tokens.append(token.Token { .comma }),
        ';' => c.tokens.append(token.Token { .semicolon }),
        ':' => c.tokens.append(token.Token { .colon }),
        '(' => c.tokens.append(token.Token { .lparen }),
        ')' => c.tokens.append(token.Token { .rparen }),
        '[' => c.tokens.append(token.Token { .lbracket }),
        ']' => c.tokens.append(token.Token { .rbracket }),
        '{' => c.tokens.append(token.Token { .lbrace }),
        '}' => c.tokens.append(token.Token { .rbrace }),

        else => {
            // numbers
            if (std.ascii.isDigit(thisc)) {
                c = literals.read_number(&c);
            }
            // literals/keywords
            else if (std.ascii.isAlphanumeric(thisc) || thisc == '_') {
                c = literals.read_identifier(&c);
            }
            else {
                std.debug.print("Unexpected character {s}", .{thisc});
            }
        }
    }
    return c;
}

pub fn print_tokens(c: compstate.CompilerState) void {
    for (c.tokens) |item| {
        std.debug.print("token {s}", .{@tagName(item)});
    }
}