local romfont = require "romfont"
local M = {}

local MESSAGE_PANEL_COLUMNS = 20
local MESSAGE_PANEL_ROWS = 40
local MESSAGE_PANEL_OFFSET_X = 960
local MESSAGE_PANEL_OFFSET_Y = 0
local MESSAGE_PANEL_SCALE_X = 2
local MESSAGE_PANEL_SCALE_Y = 2

local message_panel_cells
local message_panel_canvas

local message_panel_column = 1
local message_panel_row = 1

function M.draw()
    love.graphics.setCanvas(message_panel_canvas)
    love.graphics.clear()
    local r,g,b,a = love.graphics.getColor()
    for row = 1, MESSAGE_PANEL_ROWS do
      local plot_y = row * romfont.ROMFONT_CELL_HEIGHT - romfont.ROMFONT_CELL_HEIGHT
      for column = 1, MESSAGE_PANEL_COLUMNS do
        local plot_x = column * romfont.ROMFONT_CELL_WIDTH - romfont.ROMFONT_CELL_WIDTH
        local cell = message_panel_cells[row][column]
        love.graphics.setColor(cell.color)
        love.graphics.draw(romfont.romfont_tileset_image, romfont.romfont_tile_quads[cell.character], plot_x, plot_y)
      end
    end
    love.graphics.setColor(r,g,b,a)
    love.graphics.setCanvas()
    love.graphics.draw(message_panel_canvas, MESSAGE_PANEL_OFFSET_X, MESSAGE_PANEL_OFFSET_Y, 0, MESSAGE_PANEL_SCALE_X, MESSAGE_PANEL_SCALE_Y)
end

function M.set_up()
    message_panel_canvas = love.graphics.newCanvas(MESSAGE_PANEL_COLUMNS * romfont.ROMFONT_CELL_WIDTH, MESSAGE_PANEL_ROWS * romfont.ROMFONT_CELL_HEIGHT)
    message_panel_canvas:setFilter("nearest","nearest")
    message_panel_cells = {}
    while #message_panel_cells < MESSAGE_PANEL_ROWS do
      local line = {}
      table.insert(message_panel_cells, line)
      while #line < MESSAGE_PANEL_COLUMNS do
        local cell = {color={1,1,1},character=1}
        table.insert(line, cell)
      end
    end
end

local function write_character(color, character)
    local cell = message_panel_cells[message_panel_row][message_panel_column]
    cell.color = color
    cell.character = character + 1
    message_panel_column = message_panel_column + 1
    if message_panel_column > MESSAGE_PANEL_COLUMNS then
        message_panel_column = message_panel_column - MESSAGE_PANEL_COLUMNS
        message_panel_row = message_panel_row + 1
        while message_panel_row > MESSAGE_PANEL_ROWS do
            local line = table.remove(message_panel_cells, 1)
            for column = 1, #line do
                line[column].character = 1
            end
            table.insert(message_panel_cells, line)
            message_panel_row = message_panel_row - 1
        end
    end
end

function M.write_line(color, text)
    for index = 1, #text do
        write_character(color, string.byte(text, index))
    end
    while message_panel_column ~= 1 do
        write_character(color, 32)
    end
end

return M