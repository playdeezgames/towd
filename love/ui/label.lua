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
    function instance:on_draw()
        if self.justify == M.CENTER then
            font:write_centered(self.text, self.x, self.y, self.hue)
        else
            font:write(self.text, self.x, self.y, self.hue)
        end
    end
    return instance
end
return M