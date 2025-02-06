local M = {}

local ROMFONT_TILESET_FILENAME = "assets/images/romfont8x8.png"
M.ROMFONT_CELL_WIDTH = 8
M.ROMFONT_CELL_HEIGHT = 8

M.romfont_tile_quads = {}
M.romfont_tileset_image = nil

function M.set_up()
  M.romfont_tileset_image = love.graphics.newImage(ROMFONT_TILESET_FILENAME)
  local image_width = M.romfont_tileset_image:getWidth()
  local image_height = M.romfont_tileset_image:getHeight()
  local tile_columns = image_width / M.ROMFONT_CELL_WIDTH
  local tile_rows = image_height / M.ROMFONT_CELL_HEIGHT
  for row = 1, tile_rows do
    for column = 1, tile_columns do
      local source_x = column * M.ROMFONT_CELL_WIDTH - M.ROMFONT_CELL_WIDTH
      local source_y = row * M.ROMFONT_CELL_HEIGHT - M.ROMFONT_CELL_HEIGHT
      local quad = love.graphics.newQuad(source_x, source_y, M.ROMFONT_CELL_WIDTH, M.ROMFONT_CELL_HEIGHT, M.romfont_tileset_image)
      table.insert(M.romfont_tile_quads, quad)
    end
  end
end

return M