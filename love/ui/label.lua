local actor = require "scene.actor"
local M = {}
M.LEFT = "LEFT"
M.CENTER = "CENTER"
function M.new(parent, font, text, x, y, hue, justify)
    local instance = actor.new(parent)
    instance.font = font
    instance.text = text
    instance.x = x
    instance.y = y
    instance.hue = hue
    instance.justify = justify
    function instance:legacy_on_draw()
        if self.justify == M.CENTER then
            font:write_centered(self.text, self.x, self.y, self.hue)
        else
            font:write(self.text, self.x, self.y, self.hue)
        end
    end
    function instance:set_hue(hue)
        self.hue = hue
    end
    function instance:get_text()
        return self.text
    end
    function instance:set_text(text)
        self.text = text
    end
    function instance:legacy_get_top()
        return self.y
    end
    function instance:legacy_get_bottom()
        return self:legacy_get_top() + self.font:get_height() - 1
    end
    return instance
end
return M