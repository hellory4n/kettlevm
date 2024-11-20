const std = @import("std");

pub fn strconcat(a: []const u8, b: []const u8) ![]const u8
{
    var arena = std.heap.ArenaAllocator.init(std.heap.page_allocator);
    defer arena.deinit();
    const allocator = arena.allocator();

    const result = try std.fmt.allocPrint(allocator, "{s}{s}!", .{a, b});
    defer allocator.free(result);
    return result;
}