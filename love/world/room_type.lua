local room_cell_type = require "world.room_cell_type"
local M = {}
M.START = "START"
local data = {
    [M.START] = {
        room_cell_type_id = room_cell_type.BLANK
    }
}
local function get_descriptor(room_type_id)
    assert(type(room_type_id) == "string", "room_type_id should be a string")
    if data[room_type_id] == nil then
        data[room_type_id] = {}
    end
    return data[room_type_id]
end
function M.get_room_cell_type(room_type_id)
    assert(type(room_type_id) == "string", "room_type_id should be a string")
    return get_descriptor(room_type_id).room_cell_type_id
end
function M.get_initializer(room_type_id)
    assert(type(room_type_id) == "string", "room_type_id should be a string")
    return get_descriptor(room_type_id).initializer
end
function M.set_initializer(room_type_id, initializer)
    assert(type(room_type_id) == "string", "room_type_id should be a string")
    assert(type(initializer) == "function", "initializer should be a function")
    get_descriptor(room_type_id).initializer = initializer
end
return M