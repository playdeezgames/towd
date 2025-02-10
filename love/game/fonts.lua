local font_wrapper = require "gfx.font_wrapper"
local font_id      = require "game.font_id"
local M = {}

local font_table = {
    [font_id.M6X11PLUS_48] = {"assets/fonts/m6x11plus.ttf",48},
    [font_id.M6X11PLUS_96] = {"assets/fonts/m6x11plus.ttf",96}
}


function M.load()
    for k, v in pairs(font_table) do
        M[k] = font_wrapper.new(v[1], v[2])
    end
end

return M