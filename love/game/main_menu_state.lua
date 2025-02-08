local state = require "scene.state"
local states = require "game.states"
local M = {}

function M.new(parent)
    local instance = state.new(parent)
    function instance:on_draw()
        love.graphics.print("MAIN MENU", 0, 0)
    end
    return instance
end

return M