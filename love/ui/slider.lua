local actor = require "scene.actor"
local M = {}
function M.new(parent, options)
    assert(type(parent)=="table","parent should be a table")
    assert(type(options)=="table", "options should be a table")
    local instance = actor.new(parent)
    instance.x = options.x
    instance.y = options.y
    instance.width = options.width
    instance.height = options.height
    instance.border_hue = options.border_hue
    instance.empty_hue = options.empty_hue
    instance.full_hue = options.full_hue
    instance.value = options.value
    function instance:legacy_on_draw()
        local r, g, b, a = love.graphics.getColor()
        love.graphics.setColor(self.border_hue)
        love.graphics.rectangle("fill", self.x, self.y, self.width, self.height)
        love.graphics.setColor(self.empty_hue)
        love.graphics.rectangle("fill", self.x + 2, self.y + 2, self.width - 4, self.height - 4)
        love.graphics.setColor(self.full_hue)
        love.graphics.rectangle("fill", self.x + 2, self.y + 2, math.floor((self.width - 4) * self.value), self.height - 4)
        love.graphics.setColor(r, g, b, a)
    end
    function instance:set_value(value)
        self.value = value
    end
    function instance:set_border_hue(hue)
        self.border_hue = hue
    end
    function instance:set_full_hue(hue)
        self.full_hue = hue
    end
    function instance:legacy_get_top()
        return self.y
    end
    function instance:legacy_get_bottom()
        return self:legacy_get_top() + self.height - 1
    end
    function instance:legacy_get_left()
        return self.x
    end
    function instance:legacy_get_right()
        return self:legacy_get_left() +  self.width - 1
    end
    return instance
end
return M