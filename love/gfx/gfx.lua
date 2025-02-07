local font = require "gfx.font"
local tileset = require "gfx.tileset"
local grimoire = require "game.grimoire"

local M = {}

local fonts = {}
local tilesets = {}

function M.load()
    fonts[grimoire.FONT3X5] = font.new("/assets/images/Font3x5.png")
    fonts[grimoire.FONT4X6] = font.new("/assets/images/Font4x6.png")
    fonts[grimoire.FONT5X7] = font.new("/assets/images/Font5x7.png")
    fonts[grimoire.FONT7X7] = font.new("/assets/images/Font7x7.png")

    tilesets[grimoire.TILESET_CHARACTERS] = tileset.new("/assets/images/Characters.png",8,16)
    tilesets[grimoire.TILESET_TERRAIN] = tileset.new("/assets/images/Terrain.png",8,16)
    tilesets[grimoire.TILESET_ITEMS] = tileset.new("/assets/images/Items.png",8,16)
end
function M.get_font(font_id)
    return fonts[font_id]
end
function M.get_tileset(tileset_id)
    return tilesets[tileset_id]
end

return M