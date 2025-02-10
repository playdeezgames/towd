local M = {}
function M.clear(hue)
    assert(type(hue)=="table","hue should be a table")
    love.graphics.clear(hue[1],hue[2],hue[3])
end
function M.get_size()
    local width, height = love.window.getMode()
    return { width = width, height = height}
end
return M