local state = require "scene.state"
local states = require "game.states"
local commands = require "game.commands"
local fonts = require "gfx.fonts"
local hues  = require "gfx.hues"
local gfx   = require "gfx.gfx"
local images= require "gfx.images"
local M = {}

function M.new(parent)
    local instance = state.new(parent)
    function instance:on_draw()
        images.SPLASH:draw(0,0)
        local view_size = gfx.get_size()
        local font = fonts.M6X11PLUS_96
        font:write_centered("Tomb of Woeful DOOM!", view_size.width/2+4,4+view_size.height / 2 - font:get_height()/2, hues.CYAN)
        font:write_centered("Tomb of Woeful DOOM!", view_size.width/2, view_size.height / 2 - font:get_height()/2, hues.LIGHT_CYAN)
        font = fonts.M6X11PLUS_48
        font:write_centered("Press SPACE or (A)", view_size.width/2+2,2+ view_size.height - font:get_height(), hues.DARK_GRAY)
        font:write_centered("Press SPACE or (A)", view_size.width/2, view_size.height - font:get_height(), hues.WHITE)
    end
    function instance:on_command(command)
        if command == commands.GREEN then
            self:get_parent():set_state(states.MAIN_MENU)
        end
    end
    function instance:on_load()
    end
    return instance
end

return M