local M = {}
local VIEW_WIDTH = 1280
local VIEW_HEIGHT = 720
M.VIEW_WIDTH = VIEW_WIDTH
M.VIEW_HEIGHT = VIEW_HEIGHT
function M.clear(hue)
    assert(type(hue)=="table","hue should be a table")
    love.graphics.clear(hue[1],hue[2],hue[3])
end
function M.get_size()
    local width, height = love.window.getMode()
    return { width = width, height = height}
end
function M.update_scale()
    local view_size = M.get_size()
    love.graphics.scale(view_size.width / M.VIEW_WIDTH, view_size.height / M.VIEW_HEIGHT)
end
function M.adjust_xy(x,y)
    local view_size = M.get_size()
    return x * M.VIEW_WIDTH / view_size.width, y * M.VIEW_HEIGHT / view_size.height
end
return M