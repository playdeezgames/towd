local actor = require "scene.actor"
local M = {}

function M.new(parent, image, x, y)
    local instance = actor.new(parent)
    instance.image = image
    instance.x = x
    instance.y = y
    instance.hue = {1,1,1}
    function instance:on_draw()
        local r, g, b, a = love.graphics.getColor()
        love.graphics.setColor(self.hue)
        self.image:draw(x,y)
        love.graphics.setColor(r,g,b,a)
    end
    function instance:get_left()
        return self.x
    end
    function instance:get_top()
        return self.y
    end
    function instance:get_right()
        return self:get_left() + instance.image:get_width() - 1
    end
    function instance:get_bottom()
        return self:get_top() + instance.image:get_height() - 1
    end
    function instance:set_hue(hue)
        instance.hue = hue
    end
    return instance
end

return M