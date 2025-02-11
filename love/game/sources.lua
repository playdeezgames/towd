local source_id = require "game.source_id"
local source_wrapper = require "sfx.source_wrapper"
local M = {}

local source_table = {
    [source_id.BLIP] = {"assets/sources/BLIP.wav","static"},
    [source_id.BOOP] = {"assets/sources/BOOP.wav","static"}
}

function M.load()
    for k, v in pairs(source_table) do
        M[k] = source_wrapper.new(v[1],v[2])
    end
end


return M