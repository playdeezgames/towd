local avatar = require "world.avatar"
local character = require "world.character"
local room_cell = require "world.room_cell"
local directions= require "game.directions"
local room      = require "world.room"
local colors    = require "game.colors"
local M = {}
local romfont = require "romfont"
local TOOL_TIP_COLUMNS = 80
local TOOL_TIP_ROWS = 1
local TOOL_TIP_OFFSET_X = 0
local TOOL_TIP_OFFSET_Y = 640
local TOOL_TIP_SCALE_X = 2
local TOOL_TIP_SCALE_Y = 2

local tool_tip_text = ""
local tool_tip_canvas = nil

function M.set_up()
    tool_tip_canvas = love.graphics.newCanvas(TOOL_TIP_COLUMNS * romfont.ROMFONT_CELL_WIDTH, TOOL_TIP_ROWS * romfont.ROMFONT_CELL_HEIGHT)
    tool_tip_canvas:setFilter("nearest","nearest")
end

function M.set_text(text)
    tool_tip_text = text
end

local function get_cursor_position(character_id)
	local direction_id = character.get_direction(character_id)
	if direction_id == nil then return end
	local room_cell_id = character.get_room_cell(character_id)
	local column, row = room_cell.get_position(room_cell_id)
	return directions.get_next_position(direction_id, column, row)
end

function M.update()
    local character_id = avatar.get_character()
    local room_cell_id = room.get_room_cell(character.get_room(character_id), get_cursor_position(character_id))
    tool_tip_text = room_cell.get_description(room_cell_id)
end

function M.draw()
    love.graphics.setCanvas(tool_tip_canvas)
    love.graphics.clear()
    local r, g, b, a = love.graphics.getColor()
    love.graphics.setColor(colors.WHITE)
    local plot_x = (TOOL_TIP_COLUMNS - #tool_tip_text) * romfont.ROMFONT_CELL_WIDTH / 2
    local plot_y = 0
    for index = 1, #tool_tip_text do
        local character = string.byte(tool_tip_text, index) + 1
        love.graphics.draw(romfont.romfont_tileset_image, romfont.romfont_tile_quads[character], plot_x, plot_y)
        plot_x = plot_x + romfont.ROMFONT_CELL_WIDTH
    end
    love.graphics.setColor(r, g, b, a)
    love.graphics.setCanvas()
    love.graphics.draw(tool_tip_canvas, TOOL_TIP_OFFSET_X, TOOL_TIP_OFFSET_Y, 0, TOOL_TIP_SCALE_X, TOOL_TIP_SCALE_Y)
end

return M