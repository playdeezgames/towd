local room_type = require "world.room_type"
local room = require "world.room"
local room_cell = require "world.room_cell"
local feature = require "world.feature"
local room_cell_type = require "world.room_cell_type"
local character = require "world.character"

local terrain_table = {
    [room_cell_type.BLANK] = 1
}

local function generate_room_cell_type(column, row, columns, rows)
    if row == 1 then
        if column == 1 then
            return room_cell_type.NORTHWEST_INSIDE_CORNER
        elseif column == columns then
            return room_cell_type.NORTHEAST_INSIDE_CORNER
        else
            return room_cell_type.NORTH_WALL
        end
    elseif row == rows then
        if column == 1 then
            return room_cell_type.SOUTHWEST_INSIDE_CORNER
        elseif column == columns then
            return room_cell_type.SOUTHEAST_INSIDE_CORNER
        else
            return room_cell_type.SOUTH_WALL
        end
    else
        if column == 1 then
            return room_cell_type.WEST_WALL
        elseif column == columns then
            return room_cell_type.EAST_WALL
        end
    end
    local total = 0
    for _, v in pairs(terrain_table) do
        total = total + v
    end
    local generated = math.random(1, total)
    for k, v in pairs(terrain_table) do
        if generated <= v then
            return k
        else
            generated = generated - v
        end
    end
end

local function initialize_terrain(room_id)
    local columns = room.get_columns(room_id)
    local rows = room.get_rows(room_id)
    for column = 1, columns do
        for row = 1, rows do
            local room_cell_id = room.get_room_cell(room_id, column, row)
            feature.recycle(room_cell.get_feature(room_cell_id))
            character.recycle(room_cell.get_character(room_cell_id))
            room_cell.set_room_cell_type(room_cell_id, generate_room_cell_type(column, row, columns, rows))
        end
    end
end

room_type.set_initializer(
    room_type.START,
    function(room_id)
        initialize_terrain(room_id)
    end)
return nil