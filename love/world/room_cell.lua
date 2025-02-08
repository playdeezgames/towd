local room_cell_type = require "world.room_cell_type"
local world = require "world.world"
local M = {}
world.data.room_cells = {}
function world.get_room_cells()
	return M
end
local function get_room_cell_data(room_cell_id)
    return world.data.room_cells[room_cell_id]
end
function M.initialize(room_cell_id, room_cell_type_id, room_id, column, row)
    world.data.room_cells[room_cell_id] = {
        room_cell_type_id = room_cell_type_id,
        room_id = room_id,
        column = column,
        row = row
    }
    local initializer = room_cell_type.get_initializer(room_cell_type_id)
    if initializer ~= nil then
        initializer(room_cell_id)
    end
end
function M.create(room_cell_type_id, room_id, column, row)
    assert(type(room_cell_type_id)=="string", "room_cell_type_id should be a string")
    --TODO: look for a recycled room cell first
    local room_cell_id = #world.data.room_cells + 1
    M.initialize(room_cell_id, room_cell_type_id, room_id, column, row)
    return room_cell_id
end
function M.get_room_cell_type(room_cell_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    return get_room_cell_data(room_cell_id).room_cell_type_id
end
function M.set_room_cell_type(room_cell_id, room_cell_type_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    assert(type(room_cell_type_id)=="string", "room_cell_type_id should be a string")
    M.initialize(room_cell_id, room_cell_type_id, M.get_room(room_cell_id), M.get_column(room_cell_id), M.get_row(room_cell_id))
end
function M.get_room(room_cell_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    return get_room_cell_data(room_cell_id).room_id
end
function M.get_column(room_cell_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    return get_room_cell_data(room_cell_id).column
end
function M.get_row(room_cell_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    return get_room_cell_data(room_cell_id).row
end
function M.get_position(room_cell_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    local room_cell_data = get_room_cell_data(room_cell_id)
    return room_cell_data.column, room_cell_data.row
end
function M.get_character(room_cell_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    return get_room_cell_data(room_cell_id).character_id
end
function M.set_character(room_cell_id, character_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    get_room_cell_data(room_cell_id).character_id = character_id
end
function M.has_character(room_cell_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    return get_room_cell_data(room_cell_id).character_id ~= nil
end
function M.get_feature(room_cell_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    return get_room_cell_data(room_cell_id).feature_id
end
function M.set_feature(room_cell_id, feature_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    get_room_cell_data(room_cell_id).feature_id = feature_id
end
function M.has_feature(room_cell_id)
    assert(type(room_cell_id)=="number", "room_cell_id should be a number")
    return get_room_cell_data(room_cell_id).feature_id ~= nil
end
function M.can_enter(room_cell_id)
    if room_cell_id == nil then return false end
    if M.has_character(room_cell_id) then return false end
    if M.has_feature(room_cell_id) then return false end
    local room_cell_type_id = M.get_room_cell_type(room_cell_id)
    if room_cell_type.get_blocking(room_cell_type_id) then return false end
    return true
end
return M