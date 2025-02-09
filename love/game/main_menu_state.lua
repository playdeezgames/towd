local state = require "scene.state"
local M = {}

function M.new(parent)
    local instance = state.new(parent)
    function instance:on_draw()
        local y = 0
        love.graphics.print("MAIN MENU", 0, y)
    end
    return instance
end

return M