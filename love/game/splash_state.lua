local state = require "scene.state"
local states = require "game.states"
local commands = require "game.commands"
local fonts = require "gfx.fonts"
local hues  = require "gfx.hues"
local gfx   = require "gfx.gfx"
local M = {}

function M.new(parent)
    local instance = state.new(parent)
    function instance:on_draw()
        local font = fonts.M6X11PLUS
        local view_size = gfx.get_size()
        font:write_centered("Tomb of Woeful Doom!", view_size.width/2, view_size.height / 2 - font:get_height()/2, hues.LIGHT_CYAN)
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