local state = require "scene.state"
local states = require "game.states"
local M = {}

function M.new(parent)
    local instance = state.new(parent)
    function instance:on_draw()
        love.graphics.print("SPLASH", 0, 0)
    end
    function instance:on_keypressed(key, scancode, isrepeat)
        self:get_parent():set_state(states.MAIN_MENU)
    end
    return instance
end

return M