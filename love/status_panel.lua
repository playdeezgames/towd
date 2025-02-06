local character = require "world.character"
local avatar = require "world.avatar"
local statistic_type = require "world.statistic_type"
local romfont = require "romfont"
local colors  = require "game.colors"
local M = {}

local STATUS_PANEL_COLUMNS = 20
local STATUS_PANEL_ROWS = 40
local STATUS_PANEL_OFFSET_X = 0
local STATUS_PANEL_OFFSET_Y = 0
local STATUS_PANEL_SCALE_X = 2
local STATUS_PANEL_SCALE_Y = 2

local status_panel_cells
local status_panel_canvas

function M.draw()
    love.graphics.setCanvas(status_panel_canvas)
    love.graphics.clear()
    local r,g,b,a = love.graphics.getColor()
    for row = 1, STATUS_PANEL_ROWS do
      local plot_y = row * romfont.ROMFONT_CELL_HEIGHT - romfont.ROMFONT_CELL_HEIGHT
      for column = 1, STATUS_PANEL_COLUMNS do
        local plot_x = column * romfont.ROMFONT_CELL_WIDTH - romfont.ROMFONT_CELL_WIDTH
        local cell = status_panel_cells[row][column]
        love.graphics.setColor(cell.color)
        love.graphics.draw(romfont.romfont_tileset_image, romfont.romfont_tile_quads[cell.character], plot_x, plot_y)
      end
    end
    love.graphics.setColor(r,g,b,a)
    love.graphics.setCanvas()
    love.graphics.draw(status_panel_canvas, STATUS_PANEL_OFFSET_X, STATUS_PANEL_OFFSET_Y, 0, STATUS_PANEL_SCALE_X, STATUS_PANEL_SCALE_Y)
end

local function clear_status_panel()
    for row = 1, STATUS_PANEL_ROWS do
      for column = 1, STATUS_PANEL_COLUMNS do
        status_panel_cells[row][column].character = 1
      end
    end
end

local function write_status_panel(column, row, color, text)
    for index = 1, #text do
      local cell = status_panel_cells[row][column]
      cell.color = color
      cell.character = string.byte(text, index) + 1
      column = column + 1
    end
end

local function show_stat(row, character_id, statistic_type_id, text)
  local value = character.get_statistic(character_id, statistic_type_id)
  write_status_panel(1, row, colors.LIGHT_GRAY, text..value)
  return row + 1
end

local function show_range_stat(row, character_id, statistic_type_id, maximum_statistic_type_id, text)
  local value = character.get_statistic(character_id, statistic_type_id)
  local maximum_value = character.get_statistic(character_id, maximum_statistic_type_id)
  write_status_panel(1, row, colors.LIGHT_GRAY, text..value.."/"..maximum_value)
  return row + 1
end

function M.update()
    local character_id = avatar.get_character()
    clear_status_panel()
    local row = 1
    row = show_range_stat(row, character_id, statistic_type.XP, statistic_type.XP_GOAL, "      XP: ")
    row = show_stat(row, character_id, statistic_type.XP_LEVEL, "   Level: ")
    row = show_stat(row, character_id, statistic_type.MOVES, "   Moves: ")
    row = show_stat(row, character_id, statistic_type.TREES_MURDERED, "   Trees: ")
    row = show_range_stat(row, character_id, statistic_type.ENERGY, statistic_type.MAXIMUM_ENERGY, "  Energy: ")
    row = show_range_stat(row, character_id, statistic_type.HEALTH, statistic_type.MAXIMUM_HEALTH, "  Health: ")
    row = show_stat(row, character_id, statistic_type.WOOD, "    Wood: ")
    row = show_stat(row, character_id, statistic_type.JOOLS, "   Jools: ")
    row = show_stat(row, character_id, statistic_type.STRENGTH, "Strength: ")
    row = show_stat(row, character_id, statistic_type.ADVANCEMENT_POINTS, "      AP: ")
end

function M.set_up()
    status_panel_canvas = love.graphics.newCanvas(STATUS_PANEL_COLUMNS * romfont.ROMFONT_CELL_WIDTH, STATUS_PANEL_ROWS * romfont.ROMFONT_CELL_HEIGHT)
    status_panel_canvas:setFilter("nearest","nearest")
    status_panel_cells = {}
    while #status_panel_cells < STATUS_PANEL_ROWS do
      local line = {}
      table.insert(status_panel_cells, line)
      while #line < STATUS_PANEL_COLUMNS do
        local cell = {color={1,1,1},character=1}
        table.insert(line, cell)
      end
    end
end

return M