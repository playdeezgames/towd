local actor = require "scene.actor"
local M = {}
function M.new()
    local instance = actor.new(nil)
    return instance
end
return M