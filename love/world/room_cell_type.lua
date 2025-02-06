local grimoire = require "game.grimoire"
local M = {}
M.BLANK = "BLANK"
local data = {
    [M.BLANK] = {
        tile = grimoire.TILE_BLANK,
        blocking = false
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