local actor = require "scene.actor"
local label = require "ui.label"
local M = {}
function M.new(parent, font, text, x, y, dx, dy, hue, shadow_hue, justify)
    local instance = actor.new(parent)
    function instance:on_load()
        label.new(self, font, text, x + dx, y + dy, shadow_hue, justify)
        label.new(self, font, text, x, y, hue, justify)
    end
    return instance
end
return M