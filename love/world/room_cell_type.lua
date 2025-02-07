local grimoire = require "game.grimoire"
local M = {}
M.BLANK = "BLANK"
M.NORTH_WALL = "NORTH_WALL"
M.EAST_WALL = "EAST_WALL"
M.SOUTH_WALL = "SOUTH_WALL"
M.WEST_WALL = "WEST_WALL"
M.NORTHEAST_INSIDE_CORNER = "NORTHEAST_INSIDE_CORNER"
M.SOUTHEAST_INSIDE_CORNER = "SOUTHEAST_INSIDE_CORNER"
M.SOUTHWEST_INSIDE_CORNER = "SOUTHWEST_INSIDE_CORNER"
M.NORTHWEST_INSIDE_CORNER = "NORTHWEST_INSIDE_CORNER"
local data = {
    [M.BLANK] = {
        tile = grimoire.TILE_BLANK,
        blocking = false
    },
    [M.NORTH_WALL] = {
        tile = grimoire.TILE_NORTH_WALL,
        blocking = true
    },
    [M.EAST_WALL] = {
        tile = grimoire.TILE_EAST_WALL,
        blocking = true
    },
    [M.SOUTH_WALL] = {
        tile = grimoire.TILE_SOUTH_WALL,
        blocking = true
    },
    [M.WEST_WALL] = {
        tile = grimoire.TILE_WEST_WALL,
        blocking = true
    },
    [M.NORTHEAST_INSIDE_CORNER] = {
        tile = grimoire.TILE_NORTHEAST_INSIDE_CORNER,
        blocking = true
    },
    [M.SOUTHEAST_INSIDE_CORNER] = {
        tile = grimoire.TILE_SOUTHEAST_INSIDE_CORNER,
        blocking = true
    },
    [M.SOUTHWEST_INSIDE_CORNER] = {
        tile = grimoire.TILE_SOUTHWEST_INSIDE_CORNER,
        blocking = true
    },
    [M.NORTHWEST_INSIDE_CORNER] = {
        tile = grimoire.TILE_NORTHWEST_INSIDE_CORNER,
        blocking = true
    }
}
local function get_descriptor(room_cell_type_id)
    assert(type(room_cell_type_id) == "string", "room_cell_type_id should be a string")
    if data[room_cell_type_id] == nil then
        data[room_cell_type_id] = {}
    end
    return data[room_cell_type_id]
end
function M.get_tile(room_cell_type_id)
    assert(type(room_cell_type_id) == "string", "room_cell_type_id should be a string")
    return get_descriptor(room_cell_type_id).tile
end
function M.get_initializer(room_cell_type_id)
    assert(type(room_cell_type_id) == "string", "room_cell_type_id should be a string")
    return get_descriptor(room_cell_type_id).initializer
end
function M.set_initializer(room_cell_type_id, initializer)
    assert(type(room_cell_type_id) == "string", "room_cell_type_id should be a string")
    assert(type(initializer) == "function", "initializer should be a function")
    local old_initializer = M.get_initializer(room_cell_type_id)
    get_descriptor(room_cell_type_id).initializer = initializer
    return old_initializer
end
function M.get_blocking(room_cell_type_id)
    assert(type(room_cell_type_id) == "string", "room_cell_type_id should be a string")
    return get_descriptor(room_cell_type_id).blocking
end
return M