local state = require "scene.state"
local states = require "game.states"
local commands = require "game.commands"
local M = {}

function M.new(parent)
    local instance = state.new(parent)
    function instance:on_draw()
        love.graphics.print("SPLASH", 0, 0)
    end
    function instance:on_command(command)
        if command == commands.GREEN then
            self:get_parent():set_state(states.MAIN_MENU)
        end
    end
    return instance
end

return M