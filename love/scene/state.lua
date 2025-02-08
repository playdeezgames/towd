local actor = require "scene.actor"
local M = {}
function M.new(parent)
    local instance = actor.new(parent)
    function instance:start()
        self:enable()
        if self.on_start ~= nil then
            self:on_start()
        end
    end
    function instance:finish()
        self:disable()
        if self.on_finish ~= nil then
            self:on_finish()
        end
    end
    instance:disable()
    return instance
end
return M