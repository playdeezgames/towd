local image_wrapper = require "gfx.image_wrapper"
local image_id = require "game.image_id"
local M = {}

local image_table = {
    [image_id.SPLASH] = "assets/images/splash.png"
}

function M.load()
    for k, v in pairs(image_table) do
        M[k] = image_wrapper.new(v)
    end
end


return M