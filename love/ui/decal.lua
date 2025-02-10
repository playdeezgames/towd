local actor = require "scene.actor"
local M = {}

function M.new(parent, image, x, y)
    local instance = actor.new(parent)
    instance.image = image
    instance.x = x
    instance.y = y
    function instance:on_draw()
        self.image:draw(x,y)
    end
    return instance
end

return M