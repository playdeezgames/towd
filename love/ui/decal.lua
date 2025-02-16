local actor = require "scene.actor"
local M = {}
function M.new(parent, properties)
    local instance = actor.new(parent, properties)
    return instance
end
return M