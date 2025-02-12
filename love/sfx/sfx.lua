local json = require "json"

local M = {}
local DEFAULT_MASTER_VOLUME = 0.5
local DEFAULT_SFX_VOLUME = 0.75
local DEFAULT_MUX_VOLUME = 1.0
local AUDIO_SETTINGS_FILENAME = "audio_settings.json"
local settings = {
    master_volume = DEFAULT_MASTER_VOLUME,
    sfx_volume = DEFAULT_SFX_VOLUME,
    mux_volume = DEFAULT_MUX_VOLUME
}

local sfx_sources = {}
local mux_sources = {}

function M.load()
    local file_info = love.filesystem.getInfo(AUDIO_SETTINGS_FILENAME, "file")
    if file_info == nil then
        M.set_master_volume(DEFAULT_MASTER_VOLUME)
        M.set_sfx_volume(DEFAULT_SFX_VOLUME)
        M.set_mux_volume(DEFAULT_MUX_VOLUME)
    else
        local file = love.filesystem.newFile(AUDIO_SETTINGS_FILENAME)
        file:open("r")
        local status, loaded_settings = pcall(function() return json.decode(file:read()) end)
        if status then
            M.set_master_volume(loaded_settings.master_volume or DEFAULT_MASTER_VOLUME)
            M.set_sfx_volume(loaded_settings.sfx_volume or DEFAULT_SFX_VOLUME)
            M.set_mux_volume(loaded_settings.mux_volume or DEFAULT_MUX_VOLUME)
        else
            M.set_master_volume(DEFAULT_MASTER_VOLUME)
            M.set_sfx_volume(DEFAULT_SFX_VOLUME)
            M.set_mux_volume(DEFAULT_MUX_VOLUME)
        end
        file:close()
    end
end

function M.add_sfx_source(source)
    table.insert(sfx_sources, source)
end

function M.add_mux_source(source)
    table.insert(mux_sources, source)
end

function M.save()
    local file = love.filesystem.newFile(AUDIO_SETTINGS_FILENAME)
    file:open("w")
    file:write(json.encode(settings))
    file:close()
end

function M.get_master_volume()
    return settings.master_volume
end

function M.get_sfx_volume()
    return settings.sfx_volume
end

function M.get_mux_volume()
    return settings.mux_volume
end

function M.set_master_volume(volume)
    settings.master_volume = volume
end

function M.set_sfx_volume(volume)
    settings.sfx_volume = volume
end

function M.set_mux_volume(volume)
    settings.mux_volume = volume
end

function M.apply_volumes()
    local sfx_volume = M.get_master_volume() * M.get_sfx_volume()
    for _, source in ipairs(sfx_sources) do
        source:set_volume(sfx_volume)
    end

    local mux_volume = M.get_master_volume() * M.get_mux_volume()
    for _, source in ipairs(mux_sources) do
        source:set_volume(mux_volume)
    end
end

return M