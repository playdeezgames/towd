local colors = require "game.colors"
local avatar = require "world.avatar"
local character = require "world.character"
local room      = require "world.room"
local gfx       = require "gfx.gfx"
local grimoire  = require "game.grimoire"
local room_cell = require "world.room_cell"
local room_cell_type = require "world.room_cell_type"
local character_type = require "world.character_type"
local verb_type      = require "world.verb_type"
local directions     = require "game.directions"

local M = {}
function M.set_state(_)
    assert(false, "set_state did not get set by the state machine")
end
function M.load()
end
function M.update()
    love.graphics.setBackgroundColor(colors.BLACK)
    love.graphics.clear()
    local room_id = character.get_room(avatar.get_character())
    local columns, rows = room.get_size(room_id)
    local terrain_tileset = gfx.get_tileset(grimoire.TILESET_TERRAIN)
    local character_tileset = gfx.get_tileset(grimoire.TILESET_CHARACTERS)
    for column = 1, columns do
        local x = (column - 1) * terrain_tileset.cell_width
        for row = 1, rows do
            local y = (row - 1) * terrain_tileset.cell_height

            local room_cell_id = room.get_room_cell(room_id, column, row)
            local room_cell_type_id = room_cell.get_room_cell_type(room_cell_id)
            local tile = room_cell_type.get_tile(room_cell_type_id)
            terrain_tileset:draw_tile(tile, x, y)

            local character_id = room_cell.get_character(room_cell_id)
            if character_id ~= nil then
                local character_type_id = character.get_character_type(character_id)
                tile = character_type.get_tile(character_type_id)
                character_tileset:draw_tile(tile, x, y)
            end
        end
    end
end
function M.handle_command(command, isrepeat)
    if command == grimoire.COMMAND_UP then
        character.do_verb(avatar.get_character(), verb_type.MOVE, {direction_id = directions.NORTH})
    elseif command == grimoire.COMMAND_DOWN then
        character.do_verb(avatar.get_character(), verb_type.MOVE, {direction_id = directions.SOUTH})
    elseif command == grimoire.COMMAND_LEFT then
        character.do_verb(avatar.get_character(), verb_type.MOVE, {direction_id = directions.WEST})
    elseif command == grimoire.COMMAND_RIGHT then
        character.do_verb(avatar.get_character(), verb_type.MOVE, {direction_id = directions.EAST})
    end
end
function M.start()
end
function M.finish()
end
return M