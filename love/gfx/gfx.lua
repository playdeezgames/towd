local font = require "gfx.font"
local grimoire = require "game.grimoire"

local M = {}

local fonts = {}

function M.load()
    fonts[grimoire.FONT3X5] = font.new("/assets/images/Font3x5.png")
    fonts[grimoire.FONT4X6] = font.new("/assets/images/Font4x6.png")
    fonts[grimoire.FONT5X7] = font.new("/assets/images/Font5x7.png")
    fonts[grimoire.FONT8X8] = font.new("/assets/images/Font8x8.png")
end

function M.get_font(font_id)
    return fonts[font_id]
end

return M