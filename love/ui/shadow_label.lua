local actor = require "scene.actor"
local label = require "ui.label"
local M = {}
function M.new(parent, font, text, x, y, dx, dy, hue, shadow_hue, justify)
    local instance = actor.new(parent)
    function instance:on_load()
        self.shadow = label.new(self, font, text, x + dx, y + dy, shadow_hue, justify)
        self.label = label.new(self, font, text, x, y, hue, justify)
    end
    function instance:set_hue(hue)
        self.label:set_hue(hue)
    end
    function instance:set_shadow_hue(hue)
        self.shadow:set_hue(hue)
    end
    function instance:get_text()
        return self.label:get_text()
    end
    return instance
end
return M