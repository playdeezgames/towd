local source_id = require "game.sfx.source_id"
local source_wrapper = require "sfx.source_wrapper"
local sfx = require "sfx.sfx"
local M = {}

local source_table = {
    [source_id.BLIP] = {"assets/sources/BLIP.wav","static",false},
    [source_id.BOOP] = {"assets/sources/BOOP.wav","static",false},
    [source_id.MINOR_SONG] = {"assets/sources/MINOR_SONG.wav","stream",true}
}

function M.load()
    for k, v in pairs(source_table) do
        local filename = v[1]
        local file_type = v[2]
        local is_mux = v[3]
        local new_source = source_wrapper.new(filename,file_type)
        M[k] = new_source
        if is_mux then
            sfx.add_mux_source(new_source)
        else
            sfx.add_sfx_source(new_source)
        end
        sfx.apply_volumes()
    end
end


return M