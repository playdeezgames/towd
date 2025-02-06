local character = require "world.character"
local room      = require "world.room"
local room_cell = require "world.room_cell"
local room_cell_type = require "world.room_cell_type"
local feature        = require "world.feature"
local feature_type   = require "world.feature_type"
local character_type = require "world.character_type"
local directions     = require "game.directions"
local grimoire       = require "game.grimoire"
local avatar = require "world.avatar"
local M = {}
local GRID_CELL_WIDTH = 8
local GRID_CELL_HEIGHT = 8
local GRID_COLUMNS = 20
local GRID_ROWS = 20
local GRID_OFFSET_X = 320
local GRID_OFFSET_Y = 0
local GRID_SCALE_X = 4
local GRID_SCALE_Y = 4
local GRID_TILESET_FILENAME = "assets/images/colored_tilemap_packed.png"

local grid_tile_quads = {}
local grid_tileset_image
local grid_canvas

function M.set_up()
  grid_tileset_image = love.graphics.newImage(GRID_TILESET_FILENAME)
  grid_canvas = love.graphics.newCanvas(GRID_COLUMNS * GRID_CELL_WIDTH, GRID_ROWS * GRID_CELL_HEIGHT)
  grid_canvas:setFilter("nearest","nearest")
  local image_width = grid_tileset_image:getWidth()
  local image_height = grid_tileset_image:getHeight()
  local tile_columns = image_width / GRID_CELL_WIDTH
  local tile_rows = image_height / GRID_CELL_HEIGHT
  for row = 1, tile_rows do
    for column = 1, tile_columns do
      local source_x = column * GRID_CELL_WIDTH - GRID_CELL_WIDTH
      local source_y = row * GRID_CELL_HEIGHT - GRID_CELL_HEIGHT
      local quad = love.graphics.newQuad(source_x, source_y, GRID_CELL_WIDTH, GRID_CELL_HEIGHT, grid_tileset_image)
      table.insert(grid_tile_quads, quad)
    end
  end
end
local function get_cursor_position(character_id)
	local direction_id = character.get_direction(character_id)
	if direction_id == nil then 
    return room_cell.get_position(character.get_room_cell(character_id))
  end
	local room_cell_id = character.get_room_cell(character_id)
	local column, row = room_cell.get_position(room_cell_id)
	return directions.get_next_position(direction_id, column, row)
end

local function draw_grid_tile(tile, x, y)
  love.graphics.draw(grid_tileset_image, grid_tile_quads[tile], x, y)
end

function M.draw()
  love.graphics.setCanvas(grid_canvas)
  local avatar_character_id = avatar.get_character()
  local cursor_column, cursor_row = get_cursor_position(avatar_character_id)
  local room_id = character.get_room(avatar_character_id)
  for column = 1, room.get_columns(room_id) do
    local plot_x = column * GRID_CELL_WIDTH - GRID_CELL_WIDTH
    for row = 1, room.get_rows(room_id) do
      local plot_y = row * GRID_CELL_HEIGHT - GRID_CELL_HEIGHT
      local room_cell_id = room.get_room_cell(room_id, column, row)
      local room_cell_type_id = room_cell.get_room_cell_type(room_cell_id)
      local tile = room_cell_type.get_tile(room_cell_type_id)
      draw_grid_tile(tile, plot_x, plot_y)

      local feature_id = room_cell.get_feature(room_cell_id)
      if feature_id ~= nil then
        local feature_type_id = feature.get_feature_type(feature_id)
        tile = feature_type.get_tile(feature_type_id)
        love.graphics.draw(grid_tileset_image, grid_tile_quads[tile], plot_x, plot_y)
      end

      local character_id = room_cell.get_character(room_cell_id)
      if character_id ~= nil then
        local character_type_id = character.get_character_type(character_id)
        tile = character_type.get_tile(character_type_id)
        draw_grid_tile(tile, plot_x, plot_y)
      end

      if column == cursor_column and row == cursor_row then
        draw_grid_tile(grimoire.TILE_CURSOR, plot_x, plot_y)
      end
    end
  end
  love.graphics.setCanvas()
  love.graphics.draw(grid_canvas, GRID_OFFSET_X, GRID_OFFSET_Y, 0, GRID_SCALE_X, GRID_SCALE_Y)
end
return M