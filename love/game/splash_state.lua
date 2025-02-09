local state = require "scene.state"
local states = require "game.states"
local commands = require "game.commands"
local fonts = require "gfx.fonts"
local hues  = require "gfx.hues"
local M = {}

function M.new(parent)
    local instance = state.new(parent)
    function instance:on_draw()
        fonts.M6X11PLUS:write("Press SPACE or (A)", 0, 0, hues.WHITE)
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