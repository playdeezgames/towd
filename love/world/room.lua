local room_cell = require "world.room_cell"
local room_type = require "world.room_type"
local world = require "world.world"
local feature = require "world.feature"
local character = require "world.character"
local M = {}
world.data.rooms = {}

local function get_room_data(room_id)
    return world.data.rooms[room_id]
end
function M.initialize(room_id)
    assert(type(room_id) == "number", "room_id should be a number")
    local initializer = room_type.get_initializer(M.get_room_type(room_id))
    if initializer ~= nil then
        initializer(room_id)
    end
end
function M.create(room_type_id, columns, rows)
    local room_id = #world.data.rooms + 1
    world.data.rooms[room_id]={
        room_type_id = room_type_id,
        columns = columns,
        rows = rows,
        cells = {}
    }
    for column = 1, columns do
        get_room_data(room_id).cells[column] = {}
        for row = 1, rows do
            get_room_data(room_id).cells[column][row] = room_cell.create(room_type.get_room_cell_type(M.get_room_type(room_id)), room_id, column, row)
        end
    end
    M.initialize(room_id)
    return room_id
end
function M.get_room_type(room_id)
    assert(type(room_id) == "number", "room_id should be a number")
    return get_room_data(room_id).room_type_id
end
function M.get_columns(room_id)
    assert(type(room_id) == "number", "room_id should be a number")
    return get_room_data(room_id).columns
end
function M.get_rows(room_id)
    assert(type(room_id) == "number", "room_id should be a number")
    return get_room_data(room_id).rows
end
function M.get_size(room_id)
    return M.get_columns(room_id), M.get_rows(room_id)
end
function M.get_room_cell(room_id, column, row)
    if column == nil or row == nil then return nil end
    assert(type(room_id) == "number", "room_id should be a number")
    assert(type(column) == "number", "column should be a number")
    assert(type(row) == "number", "row should be a number")
    local room_column = get_room_data(room_id).cells[column]
    if room_column == nil then
        return nil
    end
    return room_column[row]
end
function M.set_statistic(room_id, statistic_type_id, statistic_value)
    local old_value = M.get_statistic(room_id, statistic_type_id)
    assert(type(room_id)=="number", "room_id should be a number")
    assert(type(statistic_type_id)=="string", "statistic_type_id should be a string")
    assert(type(statistic_value)=="number", "statistic_value should be a number")
    local feature_data = get_room_data(room_id)
    if feature_data.statistics == nil then
        feature_data.statistics = {}
    end
    feature_data.statistics[statistic_type_id]=statistic_value
    return old_value
end
function M.get_statistic(room_id, statistic_type_id)
    assert(type(room_id)=="number", "room_id should be a number")
    assert(type(statistic_type_id)=="string", "statistic_type_id should be a string")
    local feature_data = get_room_data(room_id)
    if feature_data.statistics == nil then
        return nil
    end
    return feature_data.statistics[statistic_type_id]
end
function M.change_statistic(room_id, statistic_type_id, delta)
    assert(type(room_id)=="number", "room_id should be a number")
    assert(type(statistic_type_id)=="string", "statistic_type_id should be a string")
    assert(type(delta)=="number", "delta should be a number")
    local new_value = M.get_statistic(room_id, statistic_type_id) + delta
    M.set_statistic(room_id, statistic_type_id, new_value)
    return new_value
end
function M.get_feature(room_id, column, row)
    assert(type(room_id)=="number", "room_id should be a number")
    assert(type(column)=="number", "column should be a number")
    assert(type(row)=="number", "row should be a number")
    local room_cell_id = M.get_room_cell(room_id, column, row)
    if room_cell_id == nil then return nil end
    return room_cell.get_feature(room_cell_id)
end
function M.create_features(room_id, feature_type_id, feature_count)
    local result = {}
    local columns, rows = M.get_size(room_id)
    while feature_count > 0 do
        local room_cell_id = M.get_room_cell(room_id, math.random(1, columns), math.random(1, rows))
        if room_cell.can_enter(room_cell_id) then
            local feature_id = feature.create(feature_type_id)
            room_cell.set_feature(room_cell_id, feature_id)
            table.insert(result, feature_id)
            feature_count = feature_count - 1
        end
    end
    return result
end
function M.create_characters(room_id, character_type_id, character_count)
    local result = {}
    local columns, rows = M.get_size(room_id)
    while character_count > 0 do
        local room_cell_id = M.get_room_cell(room_id, math.random(1, columns), math.random(1, rows))
        if room_cell.can_enter(room_cell_id) then
            local character_id = character.create(character_type_id, room_cell_id)
            table.insert(result, character_id)
            character_count = character_count - 1
        end
    end
    return result
end
return M