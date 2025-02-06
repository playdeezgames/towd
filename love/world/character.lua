local room_cell = require "world.room_cell"
local character_type = require "world.character_type"
local world = require "world.world"
local M = {}
world.data.characters = {}
local function get_character_data(character_id)
    assert(type(character_id)=="number", "character_id must be a number.")
    return world.data.characters[character_id]
end
function M.initialize(character_id, character_type_id, room_cell_id)
    world.data.characters[character_id] = {
        character_type_id = character_type_id
    }
    M.set_room_cell(character_id, room_cell_id)
    local initializer = character_type.get_initializer(character_type_id)
    if initializer ~= nil then
        initializer(character_id)
    end
end
function M.create(character_type_id, room_cell_id)
    assert(type(character_type_id)=="string", "character_type_id must be a string.")
    assert(type(room_cell_id)=="number", "room_cell_id must be a number")
    local character_id = #world.data.characters + 1
    M.initialize(character_id, character_type_id, room_cell_id)
    return character_id
end
function M.get_character_type(character_id)
    assert(type(character_id)=="number", "character_id must be a number.")
    return get_character_data(character_id).character_type_id
end
function M.get_room_cell(character_id)
    assert(type(character_id)=="number", "character_id must be a number.")
    return get_character_data(character_id).room_cell_id
end
function M.get_position(character_id)
    assert(type(character_id)=="number", "character_id must be a number.")
    return room_cell.get_position(M.get_room_cell(character_id))
end
function M.set_room_cell(character_id, room_cell_id)
    assert(type(character_id)=="number", "character_id must be a number.")
    assert(type(room_cell_id)=="number" or type(room_cell_id)=="nil", "room_cell_id must be a number or nil")
    local previous_room_cell_id = M.get_room_cell(character_id)
    if previous_room_cell_id ~= nil then
        room_cell.set_character(previous_room_cell_id, nil)
    end
    get_character_data(character_id).room_cell_id = room_cell_id
    if room_cell_id ~= nil then
        room_cell.set_character(room_cell_id, character_id)
    end
end
function M.get_room(character_id)
    assert(type(character_id)=="number", "character_id must be a number.")
    return room_cell.get_room(M.get_room_cell(character_id))
end
function M.set_statistic(character_id, statistic_type_id, statistic_value)
    assert(type(character_id)=="number", "character_id must be a number.")
    assert(type(statistic_type_id)=="string", "statistic_type_id must be a string")
    assert(type(statistic_value)=="number", "statistic_value must be a number")
    local character_data = get_character_data(character_id)
    if character_data.statistics == nil then
        character_data.statistics = {}
    end
    local previous_value = M.get_statistic(character_id, statistic_type_id)
    character_data.statistics[statistic_type_id] = statistic_value
    return previous_value
end
function M.change_statistic(character_id, statistic_type_id, delta)
    assert(type(character_id)=="number", "character_id must be a number.")
    assert(type(statistic_type_id)=="string", "statistic_type_id must be a string")
    assert(type(delta)=="number", "delta must be a number")
    local new_value = M.get_statistic(character_id, statistic_type_id) + delta
    M.set_statistic(character_id, statistic_type_id, new_value)
    return new_value
end
function M.get_statistic(character_id, statistic_type_id)
    assert(type(character_id)=="number", "character_id must be a number.")
    assert(type(statistic_type_id)=="string", "statistic_type_id must be a string")
    local character_data = get_character_data(character_id)
    if character_data.statistics == nil then
        return nil
    end
    return character_data.statistics[statistic_type_id]
end
function M.do_verb(character_id, verb_type_id, context)
    assert(type(character_id)=="number", "character_id must be a number.")
    assert(type(verb_type_id)=="string", "verb_type_id must be a string.")
    assert(type(context)=="nil" or type(context)=="table", "context must be a table or nil.")
    local character_type_id = M.get_character_type(character_id)
    local verb_doer = character_type.get_verb_doer(character_type_id)
    if verb_doer ~= nil then
        verb_doer(character_id, verb_type_id, context)
    end
end
function M.recycle(character_id)
    if character_id == nil then return end
    assert(type(character_id)=="number", "character_id must be a number.")
    world.data.characters[character_id] = {}
end
function M.get_direction(character_id)
    assert(type(character_id)=="number", "character_id must be a number.")
    return get_character_data(character_id).direction_id
end
function M.set_direction(character_id, direction_id)
    assert(type(character_id)=="number", "character_id must be a number.")
    assert(type(direction_id)=="string", "direction_id must be a string.")
    get_character_data(character_id).direction_id = direction_id
end
function M.get_description(character_id)
    if character_id == nil then return "" end
    local describer = character_type.get_describer(M.get_character_type(character_id))
    if describer == nil then return "" end
    return describer(character_id)
end
return M