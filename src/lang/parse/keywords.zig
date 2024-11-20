const std = @import("std");
const token = @import("token.zig");

/// returns an identifier if it's not a keyword
pub fn read_keyword(id: []const u8) token.Token
{
    if (std.mem.eql(u8, id, "true")) return token.Token { .ktrue = 0 };
    if (std.mem.eql(u8, id, "false")) return token.Token { .kfalse = 0 };
    if (std.mem.eql(u8, id, "class")) return token.Token { .kclass = 0 };
    if (std.mem.eql(u8, id, "fun")) return token.Token { .kfun = 0 };
    if (std.mem.eql(u8, id, "msg")) return token.Token { .kmsg = 0 };
    if (std.mem.eql(u8, id, "static")) return token.Token { .kstatic = 0 };
    if (std.mem.eql(u8, id, "parf")) return token.Token { .kparf = 0 };
    if (std.mem.eql(u8, id, "if")) return token.Token { .kif = 0 };
    if (std.mem.eql(u8, id, "else")) return token.Token { .kelse = 0 };
    if (std.mem.eql(u8, id, "for")) return token.Token { .kfor = 0 };
    if (std.mem.eql(u8, id, "string")) return token.Token { .kstring = 0 };
    if (std.mem.eql(u8, id, "int")) return token.Token { .kint = 0 };
    if (std.mem.eql(u8, id, "uint")) return token.Token { .kuint = 0 };
    if (std.mem.eql(u8, id, "float")) return token.Token { .kfloat = 0 };
    if (std.mem.eql(u8, id, "bool")) return token.Token { .kbool = 0 };
    if (std.mem.eql(u8, id, "char")) return token.Token { .kchar = 0 };
    if (std.mem.eql(u8, id, "void")) return token.Token { .kvoid = 0 };
    if (std.mem.eql(u8, id, "return")) return token.Token { .kreturn = 0 };
    return token.Token { .identifier = id };
}