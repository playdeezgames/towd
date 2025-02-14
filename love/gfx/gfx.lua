local M = {}
M.VIEW_WIDTH = 1280
M.VIEW_HEIGHT = 720
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
    local x_ratio = view_size.width / M.VIEW_WIDTH
    local y_ratio = view_size.height / M.VIEW_HEIGHT
    local ratio = math.min(x_ratio, y_ratio)
    local width = M.VIEW_WIDTH * ratio
    local height = M.VIEW_HEIGHT * ratio
    local x_offset = (view_size.width - width) / 2
    local y_offset = (view_size.height - height) / 2
    love.graphics.translate(x_offset, y_offset)
    love.graphics.scale(ratio, ratio)
end
function M.adjust_xy(x,y)
    local view_size = M.get_size()
    local x_ratio = view_size.width / M.VIEW_WIDTH
    local y_ratio = view_size.height / M.VIEW_HEIGHT
    local ratio = math.min(x_ratio, y_ratio)
    local width = M.VIEW_WIDTH * ratio
    local height = M.VIEW_HEIGHT * ratio
    local x_offset = (view_size.width - width) / 2
    local y_offset = (view_size.height - height) / 2
    return (x - x_offset) / ratio, (y - y_offset) / ratio
end
function M.handle_resize(w,h)
    --TODO: may be unneeded
end
return M